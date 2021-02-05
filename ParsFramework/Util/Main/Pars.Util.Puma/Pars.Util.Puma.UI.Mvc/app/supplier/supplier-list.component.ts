import { Component, OnInit, ViewChild } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";

import {
    ComponentBase, MessageText, Logger, Loggers, CoreHelper, LookupList, ReadState, Lookup, DataTable, LookupResponse, HttpService, WebApiObservableService,
    LcwGridColumnComponent, PagedList, LcwGridComponent, NotifyMessage, NotifyMessageType,
    LcwGridService, LcwGridDataRequest, ExportFormat
} from "../component.imports";

import { DefinitionService } from "../services/definition.service";
import { SupplierService, GetSuppliersFilteredResponse, GetSuppliersFilteredRequest, Supplier } from "./supplier.service";
import { ComponentOptions } from '../settings/componentOptions';

@Component({
    moduleId: module.id,
    selector: "supplier-list",
    providers: [NotifyMessage, DefinitionService, SupplierService, HttpService, WebApiObservableService,
        { provide: LcwGridService, useFactory: LcwGridService.create }
    ],
    templateUrl: "supplier-list.component.html"
})
export class SupplierListComponent extends ComponentBase implements OnInit {
    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
    @ViewChild("msgErrorReadingSuppliers") msgErrorReadingSuppliers: MessageText;
    


  
    //suppliers: PagedList<Supplier>;
    private _errorMessage: string;

    private get errorMessage(): string { return this._errorMessage; };
    private set errorMessage(value: string) {
        this._errorMessage = value;
        this.notifyService.createMessage(NotifyMessageType.Error, "",
            //this.Msg_ErroHeader,
            this._errorMessage);
    };

    handleError(message: string, errrorString: string, title: string = null) {

    }

    gridColumnInit(column: LcwGridColumnComponent): void {
        //debugger;
        //column.isHidden = true;
    }

    private logger: Logger;
    filter: SupplierListFilter;
    pageSize: number = 20;   
    gridService: LcwGridService;

    //private msgNoRecordsFound: string;

    notifyOptions = ComponentOptions.NotifyMessageOptions;

    constructor(
        private notifyService: NotifyMessage,
        private _definitionService: DefinitionService,
        private _supplierService: SupplierService,
        private _router: Router) {
        super();

        this.logger = new Logger(CoreHelper.nameOfInstance(this), Loggers.Console | Loggers.LocalStore);
        this.logger.suspend();

        this.gridService = LcwGridService.create();
        this.gridService.onDataRequest.subscribe((ctx: LcwGridDataRequest) => {
            this.getSuppliersData(ctx.pageNumber, ctx.pageSize);
        });

        //this.logger.log("Entered getSuppliers of SupplierListComponent");
    }

    gettingSuppiers: boolean = false;

   

    ngOnInit(): void {
        super.ngOnInit();
        //this.logger.warn(this.msgNoRecordsFound.value);
        //this.logger.warn(this.templateText("msgNoRecordsFound"));
        
        

        //if (this.filter == null)
        //    this.filter = JSON.parse(localStorage.getItem("theKey"));

        if (this.filter != null) {
            this.getSuppliersX();
        }
        else {
            this.filter = new SupplierListFilter();

            this.filter.ageSexGroup.readState = ReadState.Reading;
            this._definitionService.getAgeSexGroupsAsLookup()
                .subscribe((response: LookupResponse) => this.filter.ageSexGroup.values = response.values,
                (error: any) => {
                    this.filter.ageSexGroup.readState = ReadState.Error;
                    this.errorMessage = error;
                },
                () => this.filter.ageSexGroup.readState = ReadState.Read);

            this.filter.businessLine.readState = ReadState.Reading;
            this._definitionService.getBusinessLinesAsLookup()
                .subscribe((response: LookupResponse) => this.filter.businessLine.values = response.values,
                (error: any) => {
                    this.filter.businessLine.readState = ReadState.Error;
                    this.errorMessage = error;
                },
                () => this.filter.businessLine.readState = ReadState.Read);

            this.filter.segment.readState = ReadState.Reading;
            this._definitionService.getSegmentsAsLookup()
                .subscribe((response: LookupResponse) => this.filter.segment.values = response.values,
                (error: any) => {
                    this.filter.segment.readState = ReadState.Error;
                    this.errorMessage = error;
                },
                () => this.filter.segment.readState = ReadState.Read);

            this.filter.subSegment.readState = ReadState.Reading;
            this._definitionService.getSubSegmentsAsLookup()
                .subscribe((response: LookupResponse) => this.filter.subSegment.values = response.values,
                (error: any) => {
                    this.filter.subSegment.readState = ReadState.Error;
                    this.errorMessage = error;
                },
                () => this.filter.subSegment.readState = ReadState.Read);

            this.filter.supplierType.readState = ReadState.Reading;
            this._definitionService.getSupplierTypesAsLookup()
                .subscribe((response: LookupResponse) => this.filter.supplierType.values = response.values,
                (error: any) => {
                    this.filter.supplierType.readState = ReadState.Error;
                    this.errorMessage = error;
                },
                () => this.filter.supplierType.readState = ReadState.Read);
        }
    }

