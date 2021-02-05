
import { EventEmitter } from '@angular/core';
import { BooleanViews } from "./xgrid.column-info";
import { PagedList, DataTable, ExportFormat, ExportContent } from "../pars/data";

export interface XGridDataRequest {
    pageNumber?: number;
    pageSize?: number;
    searchText?: string;
    searchMember?: string;
}

export interface IXGrid {
    getData(): DataTable | PagedList<any>;
    export(format: ExportFormat): ExportContent;
    exportTo(format?: ExportFormat, filename?: string): void
    data: any;

    //gotoPage(page: number): void;
    //gotoNextPage(): void;
    //gotoPreviousPage(): void;
    //gotoFirstPage(): void;
    //gotoLastPage(): void;


}

export class XGridService {

    static create(): XGridService {
        return new XGridService();
    }

    data: any;

    private grid: IXGrid;

    init(grid: IXGrid): void {
        this.grid = grid;
    }

    getCurrentValues<T extends DataTable | PagedList<T>>(): DataTable | PagedList<T> {
        return this.grid.getData();
    }

    exportTo(format: ExportFormat, fileName?: string): void {
        this.grid.exportTo(format, fileName);
    }
    export(format: ExportFormat): ExportContent {
        return this.grid.export(format);
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

    dataRequest: EventEmitter<XGridDataRequest> = new EventEmitter<XGridDataRequest>();
    onSelect: EventEmitter<any> = new EventEmitter<any>();
}