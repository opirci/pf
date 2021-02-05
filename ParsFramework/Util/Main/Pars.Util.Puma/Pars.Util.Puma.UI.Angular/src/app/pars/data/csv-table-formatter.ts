
import { ITableFormatter } from './table-exporter';

export interface ICsvTableFormatterSettings {
    separator?: string;
    separatorReplace?: string;
}

export class CsvTableFormatter implements ITableFormatter {
    start(value?: any): string {
        return '';
    }
    end(value?: any): string {
        return '';
    }

    toRow(value: any): string {
        return value + '\n';
    }
    toColumn(value: any, mergeCount: number): string {
        return this.clearSeparators(value) + this.separator.repeat(mergeCount);
    }

    private clearSeparators(value: any): string {
        return value === null ? '' : String(value).replace(this.separator, this.separatorReplace);
    }

    readonly separator: string;
    readonly separatorReplace: string;

    constructor(format?: ICsvTableFormatterSettings) {
        if (format) {
            this.separator = format.separator ? format.separator : ',';
            this.separatorReplace = format.separatorReplace ? format.separatorReplace : ' ';

        }
    }
}
