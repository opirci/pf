
import {
    Component, OnInit, ViewChild, ElementRef, Observable,
    ComponentBase, MessageText, Logger, Loggers, CoreHelper,
    LookupList, ReadState, Lookup, DataTable, Row,
    HttpService, LookupResponse, PagedList,
    NotifyMessage, NotifyMessageType, ComponentOptions, DateWeekRange, DateWeek, UserContextService, FederationClaims,
    LcwGridComponent, LcwGridService, LcwGridDataRequest, LcwCalculateSummaryArg, LcwGridSummaryOperators,
    LcwGridCellFormatArg, LcwGridCellFormatValue, ExportFormat

} from "../component.imports";


//import { Component, OnInit, ViewChild, ElementRef, Injectable } from "@angular/core";
//import { Observable } from "rxjs";
//import { ComponentBase, MessageText, Logger, Loggers, CoreHelper } from "../pars/core";
//import { LookupList, ReadState, Lookup, DataTable, Row, DateWeekRange, DateWeek } from "../pars/data";
//import { PagedList, XGridDataSource, IXGridGetDataDelegate, IXGridSearchDataDelegate } from "../common/xgrid.component";
//import { NotifyMessage} from "../common/notify-message.component";

//import { HttpService, LookupResponse } from "../pars/service";


import { SupplierUserLoginReportService, GetSupplierUserLoginReportResponse, GetSupplierUserLoginReportRequest }
    from "./supplier-user-login-report.service";

import { DefinitionService } from "../services/definition.service";



@Component({
    moduleId: module.id,
    selector: "supplier-user-login-report",
    providers: [NotifyMessage, SupplierUserLoginReportService, HttpService],
    templateUrl: "supplier-user-login-report.component.html"
})
export class SupplierUserLoginReportComponent implements OnInit {
    @ViewChild("msgYouDoNotHavePermissionToList") msgYouDoNotHavePermissionToList: MessageText;
    @ViewChild("msgFiltersAreNotValid") msgFiltersAreNotValid: MessageText;
    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
    @ViewChild("msgErrorReadingReport") msgErrorReadingReport: MessageText;
    @ViewChild("msgError") msgError: MessageText;
    @ViewChild("msgWarning") msgWarning: MessageText;
    @ViewChild("msgGridHeader") msgGridHeader: MessageText;

    gridService: LcwGridService;

    reportType: number = 0;
    gettingReport: boolean = false;


    get gridHeaderText(): string {
        return `${this.msgGridHeader.value} (${this.mstartYear}/${this.mstartWeek} - ${this.mendYear}/${this.mendWeek})`;
    }

    range: DateWeekRange;
    mstartWeek: number;
    mstartYear: number;
    mendWeek: number;
    mendYear: number;
    notifyOptions = ComponentOptions.NotifyMessageOptions;

    //#region TestCode
    getDataTable() {
        this.supplierReportService.getDataTable().subscribe(r => {
            this.dtx = r;
            this.dtxxx = new DataTable(r.columns, r.rows, r.pageNumber, r.totalCount);
            console.log(JSON.stringify(r));
        });
    }

    dtx: DataTable;
    dtxxx: DataTable;
    dwr: DateWeekRange;

    saveDataTable() {

        this.supplierReportService.saveDataTable(this.dtxxx).subscribe(r => {
            console.log(JSON.stringify(r));
        });
    }

    getDateWeekRange() {

        this.supplierReportService.getDateWeekRange().subscribe(r => {

            console.log(JSON.stringify(r));
        });

    }
    //#endregion 

    constructor(
        private ucService: UserContextService,
        private notifyService: NotifyMessage,
        private supplierReportService: SupplierUserLoginReportService
    ) {
        this.gridService = LcwGridService.create();

        this.gridService.onDataRequest.subscribe((ctx: LcwGridDataRequest) =>
            this.getReportData(ctx.pageNumber, ctx.pageSize)
        );
    }

    private merrorMessage: string;

    private set errorMessage(value: string) {

        this.merrorMessage = value;
        this.notifyService.createMessage(NotifyMessageType.Error,
            this.msgError,
            this.merrorMessage);
    };

    ngOnInit(): void {
        this.clearFilters();
    }

    clearFilters(): void {
        let xrange = DateWeekRange.now();
        this.mstartWeek = xrange.start.week;
        this.mstartYear = xrange.start.year;
        this.mendWeek = xrange.end.week;
        this.mendYear = xrange.end.year;
        this.gridService.data = null;
        this.reportType = 1;
    }

