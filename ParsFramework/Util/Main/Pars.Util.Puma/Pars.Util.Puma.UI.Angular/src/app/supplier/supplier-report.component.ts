import { TestService } from "../services/test.service"

import {
    Component, OnInit, ViewChild, ElementRef, Observable,
    ComponentBase, MessageText, Logger, Loggers, CoreHelper,
    LookupList, ReadState, Lookup, DataTable, Row,
    HttpService, LookupResponse, PagedList,
    NotifyMessage, NotifyMessageType, ComponentOptions, BooleanViews, ExportFormat, LcwGridComponent, LcwGridService, LcwGridDataRequest
} from "../component.imports";

import { SupplierReportService, GetSupplierReportResponse, GetSupplierReportRequest, SupplierReportLine } from "./supplier-report.service";
import { DefinitionService } from "../services/definition.service";


@Component({
    moduleId: module.id,
    selector: "supplier-report",
    providers: [NotifyMessage, SupplierReportService, HttpService, TestService],
    templateUrl: "supplier-report.component.html"
})
export class SupplierReportComponent extends ComponentBase implements OnInit {
    @ViewChild("msgYouDoNotHavePermissionToList") msgYouDoNotHavePermissionToList: MessageText;
    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
    @ViewChild("msgErrorReadingReport") msgErrorReadingReport: MessageText;
    @ViewChild("msgError") msgError: MessageText;
    @ViewChild("msgWarning") msgWarning: MessageText;


    @ViewChild("headerDiv")
    headerDiv: ElementRef
    gettingSuppierLines: boolean = false;

    gridService: LcwGridService;

    reportLines: PagedList<SupplierReportLine> | DataTable;
    BooleanViews = BooleanViews;
    //reportLines: DataTable;
    @ViewChild("grid") grid: LcwGridComponent;


    private logger: Logger;
    private merrorMessage: string;


    private get errorMessage(): string { return this.merrorMessage; };

    get canGetList(): boolean { return true; };

    private set errorMessage(value: string) {
        this.merrorMessage = value;
        this.notifyService.createMessage(NotifyMessageType.Error,
            "",
            //this.Msg_ErroHeader,
            this.errorMessage);
    };

    supplierType: LookupList = new LookupList();

    constructor(
        private testService: TestService,
        private notifyService: NotifyMessage,
        private supplierReportService: SupplierReportService,
        private definitionService: DefinitionService,

    ) {
        super();
        this.logger = new Logger(CoreHelper.nameOfInstance(this), Loggers.Console | Loggers.LocalStore);
        this.logger.suspend();

        this.gridService = LcwGridService.create();

        this.gridService.onDataRequest.subscribe((ctx: LcwGridDataRequest) =>
            this.getSuppliersReport(ctx.pageNumber)
        );

    }

    private getSuppliersReport(page: number): void {
        var request: GetSupplierReportRequest = new GetSupplierReportRequest();
        request.supplierType = this.supplierType.selectedValue.Ref;

        if (this.testMode) {
            this.testService.getSupplierReportAsDataTable(request)
                .map((response: DataTable) => this.gridService.data = new DataTable(response.columns, response.rows, response.pageNumber, response.totalCount))
                .subscribe(d => this.gettingSuppierLines = false);
        }
        else {
            this.supplierReportService.getSupplierReport(request)
                .map((response: GetSupplierReportResponse) => this.gridService.data = new PagedList<SupplierReportLine>(this.fromJson(response.values), page, response.count))
                .subscribe((d: PagedList<any>) => {
                    this.gettingSuppierLines = false;
                    if (d.length === 0) {
                        this.notifyService.createMessage(NotifyMessageType.Error, "",
                            //this.Msg_ErroHeader,
                            this.msgNoRecordsFound.value);
                    }
                    return d;
                },
                (error: any) => {
                    let errMsg = (error.message) ? error.message :
                        error.status ? `${error.status} - ${error.statusText}` : "Server error";
                    this.logger.error(errMsg);

                    this.errorMessage = this.msgErrorReadingReport.value;
                    this.logger.error("Error reading Supplier Report\n" + errMsg);
                    this.gettingSuppierLines = false;
                });
        }
    }

    ngOnInit(): void {
        this.supplierType.readState = ReadState.Reading;
        this.definitionService.getSupplierTypesAsLookup()
            .subscribe((response: LookupResponse) => {
                this.supplierType.values = response.values;
            },
            (error: any) => {
                this.supplierType.readState = ReadState.Error;
                this.errorMessage = error;
            },
            () => this.supplierType.readState = ReadState.Read);
    }

    printable: boolean = false;



    export(format: ExportFormat) {
        this.gridService.exportTo(format);
    }



    printSupplierLines(): void {
        //this.printable = !this.printable;
        //return;
        //this.printable = true;
        this.headerDiv.nativeElement.style.visibility = "hidden";
        window.print();
        this.headerDiv.nativeElement.style.visibility = "visible";
        //this.printable = false;

        ////  let printContents: string = document.getElementById('report').innerHTML;
        ////  let popupWin: Window = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
        ////  popupWin.document.open();
        ////  popupWin.document.write("<html><head>");

        ////  popupWin.document.write(this.fetchStylesheets());
        ////  //popupWin.document.write(this.fetchscriptSheets());
        ////  popupWin.document.write(`           
        ////           </head>
        ////  <body onload="window.print();window.close()">${printContents}</body>
        ////</html>`
        ////  );
        ////  popupWin.document.close();
    }

    fetchStylesheets(): string {
        var output: string = '';
        for (var i = 0; i < document.styleSheets.length; i++) {
            output = output + ' <link rel="stylesheet" type="text/css" href="' +
                window.document.styleSheets[i].href + '" /> ';
        }
        return output;
    }

    //fetchscriptSheets(): string {
    //    var output: string = '';
    //    for (var i = 0; i < document.scripts.length; i++) {
    //        output = output + ' <script type="text/javascript" src="' +
    //      window.document.scripts[i].src + '" /> ';
    //    }
    //    return output;
    //}

    getDataBack(): void {
        debugger;
        let dataTable: DataTable = this.grid.getData() as DataTable;
        debugger;
        this.testService.saveDataTable(dataTable).subscribe(
            d => d,
            error => {

            });
    }

    getSupplierLines(): void {
        this.gettingSuppierLines = true;
        this.getSuppliersReport(0);
        //this.dataSource.getData(0, 0);
    }

    //private selectFromGrid(evt: any): void {
    //    debugger;
    //    alert((<Row>evt.result));
    //}

    fromJson(supplierUsers: SupplierReportLine[]): SupplierReportLine[] {
        let arr: Array<SupplierReportLine> = [];
        for (var supItem of supplierUsers) {
            arr.push(new SupplierReportLine(supItem));
        }
        return arr;
    }

    testMode: boolean = false;

}