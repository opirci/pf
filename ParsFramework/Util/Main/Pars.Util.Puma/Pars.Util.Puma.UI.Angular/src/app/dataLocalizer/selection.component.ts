import { Component, Injectable, AfterViewInit, Output, Input, EventEmitter, ViewChildren, ViewChild, ElementRef, Renderer } from '@angular/core';
import { Headers, Http, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { MessageText } from "../pars/core";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { LookupResponse, WebApiObservableService } from "../pars/service";
import { DefinitionService } from "../services/definition.service";
import { NotifyMessage, NotifyMessageType } from "../common/notify-message.component";
import { GetDatabasesRequest, GetTablesRequest, GetTableColumnRequest, GetLocalesByTableResponse } from "./contracts";
import { ComponentOptions } from '../settings/componentOptions';
import 'rxjs/add/operator/toPromise';
import * as $ from "jquery";
import { DataTable } from "../pars/data";


@Component({
    moduleId: module.id,
    selector: 'selection',
    templateUrl: 'selection.component.html',
    styleUrls: ['selection.component.css'],
    providers: [NotifyMessage, DefinitionService, WebApiObservableService],
})
export class SelectionComponent implements AfterViewInit {

    @ViewChild("msgSuccessHeader") msgSuccessHeader: MessageText;
    @ViewChild("msgSuccessContent") msgSuccessContent: MessageText;
    @ViewChild("msgErroHeader") msgErroHeader: MessageText;
    @ViewChild("msgErrorContent") msgErrorContent: MessageText;

    servers: any[];
    databases: any[];
    tables: any[];
    columns: any[];
    languages: any[];

    selectedServer: string = "-1";
    selectedDatabase: string = "-1";
    selectedTable: string = "-1";
    selectedColumn: string = "-1";
    selectedLanguages: number[];


    notifyOptions = ComponentOptions.NotifyMessageOptions;

    @Output() loadFunc: EventEmitter<any> = new EventEmitter<any>();

    @Output() onSave: EventEmitter<any> = new EventEmitter<any>();
    @Input() canSave: boolean = false;

    @Output() onExport: EventEmitter<any> = new EventEmitter<any>();
    @Input() canExport: boolean = false;

    @Output() onImport: EventEmitter<any> = new EventEmitter<any>();
    @Input() canImport: boolean = false;



    private apiUrl: string = WebApiEndPoints.apiServer;

    isReadingServers: boolean = false;
    isReadingDatabases: boolean = false;
    isReadingTables: boolean = false;
    isReadingColumns: boolean = false;
    isReadingLanguages: boolean = false;

    constructor(
        private _definitionService: DefinitionService,
        private notifyService: NotifyMessage,
        private http: Http,
        private elt: ElementRef,
        private renderer: Renderer
    ) {
        this.getServers();
        this.getLanguages();
    }

    public getServers() {
        this.isReadingServers = true;
        this._definitionService.getServers()
            .subscribe((response: LookupResponse) => {
                this.servers = response.values;
                this.databases = null;
                this.tables = null;
                this.columns = null;
                this.isReadingServers = false;
            },
            (error: any) => {
                this.isReadingServers = false;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErroHeader,
                    this.msgErrorContent + " : " + error.message);
            });
    }

    public getDatabases(serverRef: number) {
        this.isReadingDatabases = true;
        var request = new GetDatabasesRequest();
        request.serverRef = serverRef;
        this._definitionService.getDatabases(request)
            .subscribe((response: LookupResponse) => {
                this.databases = response.values;
                this.tables = null;
                this.columns = null;
                this.isReadingDatabases = false;
            },
            (error: any) => {
                this.isReadingDatabases = false;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErroHeader,
                    this.msgErrorContent + " : " + error.message);
            });
    }

    public getTables(databaseRef: number) {
        this.isReadingTables = true;
        var request = new GetTablesRequest();
        request.databaseRef = databaseRef;
        this._definitionService.getTables(request)
            .subscribe((response: LookupResponse) => {
                this.tables = response.values;
                this.columns = null;
                this.isReadingTables = false;
            },
            (error: any) => {
                this.isReadingTables = false;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErroHeader,
                    this.msgErrorContent + " : " + error.message);
            });
    }

    public getColumns(tableRef: number) {
        this.isReadingColumns = true;
        var request = new GetTableColumnRequest();
        request.tableRef = tableRef;
        this._definitionService.getColumns(request)
            .subscribe((response: LookupResponse) => {
                this.columns = response.values;
                this.isReadingColumns = false;
            },
            (error: any) => {
                this.isReadingColumns = false;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErroHeader,
                    this.msgErrorContent + " : " + error.message);
            });
    }

    public getLanguages() {
        this.isReadingLanguages = true;
        this._definitionService.getLanguages()
            .subscribe((response: LookupResponse) => {
                this.languages = response.values;
                this.isReadingLanguages = false;
            },
            (error: any) => {
                this.isReadingLanguages = false;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErroHeader,
                    this.msgErrorContent + " : " + error.message);
            });
    }

    public ngAfterViewInit() {
        debugger;
        eval('$(".select2").select2();');
        var element = this.elt.nativeElement.childNodes[0];
    }

    select(server: any, database: any, table: any, column: any, languages: any, dataTable: DataTable = null): void {
        debugger;
        this.loadFunc.emit(
            {
                server, database, table, column, languages, dataTable
            }
        );
    }

    save(server: any, database: any, table: any, column: any): void {
        debugger;
        this.onSave.emit(
            {
                server,
                database,
                table,
                column
            });
    }

    public import(event, server: any, database: any, table: any, column: any, language: any): void {
        debugger;
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();
            formData.append('uploadFile', file, file.name);

            let headers = new Headers();
            headers.append('enctype', 'multipart/form-data');
            headers.append('Accept', 'application/json');
            let options = new RequestOptions({ headers: headers });

            let dataTable: DataTable;

            this.http.post(`${this.apiUrl + "dataLocalizer/ImportTable"}`, formData, options)
                .map(res => res.json() as GetLocalesByTableResponse)
                .catch(error => Observable.throw(error))
                .subscribe((response: GetLocalesByTableResponse) => {
                    debugger;
                    if (!response.value.localeTable) {
                        debugger;
                        //this.notifyService.createMessage(NotifyMessageType.Bare,
                        //    this.msgWarningTitle,
                        //    this.msgNoRecordsFound);
                    } else {
                        debugger;
                        response.value.localeTable.columns.forEach(c => { c.name = c.name.replace(/ /g, ''); });
                        dataTable = new DataTable(response.value.localeTable.columns,
                            response.value.localeTable.rows,
                            1);

                        //this.select(this.selectedServer, this.selectedDatabase, this.selectedTable, this.selectedColumn, this.selectedLanguages, dataTable);
                        this.select(server, database, table, column, language, dataTable);
                    }
                },
                (error: any) => {
                    debugger;
                    this.notifyService.createMessage(NotifyMessageType.Error,
                        this.msgErroHeader,
                        this.msgErrorContent);
                });
        }
    }

    popup() {
        debugger;
        //var pop = this.elt.nativeElement.getElementsByTagName("popover-content")[0];
    }

    export(): void {
        this.onExport.emit();
    }
}

