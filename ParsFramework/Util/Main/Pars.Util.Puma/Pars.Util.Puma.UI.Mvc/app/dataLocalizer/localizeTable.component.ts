import { Component, Output, EventEmitter, ViewChild } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { ColumnInfo, LcwGridComponent, LcwGridService, LcwGridDataRequest } from "../common/lcw-grid/lcw-grid.component";
import { GetLocalesByTableRequest, GetLocalesByTableResponse, DataLocalization, SaveLocalesRequest, SaveLocalesResponse } from './contracts';
import { Headers, Http, RequestOptions } from '@angular/http';
import { WebApiObservableService } from "../pars/service";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { SelectionComponent } from "./selection.component";
export { SelectionComponent } from "./selection.component";
import { DataTable, Column, Row } from "../pars/data";
import { NotifyMessage, NotifyMessageType } from "../common/notify-message.component";
import { MessageText } from "../pars/core";
import { ComponentOptions } from '../settings/componentOptions';

import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";


@Component({
    moduleId: module.id,
    selector: 'localize-table',
    templateUrl: 'LocalizeTable.component.html',
    providers: [NotifyMessage, WebApiObservableService]
})

export class LocalizeTableComponent {
    @ViewChild("msgWarningTitle") msgWarningTitle: MessageText;
    @ViewChild("msgNoRecordsFound") msgNoRecordsFound: MessageText;
    @ViewChild("msgErrorOccured") msgErrorOccured: MessageText;

    @ViewChild("msgSuccessHeader") msgSuccessHeader: MessageText;
    @ViewChild("msgSuccessContent") msgSuccessContent: MessageText;
    @ViewChild("msgErrorHeader") msgErrorHeader: MessageText;
    @ViewChild("msgErrorContent") msgErrorContent: MessageText;

    notifyOptions = ComponentOptions.NotifyMessageOptions;

    //localesTable: DataTable;
    //dataSource: XGridDataSource;
    private apiUrl: string = WebApiEndPoints.apiServer;
    isExport: boolean = false;

    @ViewChild("grid1")
    grid1: LcwGridComponent;
    gridService: LcwGridService;

    constructor(
        private webApiObservableService: WebApiObservableService
        , private notifyService: NotifyMessage, private http: Http
    ) {
        this.gridService = LcwGridService.create();
    }

    save(result: any): void {
        debugger;
        var request = new SaveLocalesRequest();
        var table: DataTable = <DataTable>this.grid1.getData();

        var datalocalizationInfo = new DataLocalization();
        datalocalizationInfo.serverName = this.getTextFromSelect(result.server);
        datalocalizationInfo.databaseName = this.getTextFromSelect(result.database);
        datalocalizationInfo.tableName = this.getTextFromSelect(result.table);
        datalocalizationInfo.columnName = this.getTextFromSelect(result.column);
        request.value = datalocalizationInfo;
        request.value.localeTable = this.isExport ? table : this.filterDataTable(table);

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
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgErrorHeader,
                    this.msgErrorContent);
            });

    }

    public import(event) {
        debugger;

    }

    filterDataTable(table: DataTable): DataTable {
        let dataColumns: Column[] = [];
        let dataColumn: Column;
        let dataRows: Row[] = [];
        let dataRow: Row;


        table.columns.forEach(c => {
            dataColumn = new Column();
            dataColumn.name = c.name;
            dataColumn.type = c.type;
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

    //export(): void {
    //    this.notifyService.createMessage(NotifyMessageType.Info, this.msgWarningTitle, "Export clicked");
    //}


    load(result: any, isExport: boolean = false): void {
        debugger;
        var request = new GetLocalesByTableRequest();
        var datalocalizationInfo = new DataLocalization();
        datalocalizationInfo.serverName = this.getTextFromSelect(result.server);
        datalocalizationInfo.databaseName = this.getTextFromSelect(result.database);
        datalocalizationInfo.tableName = this.getTextFromSelect(result.table);
        datalocalizationInfo.columnName = this.getTextFromSelect(result.column);

        request.value = datalocalizationInfo;
        request.languages = this.getValuesFromSelect(result.languages);

        if (result.dataTable != null) {
            //this.localesTable = new DataTable(result.dataTable.columns, result.dataTable.rows, 1);
            this.gridService.data = new DataTable(result.dataTable.columns, result.dataTable.rows, 1);

            this.isExport = true;

            return;
        }

        this.isExport = false;

        this.webApiObservableService
            .putAs<GetLocalesByTableResponse>(this.apiUrl + "dataLocalizer/getLocalesByTable", request)
            .subscribe((response: GetLocalesByTableResponse) => {
                debugger;
                if (!response.value.localeTable) {
                    this.notifyService.createMessage(NotifyMessageType.Bare,
                        this.msgWarningTitle,
                        this.msgNoRecordsFound);
                } else {
                    //this.localesTable = new DataTable(response.value.localeTable.columns, response.value.localeTable.rows, 1);
                    this.gridService.data = new DataTable(response.value.localeTable.columns, response.value.localeTable.rows, 1);
                }
            },
            (error: any) => {
                debugger;
                this.notifyService.createMessage(NotifyMessageType.Error,
                    this.msgWarningTitle,
                    this.msgNoRecordsFound);
            });
    }

    getTextFromSelect(source: any): string {
        return source.selectedOptions["0"].label;
    }

    getValuesFromSelect(source: any): number[] {
        debugger;
        let languages: number[] = [];

        for (var i = 0; i < source.selectedOptions.length; i++) {
            languages.push(source.selectedOptions[i].value);
        }

        return languages;
    }
}