    private readonly minYear: number = 1900;
    private readonly minWeek: number = 1;

    private readonly maxYear: number = 2100;
    private readonly maxWeek: number = 52;


    calcToplam(args: LcwCalculateSummaryArg): any {
        if (args == null)
            return null;
        let toplam = 0;
        for (let value of args.columnData) {
            toplam += value;
        }
        return toplam;
    }
    

    formatCell(arg: LcwGridCellFormatArg): LcwGridCellFormatValue {
        const ret: LcwGridCellFormatValue = { value: arg.value };
        
        const isNumber = CoreHelper.isNumber(arg.value);
        if (arg.summary) {
            //debugger;
            if (arg.summary.name == "toplam") {
                if (arg.value >= 500)
                    ret.cellStyle = "font-weight: bold;";
            }
            if (arg.summary.name == "ortalama") {
                if (isNumber)
                    ret.value = Number(arg.value).toFixed(2);
            }          
        }
        else {
            if (arg.value == 0) {
                ret.value = ' '
            }/*
            if (arg.value > 1000) {
                ret.cellStyle = "background-color: red;";
            }
            else if (arg.value > 100) {
                ret.cellStyle = "background-color: yellow;";
            }
            else if (arg.value > 50) {
                ret.cellStyle = "background-color: gray;";
            }*/
        }
       
        return ret;
    }

    LcwGridSummaryOperators = LcwGridSummaryOperators;

    calcOrtalama(args: LcwCalculateSummaryArg): any {
        if (args == null)
            return null;
        let toplam = 0;
        for (let value of args.columnData) {
            toplam += value;
        }
        return toplam / args.columnData.length;
    }


    private validateDateWeekRange(startYear: number, startWeek: number, endYear: number, endWeek: number): boolean {
        return this.validateDateWeek(startYear, startWeek) && this.validateDateWeek(endYear, endWeek)
            && ((startYear == endYear && startWeek <= endWeek) || (startYear < endYear));
    }

    private validateDateWeek(year: number, week: number): boolean {
        return !(year < this.minYear || year > this.maxYear || week < this.minWeek || week > this.maxWeek);
    }

    get hasAuthToList(): boolean {
        return this.ucService.userContext.hasClaim(FederationClaims.Vrp_TedPort_Admin);
    }

    get isFilterValid(): boolean {
        return this.validateDateWeekRange(this.mstartYear, this.mstartWeek, this.mendYear, this.mendWeek);
    }

    get canGetReport(): boolean {
        return this.isFilterValid && this.hasAuthToList;
    }

    getReport(): void {
        this.getReportData(1, 1);
    }

    get cannotListMessage(): string {
        let msg = "";
        if (!this.hasAuthToList)
            msg += this.msgYouDoNotHavePermissionToList.value;
        if (!this.isFilterValid)
            msg += (msg == "" ? "" : ", ") + this.msgFiltersAreNotValid.value;

        return msg;
    }

    //test(): void {
    //    this.gridService.getCurrentValues();

    //}

    export(format: ExportFormat) {
        this.gridService.exportTo(format);
    }


    private getReportData(page: number, pageSize: number): void {
        let req: GetSupplierUserLoginReportRequest = new GetSupplierUserLoginReportRequest();
        //req.startYear = this.range.start.year;
        //req.startWeek = this.range.start.week;
        //req.endYear = this.range.end.year;
        //req.endWeek = this.range.end.week;

        req.startYear = this.mstartYear;
        req.startWeek = this.mstartWeek;
        req.endYear = this.mendYear;
        req.endWeek = this.mendWeek;

        req.reportType = this.reportType;
        this.gettingReport = true;

        this.supplierReportService.getSupplierUserLoginReport(req).subscribe(r => {

            if (r.hasErrors || r.hasWarnings) {
                if (r.hasErrors)
                    this.errorMessage = r.errorMessagesText;
                if (r.hasWarnings)
                    this.errorMessage = r.warningMessagesText;
            }
            else {
                if (r!.value!.rows!.length > 0) {
                    //this.reportData = r.value;
                    this.gridService.data = new DataTable(r.value.columns, r.value.rows, r.value.pageNumber, r.value.totalCount);
                }
                else {
                    this.errorMessage = this.msgNoRecordsFound.value;
                }

            }
            this.gettingReport = false;
        },
            error => {
                let errMsg = (error.message) ? error.message :
                    error.status ? `${error.status} - ${error.statusText}` : "Server error";
                this.errorMessage = this.msgErrorReadingReport.value;
                this.gettingReport = false;
            }
        );
    }
}