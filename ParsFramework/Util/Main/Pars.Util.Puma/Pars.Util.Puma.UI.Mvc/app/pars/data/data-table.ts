
import { PagedList, IPaging } from '../data'

export class DataTable implements IPaging {
    columns: Column[] = [];
    rows: PagedList<Row>;
    totalCount: number;
    pageNumber: number;

    constructor(columns: Column[], array: Row[] | any[], pageNumber: number, totalCount: number = 0) {
        this.columns = columns;
        this.pageNumber = pageNumber;
        this.totalCount = totalCount;
        this.rows = new PagedList<Row>(this.toRows(array), pageNumber, totalCount);
    }


    toRows(array: any[]): Row[] {
        if (!array)
            return null;
        if (array.length === 0)
            return [];
        if (array[0] instanceof Row)
            return array;

        let ret: Row[] = [];
        let rw: Row;
        let rwx: Row;
        for (var item of array) {
            rwx = item as Row;
            rw = new Row();
            rw.isUpdated = rwx.isUpdated;
            rw.values = rwx.values;
            ret.push(rw);
        }
        return ret;
    }
}

export class Row {
    isUpdated: boolean = false;
    values: any[] = [];

    static markRowsIfModified(array: Row[], currentArray: Row[]): void {
        if (array == null || currentArray == null)
            return;
        if (array.length !== currentArray.length)
            return;

        for (let i = 0; i < array.length; i++) {
            if (!this.compare(array[i], currentArray[i])) {
                currentArray[i].isUpdated = true;
            }

        }
    }

    static compare(obj1: Row, obj2: Row): boolean {
        if (!(obj1 && obj2 && obj1.values && obj2.values && obj1.values.length === obj2.values.length))
            return false;

        let equals: boolean = true;
        for (let i = 0; i < obj1.values.length; i++) {
            if ((obj1.values[i] != obj2.values[i])) {
                equals = false;
                break;
            }

        }
        return equals;
    }
}

export class Column {
    name: string;
    type: string;
}

