export interface ITableFormatter {
    toRow(value: any): string;
    toColumn(value: any, merge: number): string;
    start(value?: any): string;
    end(value?: any): string;
}


export class TableExporter {

    constructor(protected formatter: ITableFormatter) {
    }

    export(data: any[][], formatter: ITableFormatter = this.formatter): string {
        const nan = "*N*A**N*";


        let fileText: string = '';
        fileText += this.formatter.start();
        let rowText: string = '';
        for (let row = 0; row < data.length; row++) {
            rowText = '';
            let mergeCount = 0;
            const rowLen: number = data[row].length;

            for (let col = 0; col < rowLen; col++) {
                let val = data[row][col];
                let nextVal = nan;
                if (col != rowLen - 1)
                    nextVal = data[row][col + 1];

                if (nextVal == undefined) {
                    mergeCount++;
                    for (col++; col != rowLen - 1; col++) {
                        nextVal = data[row][col + 1];
                        if (nextVal != undefined)
                            break;
                        mergeCount++;
                    }
                }

                rowText += formatter.toColumn(val, mergeCount + 1);
                mergeCount = 0;
            }
            fileText += formatter.toRow(rowText);
        }
        fileText += this.formatter.end();
        return fileText;
    }
}


