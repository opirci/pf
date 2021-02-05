//import { Component, OnInit, ViewChild, ElementRef, AfterContentInit } from "@angular/core";
//import { Observable } from "rxjs/Observable";
//import { Router } from "@angular/router";

////import {
////    ComponentBase, MessageText, Logger, Loggers, CoreHelper, LookupList, ReadState, Lookup, DataTable, LookupResponse, HttpService, WebApiObservableService, XHttpService,
////    XGridDataSource, IXGridGetDataDelegate, IXGridSearchDataDelegate, XGridColumnComponent, PagedList, XGridComponent, NotifyMessage, NotifyMessageType, ProgressManagerService, DataServiceExecuter
////} from "../component.imports";

//import { LookupList, ReadState, Lookup, DataTable } from "../pars/data";
//import { ComponentBase, MessageText, Logger, Loggers, CoreHelper, ProgressManagerService } from "../pars/core";
//import { LookupResponse, HttpService, WebApiObservableService, XHttpService, DataServiceExecuter } from "../pars/service";
//import { XGridDataSource, IXGridGetDataDelegate, IXGridSearchDataDelegate, XGridColumnComponent, PagedList, XGridComponent, NotifyMessage, NotifyMessageType } from "../common";


//import {
//    GetSuppliersFilteredResponse, GetSuppliersFilteredRequest, Supplier,
//    XSupplierService, XDefinitionService
//} from "./supplier-list-test.service";
//import { ComponentOptions } from '../settings/componentOptions';


//@Component({
//    moduleId: module.id,
//    selector: "supplier-list",
//    providers: [NotifyMessage, XHttpService, WebApiObservableService],
//    templateUrl: "supplier-list-test.component.html"
//})
//export class SupplierListTestComponent extends ComponentBase implements OnInit {
//    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
//    @ViewChild("msgErrorReadingSuppliers") msgErrorReadingSuppliers: MessageText;
//    @ViewChild("gridMy") gridMy: XGridComponent;



//    suppliers: PagedList<Supplier>;
//    private _errorMessage: string;

//    private get errorMessage(): string { return this._errorMessage; };
//    private set errorMessage(value: string) {
//        this._errorMessage = value;
//        this.notifyService.createMessage(NotifyMessageType.Error, "",
//            //this.Msg_ErroHeader,
//            this._errorMessage);
//    };

//    handleError(message: string, errrorString: string, title: string = null) {

//    }

//    gridColumnInit(column: XGridColumnComponent): void {
//        //debugger;
//        //column.isHidden = true;
//    }

//    private logger: Logger;
//    filter: SupplierListFilter;
//    pageSize: number = 20;
//    dataSource: XGridDataSource;
//    //private msgNoRecordsFound: string;

//    notifyOptions = ComponentOptions.NotifyMessageOptions;

//    private spinner: ProgressManagerService;
//    private executer: DataServiceExecuter<XDefinitionService>;
//    private _xdefinitionService: XDefinitionService;
//    private _xsupplierService: XSupplierService;

//    constructor(
//        private notifyService: NotifyMessage,      
//        private _router: Router,
//        private _xhttp: XHttpService,
//        private elementRef: ElementRef) {
//        super();
//        this.logger = new Logger(CoreHelper.nameOfInstance(this), Loggers.Console | Loggers.LocalStore);
//        this.logger.suspend();

//        //this.logger.log("Entered getSuppliers of SupplierListComponent");
//    }

//    gettingSuppiers: boolean = false;



//    ngOnInit(): void {
//        super.ngOnInit();
//    //ngAfterContentInit(): void {

//        this.spinner = new ProgressManagerService(this.elementRef);
//        this.executer = XDefinitionService.create(XDefinitionService, this._xhttp, this.spinner, this.notifyService);

//        //this.logger.warn(this.msgNoRecordsFound.value);
//        //this.logger.warn(this.templateText("msgNoRecordsFound"));

//        this.dataSource = new XGridDataSource(this.getDataDelegate, this.searchDataDelegate);

//        //if (this.filter == null)
//        //    this.filter = JSON.parse(localStorage.getItem("theKey"));

//        if (this.filter != null) {
//            this.getSuppliersX();
//        }
//        else {
//            this.filter = new SupplierListFilter();

//            this.executer.executeWith({}).getAgeSexGroupsAsLookup()
//                .subscribe((response: LookupResponse) => this.filter.ageSexGroup.values = response.values);

//            //this.filter.ageSexGroup.readState = ReadState.Reading;
//            //this._definitionService.getAgeSexGroupsAsLookup()
//            //    .subscribe((response: LookupResponse) => this.filter.ageSexGroup.values = response.values,
//            //    (error: any) => {
//            //        this.filter.ageSexGroup.readState = ReadState.Error;
//            //        this.errorMessage = error;
//            //    },
//            //    () => this.filter.ageSexGroup.readState = ReadState.Read);

