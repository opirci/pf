
import { Injectable } from '@angular/core';
import { TableExporter, ITableFormatter } from './table-exporter';
import { HtmlTableFormatter, IHtmlTableFormatterSettings } from './html-table-formatter';
import { CsvTableFormatter } from './csv-table-formatter';
import { ExportContent } from './export-content';

export { ExportContent } from './export-content';


@Injectable()
export class TableExportService {
    export(data: any[][], format: ExportFormat, parameter?: any): ExportContent {
        const exportContent = new ExportContent();
        let formatter: ITableFormatter;

        switch (format) {
            case ExportFormat.ExcelXlsx:
                let settings: IHtmlTableFormatterSettings;
                if (parameter) {
                    settings = parameter as IHtmlTableFormatterSettings;
                }
                formatter = new HtmlTableFormatter(settings ? settings : undefined);
                exportContent.fileExtension = 'xls';
                exportContent.header = { type: 'application/vnd.ms-excel' };
                break;
            case ExportFormat.ExcelCsvComma:
            case ExportFormat.ExcelCsvSemiColumn:
                const sep = format == ExportFormat.ExcelCsvComma ? ',' : ';';
                formatter = new CsvTableFormatter({ separator: sep });
                exportContent.fileExtension = 'csv';
                exportContent.header = { type: 'text/csv;charset=UTF-8' };
                break;
            default:
                throw Error(`Undefined export type ${format}`);
        }

        exportContent.content = '\uFEFF' + new TableExporter(formatter).export(data);
        return exportContent;
    }
}

export enum ExportFormat {
    ExcelXlsx,
    ExcelCsvComma,
    ExcelCsvSemiColumn,
}