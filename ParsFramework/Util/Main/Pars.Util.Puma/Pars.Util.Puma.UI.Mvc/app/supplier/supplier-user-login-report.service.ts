// import {
//     Injectable, Observable, WebApiEndPoints, DateWeekRange,
//     DateRange, LookupResponse, ResponseBase, RequestBase, DomainBase, HttpService, DataTable, Verbs, ParamThru
// } from "../service.imports";

import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { DateWeekRange, DateRange, DomainBase, DataTable } from "../pars/data";
import { LookupResponse, ResponseBase, RequestBase, HttpService, Verbs, ParamThru } from "../pars/service";

import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";


@Injectable()
export class SupplierUserLoginReportService {
    constructor(
        private _httpService: HttpService) {
    }

    getSupplierUserLoginReport(getSupplierUserLoginReportRequest: GetSupplierUserLoginReportRequest):
        Observable<GetSupplierUserLoginReportResponse> {
        return this._httpService.requestAs<GetSupplierUserLoginReportResponse>(
            Verbs.Get,
            WebApiEndPoints.apiServer + "supplier/GetSupplierUserLoginReport",
            getSupplierUserLoginReportRequest,
            ParamThru.Uri);
    }
     //#region TestCode
    getDataTable(): Observable<DataTable> {
        return this._httpService.requestAs<DataTable>(
            Verbs.Get,
            WebApiEndPoints.apiServer + "supplier/GetDataTable",
            null,
            ParamThru.Body);
    }

    saveDataTable(request: DataTable): Observable<any> {
        return this._httpService.requestAs<DataTable>(
            Verbs.Put,
            WebApiEndPoints.apiServer + "supplier/SaveDataTable",
            request,
            ParamThru.Body);    
    }

    getDateWeekRange(): Observable<DateWeekRange> {
        return this._httpService.requestAs<DateWeekRange>(
            Verbs.Get,
            WebApiEndPoints.apiServer + "supplier/GetDateWeekRange",
            null,
            ParamThru.Body);
    }
    //#endregion 
    
}

export class GetSupplierUserLoginReportResponse extends ResponseBase {
    value: DataTable;
    reportType: number;
}
export class GetSupplierUserLoginReportRequest extends RequestBase {
    startYear: number;
    startWeek: number;
    endWeek: number;
    endYear: number;
    //range: DateWeekRange;
    reportType: number;
}