//            this.executer.executeWith({}).getBusinessLinesAsLookup()
//                .subscribe((response: LookupResponse) => this.filter.businessLine.values = response.values);

//            //this.filter.businessLine.readState = ReadState.Reading;
//            //this._definitionService.getBusinessLinesAsLookup()
//            //    .subscribe((response: LookupResponse) => this.filter.businessLine.values = response.values,
//            //    (error: any) => {
//            //        this.filter.businessLine.readState = ReadState.Error;
//            //        this.errorMessage = error;
//            //    },
//            //    () => this.filter.businessLine.readState = ReadState.Read);

//            this.executer.executeWith({}).getSegmentsAsLookup()
//                .subscribe((response: LookupResponse) => this.filter.segment.values = response.values);

//            //this.filter.segment.readState = ReadState.Reading;
//            //this._definitionService.getSegmentsAsLookup()
//            //    .subscribe((response: LookupResponse) => this.filter.segment.values = response.values,
//            //    (error: any) => {
//            //        this.filter.segment.readState = ReadState.Error;
//            //        this.errorMessage = error;
//            //    },
//            //    () => this.filter.segment.readState = ReadState.Read);

//            this.executer.executeWith({}).getSubSegmentsAsLookup()
//                .subscribe((response: LookupResponse) => this.filter.subSegment.values = response.values);

//            //this.filter.subSegment.readState = ReadState.Reading;
//            //this._definitionService.getSubSegmentsAsLookup()
//            //    .subscribe((response: LookupResponse) => this.filter.subSegment.values = response.values,
//            //    (error: any) => {
//            //        this.filter.subSegment.readState = ReadState.Error;
//            //        this.errorMessage = error;
//            //    },
//            //    () => this.filter.subSegment.readState = ReadState.Read);


//            this.executer.executeWith({}).getSupplierTypesAsLookup()
//                .subscribe((response: LookupResponse) => this.filter.supplierType.values = response.values);

//            //this.filter.supplierType.readState = ReadState.Reading;
//            //this._definitionService.getSupplierTypesAsLookup()
//            //    .subscribe((response: LookupResponse) => this.filter.supplierType.values = response.values,
//            //    (error: any) => {
//            //        this.filter.supplierType.readState = ReadState.Error;
//            //        this.errorMessage = error;
//            //    },
//            //    () => this.filter.supplierType.readState = ReadState.Read);
//        }
//    }

//    clearFilters(): void {
//        this.filter.supplierCode = "";
//        this.filter.supplierName = "";

//        this.filter.supplierType.selectedValue = new Lookup();
//        this.filter.segment.selectedValue = new Lookup();
//        this.filter.subSegment.selectedValue = new Lookup();
//        this.filter.businessLine.selectedValue = new Lookup();
//        this.filter.ageSexGroup.selectedValue = new Lookup();
//        this.filter.suppliersWithNoUsers = false;
//        this.filter.pageNumber = 0;
//        this.filter.pageSize = 0;
//        this.suppliers = null;
//        localStorage.removeItem("theKey");
//    }

//    private createRequest = (): GetSuppliersFilteredRequest => {
//        let request: GetSuppliersFilteredRequest = new GetSuppliersFilteredRequest();
//        request.name = this.filter.supplierName;
//        request.code = this.filter.supplierCode;

//        request.segment = this.filter.segment.selectedValue.Ref;

//        if (this.filter.subSegment.selectedValue.Ref > 0)
//            request.subSegments = [this.filter.subSegment.selectedValue.Ref];

//        if (this.filter.businessLine.selectedValue.Ref > 0)
//            request.businessLines = [this.filter.businessLine.selectedValue.Ref];

//        if (this.filter.ageSexGroup.selectedValue.Ref > 0)
//            request.ageSexGroups = [this.filter.ageSexGroup.selectedValue.Ref];

//        if (this.filter.supplierType.selectedValue.Ref > 0)
//            request.supplierType = this.filter.supplierType.selectedValue.Ref;

//        request.pageNumber = this.filter.pageNumber;
//        request.pageSize = this.pageSize;

//        request.hasNoUsers = this.filter.suppliersWithNoUsers;

//        return request;
//    };

//    private lastRequest: GetSuppliersFilteredRequest;

//    getSuppliers(): void {
//        this.filter.pageNumber = 1;
//        this.getSuppliersX();
//    }

//    private getSuppliersX(): void {
//        this.lastRequest = this.createRequest();

