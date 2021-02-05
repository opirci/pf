import { EventEmitter, ChangeDetectorRef } from '@angular/core';
import { LcwCalculateSummaryArg } from "./lcw-grid-summary-context";
import { PagedList, DataTable, ExportFormat, ExportContent } from "../../pars/data";

export interface LcwGridDataRequest {
    pageNumber?: number;
    pageSize?: number;
    searchText?: string;
    searchMember?: string;
}

export interface ILcwGrid {
    getData(): DataTable | PagedList<any>;
    export(format: ExportFormat): ExportContent;
    exportTo(format?: ExportFormat, filename?: string): void
    exportInProgress: boolean;
    data(): any;
    changeDetectorRef: ChangeDetectorRef;
    hasNewData: boolean;

    //onCalculateSummary: EventEmitter<LcwCalculateSummaryArg>;
    onCalculateSummary: (arg: LcwCalculateSummaryArg) => any;
    onSelect: EventEmitter<any>;
    onDataRequest: EventEmitter<LcwGridDataRequest>

    //gotoPage(page: number): void;
    //gotoNextPage(): void;
    //gotoPreviousPage(): void;
    //gotoFirstPage(): void;
    //gotoLastPage(): void;
}