    export()
    {
        this.gridService.export(ExportFormat.ExcelCsvSemiColumn);
    }

    clearFilters(): void {
        this.filter.supplierCode = "";
        this.filter.supplierName = "";

        this.filter.supplierType.selectedValue = new Lookup();
        this.filter.segment.selectedValue = new Lookup();
        this.filter.subSegment.selectedValue = new Lookup();
        this.filter.businessLine.selectedValue = new Lookup();
        this.filter.ageSexGroup.selectedValue = new Lookup();
        this.filter.suppliersWithNoUsers = false;
        this.filter.pageNumber = 0;
        this.filter.pageSize = 0;              
        this.gridService.data = null;
        localStorage.removeItem("theKey");
    }

    private createRequest = (): GetSuppliersFilteredRequest => {
        let request: GetSuppliersFilteredRequest = new GetSuppliersFilteredRequest();
        request.name = this.filter.supplierName;
        request.code = this.filter.supplierCode;

        request.segment = this.filter.segment.selectedValue.Ref;

        if (this.filter.subSegment.selectedValue.Ref > 0)
            request.subSegments = [this.filter.subSegment.selectedValue.Ref];

        if (this.filter.businessLine.selectedValue.Ref > 0)
            request.businessLines = [this.filter.businessLine.selectedValue.Ref];

        if (this.filter.ageSexGroup.selectedValue.Ref > 0)
            request.ageSexGroups = [this.filter.ageSexGroup.selectedValue.Ref];

        if (this.filter.supplierType.selectedValue.Ref > 0)
            request.supplierType = this.filter.supplierType.selectedValue.Ref;

        request.pageNumber = this.filter.pageNumber;
        request.pageSize = this.pageSize;

        request.hasNoUsers = this.filter.suppliersWithNoUsers;

        return request;
    };

    private lastRequest: GetSuppliersFilteredRequest;

    getSuppliers(): void {
        this.filter.pageNumber = 1;
        this.getSuppliersX();
    }

    private getSuppliersX(): void {
        this.lastRequest = this.createRequest();

        this.gettingSuppiers = true;
        //this.dataSource.getData(this.lastRequest.pageNumber, this.pageSize);
        this.getSuppliersData(this.lastRequest.pageNumber, this.pageSize);

    }

    selectFromGrid(evt: any): void {
        //debugger;
        ////evt.setValue(() => { evt.result.supplierCode += "_osm"; });
        //evt.result.supplierCode += "_osm";
        //evt.grid.autoRefreshData = true;
        //return;
        this._router.navigate(["Home/SupplierDetail", (<Supplier>evt.result).supplierCode]);
    }

    private getSuppliersData(page: number, pageSize: number):void
    {
        this.filter.pageNumber = page;
        this.filter.pageSize = pageSize;
        localStorage.setItem("theKey", JSON.stringify(this.filter));
        let request = this.lastRequest;
        request.pageNumber = page;
        request.pageSize = pageSize;
        this._supplierService.getSuppliersFitered(request)
            .map((response: GetSuppliersFilteredResponse) => new PagedList<Supplier>(response.values, page, response.count))
            .subscribe((d: PagedList<any>) => {
                debugger;
                this.gettingSuppiers = false;
                if (d!.length === 0) {                    
                    this.gridService.data = null;
                    this.errorMessage = this.msgNoRecordsFound.value;
                } else {                    
                    this.gridService.data = d;
                }
                return d;
            },
            (error: any) => {
                let errMsg = (error.message) ? error.message :
                    error.status ? `${error.status} - ${error.statusText}` : "Server error";

                this.errorMessage = this.msgErrorReadingSuppliers + "\n" + errMsg;
                this.logger.error(this.errorMessage);
                this.gettingSuppiers = false;
            });
    }
  
}


class SupplierListFilter {
    supplierCode: string;
    supplierName: string;

    supplierTypesSearch: string;
    segmentsSearch: string;
    subSegmentsSearch: string;
    businessLinesSearch: string;
    ageSexGroupsSearch: string;

    supplierType: LookupList;
    segment: LookupList;
    subSegment: LookupList;
    businessLine: LookupList;
    ageSexGroup: LookupList;

    pageNumber: number;
    pageSize: number;

    constructor() {
        this.supplierType = new LookupList();
        this.segment = new LookupList();
        this.subSegment = new LookupList();
        this.businessLine = new LookupList();
        this.ageSexGroup = new LookupList();
        this.supplierName = "";
        this.supplierCode = "";
    }

    suppliersWithNoUsers: boolean;

    get supplierTypeIsSelected(): boolean {
        return this.supplierType && this.supplierType.selectedValue && this.supplierType.selectedValue.Ref && this.supplierType.selectedValue.Ref != 0;
    }
    get supplierTypeIsService(): boolean {
        var ok = this.supplierType && this.supplierType.selectedValue && this.supplierType.selectedValue.Ref && this.supplierType.selectedValue.Ref == 1;
        return ok;
    }
}