//        this.gettingSuppiers = true;
//        this.dataSource.getData(this.lastRequest.pageNumber, this.pageSize);

//    }

//    selectFromGrid(evt: any): void {
//        //debugger;
//        ////evt.setValue(() => { evt.result.supplierCode += "_osm"; });
//        //evt.result.supplierCode += "_osm";
//        //evt.grid.autoRefreshData = true;
//        //return;
//        this._router.navigate(["Home/SupplierDetail", (<Supplier>evt.result).supplierCode]);
//    }

//    private getDataDelegate: IXGridGetDataDelegate = (page: number, pageSize: number): void => {
//        this.filter.pageNumber = page;
//        this.filter.pageSize = pageSize;
//        localStorage.setItem("theKey", JSON.stringify(this.filter));
//        let request = this.lastRequest;
//        request.pageNumber = page;
//        request.pageSize = pageSize;

//        XSupplierService.create(XSupplierService, this._xhttp, this.spinner, this.notifyService)
//            .executeWith({ errorMessage: this.msgErrorReadingSuppliers.value })
//            .getSuppliersFitered(request)
//            .map((response: GetSuppliersFilteredResponse) => new PagedList<Supplier>(response.values, page, response.count))
//            .subscribe((d: PagedList<any>) => {
//                if (d!.length === 0) {
//                    this.suppliers = null;
//                    this.errorMessage = this.msgNoRecordsFound.value;
//                } else {
//                    this.suppliers = d;
//                }
//                return d;
//            });


//        //this._supplierService.getSuppliersFitered(request)
//        //    .map((response: GetSuppliersFilteredResponse) => new PagedList<Supplier>(response.values, page, response.count))
//        //    .subscribe((d: PagedList<any>) => {
//        //        debugger;
//        //        this.gettingSuppiers = false;
//        //        if (d!.length === 0) {
//        //            this.suppliers = null;
//        //            this.errorMessage = this.msgNoRecordsFound.value;
//        //        } else {
//        //            this.suppliers = d;
//        //        }
//        //        return d;
//        //    },
//        //    (error: any) => {
//        //        let errMsg = (error.message) ? error.message :
//        //            error.status ? `${error.status} - ${error.statusText}` : "Server error";

//        //        this.errorMessage = this.msgErrorReadingSuppliers + "\n" + errMsg;
//        //        this.logger.error(this.errorMessage);
//        //        this.gettingSuppiers = false;
//        //    });
//    };

//    private searchDataDelegate: IXGridSearchDataDelegate =
//    (searchText: string, searchMember: string, page: number, pageSize: number): void => { this.getDataDelegate(page, pageSize); };

//    test(ekle: boolean): void {
//        debugger;
//        if (this!.suppliers.length > 0) {

//            debugger;
//            if (ekle) {
//                this.suppliers.push(this.suppliers[0]);
//            }
//            else {

//                this.suppliers[0].contactFirstName += " - X";
//                this.suppliers[0].supplierCode += " - X";
//            }
//            //this.gridMy.refresh();
//            //this.gridMy.setValueWithRefresh(() => {
//            //    debugger;
//            //    if (ekle) {
//            //        this.suppliers.push(this.suppliers[0]);
//            //    }
//            //    else {

//            //        this.suppliers[0].contactFirstName += " - X";
//            //        this.suppliers[0].supplierCode += " - X";
//            //    }
//            //});
//        }

//    }
//}


//class SupplierListFilter {
//    supplierCode: string;
//    supplierName: string;

//    supplierTypesSearch: string;
//    segmentsSearch: string;
//    subSegmentsSearch: string;
//    businessLinesSearch: string;
//    ageSexGroupsSearch: string;

//    supplierType: LookupList;
//    segment: LookupList;
//    subSegment: LookupList;
//    businessLine: LookupList;
//    ageSexGroup: LookupList;

//    pageNumber: number;
//    pageSize: number;

//    constructor() {
//        this.supplierType = new LookupList();
//        this.segment = new LookupList();
//        this.subSegment = new LookupList();
//        this.businessLine = new LookupList();
//        this.ageSexGroup = new LookupList();
//        this.supplierName = "";
//        this.supplierCode = "";
//    }

//    suppliersWithNoUsers: boolean;

//    get supplierTypeIsSelected(): boolean {
//        return this.supplierType && this.supplierType.selectedValue && this.supplierType.selectedValue.Ref && this.supplierType.selectedValue.Ref != 0;
//    }
//    get supplierTypeIsService(): boolean {
//        var ok = this.supplierType && this.supplierType.selectedValue && this.supplierType.selectedValue.Ref && this.supplierType.selectedValue.Ref == 1;
//        return ok;
//    }
//}

