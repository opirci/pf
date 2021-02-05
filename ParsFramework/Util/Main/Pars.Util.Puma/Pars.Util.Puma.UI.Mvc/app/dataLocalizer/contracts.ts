import { LookupResponse, ResponseBase, RequestBase } from "../pars/service";
import { DataTable } from "../pars/data";

export class DataLocalization {
    serverName: string;
    databaseName: string;
    tableName: string;
    columnName: string;
    localeTable: DataTable;
}

export class GetDatabasesRequest extends RequestBase {
    serverRef: number;
}

export class GetTablesRequest extends RequestBase {
    databaseRef: number;
}

export class GetTableColumnRequest extends RequestBase {
    tableRef: number;
}

export class GetLocalesByTableRequest {
    value: DataLocalization;
    languages: number[];
}

export class GetLocalesByTableResponse extends ResponseBase {
    value: DataLocalization;
}

export class GetLocalesByRowRequest extends RequestBase {
    objectRef: number;
    guid: string;
    languages: number[];
}

export class GetLocalesByRowResponse extends ResponseBase {
    value: DataLocalization;
}

export class SaveLocalesRequest extends RequestBase {
    value: DataLocalization;
}

export class SaveLocalesResponse extends ResponseBase {
    isSuccess: boolean;
}