import { PagedList, DataTable, ExportFormat, ExportContent } from "../../pars/data";
import { BooleanViews } from "./lcw-grid.column-info";
import { ColumnInfo } from "./lcw-grid.column-info";
import { LcwCalculateSummaryArg } from "./lcw-grid-summary-context";
import { ILcwGrid, LcwGridDataRequest } from "./lcw-grid.ilcw-grid";
import { EventEmitter } from "@angular/core";


export class LcwGridService {

    static create(): LcwGridService {
        return new LcwGridService();
    }

    private _data: any;

    get data(): any { return this._data; }

    set data(value: any) {
        //debugger;
        this._data = value;
        if (this.grid) {
            this.grid.hasNewData = true;
            this.grid.changeDetectorRef.markForCheck();
            this.grid.changeDetectorRef.detectChanges();
        }

    }

    get hasData(): boolean {
        return this._data && ((this._data.length && this._data.length > 0) || (this._data.rows && this._data.rows.length > 0));
    }

    private grid: ILcwGrid;

    init(grid: ILcwGrid): void {
        this.grid = grid;        
        this.grid.onSelect.subscribe(r => this.onSelect.emit(r));
        this.grid.onDataRequest.subscribe(r => this.onDataRequest.emit(r));
        if (this.grid.onCalculateSummary != null) //TODO: CHECK the model
            this.onCalculateSummary = this.grid.onCalculateSummary;
    }
    
    getCurrentValues<T extends DataTable | PagedList<T>>(): DataTable | PagedList<T> {
        return this.grid.getData();
    }    

    onCalculateSummary: (arg: LcwCalculateSummaryArg) => any;

    exportTo(format: ExportFormat, fileName?: string): void {
        this.grid.exportTo(format, fileName);
    }
    export(format: ExportFormat): ExportContent {
        return this.grid.export(format);
    }

    get exportInProgress(): boolean {
        return this!.grid!.exportInProgress;
    }

    // get selectText(): string {
    //     return this.grid.selectText;
    // }

    // set selectText(value: string) {
    //     this.grid.selectText = value;
    // }

    // previousText: string;
    // nextText: string;
    // gridTitle: string;
    // data: any;
    // pageSize: number;
    // enablePaging: boolean;
    // pagingOnTop: boolean;
    // enableSearchBar: boolean;
    // enableSelectColumn: boolean;
    // showHeader: boolean;
    // showLineNumbers: boolean;
    // autoGenerateColumns: boolean;
    // isEditable: boolean;
    // excelExport: boolean;
    // exportAs: ExportFormat;
    // exportHiddenColumns: boolean;
    // columnHeaderStyle: string;
    // columnStyle: string;
    // exportButtonStyle: string;
    // exportButtonClass: string;
    // booleanView: BooleanViews;

    onDataRequest: EventEmitter<LcwGridDataRequest> = new EventEmitter<LcwGridDataRequest>();
    onSelect: EventEmitter<any> = new EventEmitter<any>();    
}