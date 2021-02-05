
import { ITableFormatter } from './table-exporter';

export interface IHtmlTableFormatterSettings {
    headerStyle?: string,
    cellStyle?: string,
    headerClass?: string,
    cellClass?: string;
}

export class HtmlTableFormatter {

    private firstRow = true;
    private secondRow = false;

    start(value?: any): string {
        this.firstRow = true;
        this.secondRow = false;
        return '<table><header>';
    }

    end(value?: any): string {
        return '</tbody></table>';
    }

    toRow(value: any): string {
        let val: string = '';
        if (this.firstRow) {
            this.firstRow = false;
            this.secondRow = true;
        }
        else if (this.secondRow) {
            this.secondRow = false;
            val = '</header><tbody>'
        }
        return val + '<tr>' + value + '</tr>';
    }




    toColumn(value: any, mergeCount: number): string {
        if (this.firstRow)
            return this.headerCellStr + value + '</th>';
        else
            return this.getCellStr(mergeCount) + value + '</td>';
    }

    private headerCellStr: string = null;
    private cellStr: string = null;



    private getCellStr(mergeCount: number): string {
        const colSpan = mergeCount > 0 ? " colspan='" + (mergeCount) + "' " : "";;
        
        return '<td' + colSpan + this.cellStr + '>';
    }



    private addStyleAndClass(style?: string, cclass?: string): string {
        let txt: string = '';
        if (style)
            txt += ' style=\'' + style + '\'';
        if (cclass)
            txt += ' class=\'' + cclass + '\'';
        return txt;
    }

    constructor(format?: IHtmlTableFormatterSettings) {
        let headerStyleAndClass = undefined;
        let cellStyleAndClass = undefined;
        if (format !== undefined && format !== null) {
            headerStyleAndClass = this.addStyleAndClass(format!.headerStyle, format!.headerClass);
            cellStyleAndClass = this.addStyleAndClass(format!.cellStyle, format!.cellClass);
        }
        this.headerCellStr = '<th' + (this.headerCellStr != undefined ? this.headerCellStr : '') + '>';
        //this.headerCellStr = '<th' + this!.headerCellStr + '>';
        this.cellStr = cellStyleAndClass;
        //this.cellStr = '<td' + cellStyleAndClass + '>';
    }

}
