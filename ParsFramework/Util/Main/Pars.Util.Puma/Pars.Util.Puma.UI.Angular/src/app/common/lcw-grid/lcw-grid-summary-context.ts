
import { ColumnInfo } from "./lcw-grid.column-info";
import { LcwGridService } from "./lcw-grid.service";

export interface LcwCalculateSummaryArg
{
    column: ColumnInfo;
    columnData: any[];
    gridService: LcwGridService;
    //dataContext: LcwGridSummaryContext;
    //calculatedValue: any;
}

export class LcwGridSummaryContext {
    columns: ColumnInfo[];
    data: any[][];

    private columnIndex(column: ColumnInfo): number {
        let i = 0;
        let found = -1;
        for (; i < this.columns.length; i++) {
            if (this.columns[i].fieldName == column.fieldName) {
                found = i;
                break;
            }
        }
        return found;
    }

    getColumnData(column: ColumnInfo): any[] {
        let index: number = this.columnIndex(column);
        if (index == -1)
            return null;
        return this.data[index];
    }

    getCellData(column: ColumnInfo, rowNumber: number): any {
        let cols = this.getColumnData(column);
        if (cols == null)
            return null;
        return cols[rowNumber];
    }
}