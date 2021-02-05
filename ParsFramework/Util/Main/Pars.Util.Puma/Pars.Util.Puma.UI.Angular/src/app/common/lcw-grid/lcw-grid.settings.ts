import { ExportFormat } from "../../pars/data";
import { BooleanViews } from "./lcw-grid.column-info";

export interface ILcwGridSettings {
    pageSize: number;
    pagingOnTop: boolean;
    showLineNumbers: boolean;
    excelExport: boolean;
    exportAs: ExportFormat;
    csvSeparator: string;
    booleanView: BooleanViews;
    horizontalScroll: boolean;
    verticalScroll: boolean;
    verticalScrollRowsToShow: number;
}

export class LcwGridSettings implements ILcwGridSettings {
    static readonly storeKey: string = 'LcwGridSettings';

    // enablePaging: boolean = false;
    pageSize: number = 20;
    pagingOnTop: boolean = false;

    // enableSearchBar: boolean = false;

    // showHeader: boolean = true;

    showLineNumbers: boolean = false;

    excelExport: boolean = false;
    exportAs: ExportFormat = ExportFormat.ExcelCsvComma;
    csvSeparator: string;

    booleanView: BooleanViews;

    horizontalScroll: boolean = true;
    verticalScroll: boolean = false;
    verticalScrollRowsToShow: number = 20;

    static loadFromLocalStore(): LcwGridSettings {
        return JSON.parse(localStorage.getItem(LcwGridSettings.storeKey)) as LcwGridSettings;
    }

    saveToLocalStore(): void {
        localStorage.setItem(LcwGridSettings.storeKey, JSON.stringify(this));
    }

    setTo(definedSettings: ILcwGridSettings): void {
        definedSettings.booleanView = this.booleanView;
        // definedSettings.excelExport = this.excelExport;
        definedSettings.exportAs = this.exportAs;
        definedSettings.horizontalScroll = this.horizontalScroll;
        definedSettings.pageSize = this.pageSize;
        definedSettings.pagingOnTop = this.pagingOnTop;
        definedSettings.showLineNumbers = this.showLineNumbers;
        definedSettings.verticalScroll = this.verticalScroll;
        definedSettings.verticalScrollRowsToShow = this.verticalScrollRowsToShow;
    }

    getFrom(definedSettings: ILcwGridSettings): void {

        this.booleanView = definedSettings.booleanView;
        // this.excelExport = definedSettings.excelExport;
        this.exportAs = definedSettings.exportAs;
        this.horizontalScroll = definedSettings.horizontalScroll;
        this.pageSize = definedSettings.pageSize;
        this.pagingOnTop = definedSettings.pagingOnTop;
        this.showLineNumbers = definedSettings.showLineNumbers;
        this.verticalScroll = definedSettings.verticalScroll;
        this.verticalScrollRowsToShow = definedSettings.verticalScrollRowsToShow;
    }
}
