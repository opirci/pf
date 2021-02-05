import { ExportFormat } from "../pars/data";
import { BooleanViews } from "./xgrid.column-info";

export interface IXGridSettings {
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

export class XGridSettings implements IXGridSettings {
    static readonly storeKey: string = 'XGridSettings';

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

    static loadFromLocalStore(): XGridSettings {
        return JSON.parse(localStorage.getItem(XGridSettings.storeKey)) as XGridSettings;
    }

    saveToLocalStore(): void {
        localStorage.setItem(XGridSettings.storeKey, JSON.stringify(this));
    }

    setTo(definedSettings: IXGridSettings): void {
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

    getFrom(definedSettings: IXGridSettings): void {

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
