import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, Params } from "@angular/router";
import { NotifyMessage, NotifyMessageType } from "../common/notify-message.component";
import { ComponentOptions } from '../settings/componentOptions';
import { ColumnInfo, LcwGridService, LcwGridComponent } from "../common/lcw-grid/lcw-grid.component";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { MessageText } from "../pars/core";
import { DataTable, Column, Row } from "../pars/data";
import { WebApiObservableService } from "../pars/service";
import { GetLocalesByRowRequest, GetLocalesByRowResponse, DataLocalization, SaveLocalesRequest, SaveLocalesResponse } from './contracts';

@Component({
    moduleId: module.id,
    selector: 'localize-row',
    templateUrl: 'LocalizeRow.component.html',
    providers: [NotifyMessage, WebApiObservableService]
})

export class LocalizeRowComponent {
    @ViewChild("msgWarningTitle") msgWarningTitle: MessageText;
    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
    @ViewChild("msgErrorOccured") msgErrorOccured: MessageText;

    @ViewChild("msgSuccessHeader") msgSuccessHeader: MessageText;
    @ViewChild("msgSuccessContent") msgSuccessContent: MessageText;
    @ViewChild("msgErrorHeader") msgErrorHeader: MessageText;
    @ViewChild("msgErrorContent") msgErrorContent: MessageText;

    notifyOptions = ComponentOptions.NotifyMessageOptions;

    objectRef: number;
    objectGuid: string;
    languages: number[];

    datalocalization: DataLocalization;
    
    private apiUrl: string = WebApiEndPoints.apiServer;  

    gridService: LcwGridService;    

    constructor(
        private webApiObservableService: WebApiObservableService,
        private notifyService: NotifyMessage,
        private route: ActivatedRoute) {
        this.gridService = LcwGridService.create();
        this.load();
    }

    public load() {
        debugger;
        this.objectRef = this.route.snapshot.params['objRef'];
        this.objectGuid = this.route.snapshot.params['objId'];
        let languageRefs: string = this.route.snapshot.params['lngRef'];
        this.languages = languageRefs.split('-').map<number>(x => { return parseInt(x); });

        var request = new GetLocalesByRowRequest();
        request.guid = this.objectGuid;
        request.objectRef = this.objectRef;
        request.languages = this.languages;

        this.webApiObservableService
            .putAs<GetLocalesByRowResponse>(this.apiUrl + "dataLocalizer/getLocalesByRow", request)
            .subscribe((response: GetLocalesByRowResponse) => {
                debugger;
                if (!response.value.localeTable) {
                    this.notifyService.createMessage(NotifyMessageType.Bare,
                        this.msgWarningTitle,
                        this.msgNoRecordsFound);
                } else {
                    this.datalocalization = response.value;
                    this.gridService.data = new DataTable(response.value.localeTable.columns,
                        response.value.localeTable.rows,
                        1);
                }
            },
            (error: any) => {
                debugger;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgWarningTitle,
                    this.msgNoRecordsFound);
            });
    }

    public save() {
        debugger;
        var request = new SaveLocalesRequest();
        request.value = this.datalocalization;
        //request.value.serverName = this.datalocalization.serverName;
        //request.value.databaseName = this.datalocalization.databaseName;
        //request.value.tableName = this.datalocalization.tableName;
        //request.value.columnName = this.datalocalization.columnName;
        var table = <DataTable>this.gridService.getCurrentValues();
        request.value.localeTable = this.filterDataTable(table);

        this.webApiObservableService
            .putAs<SaveLocalesResponse>(this.apiUrl + "dataLocalizer/SaveLocales", request)
            .subscribe((response: SaveLocalesResponse) => {
                debugger;
                if (response.isSuccess) {
                    this.notifyService.createMessage(NotifyMessageType.Success,
                        this.msgSuccessHeader,
                        this.msgSuccessContent);
                } else {
                    this.notifyService.createMessage(NotifyMessageType.Error,
                        this.msgErrorHeader,
                        this.msgErrorContent);
                }
            },
            (error: any) => {
                debugger;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErrorHeader,
                    this.msgErrorContent);
            });
    }

    filterDataTable(table: DataTable): DataTable {
        let dataColumns: Column[] = [];
        let dataColumn: Column;
        let dataRows: Row[] = [];
        let dataRow: Row;


        table.columns.forEach(c => {
            dataColumn = new Column();
            dataColumn.name = c.name;
            dataColumn.type = c.type ? c.type : "System.String";
            dataColumns.push(dataColumn);
        });

        table.rows.filter(x => x.isUpdated).forEach(r => {
            dataRow = new Row();
            dataRow.values = r.values;
            dataRows.push(dataRow);
        });

        var updatedTable = new DataTable(dataColumns, dataRows, 1);
        return updatedTable;
    }
}