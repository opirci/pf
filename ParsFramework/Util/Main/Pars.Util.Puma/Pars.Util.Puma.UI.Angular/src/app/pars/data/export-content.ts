import { TableExporter } from "./table-exporter";

export class ExportContent {
    content: string;
    fileExtension: string;
    header: string;
    exporter: TableExporter;
}