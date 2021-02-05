//import {
//    Injectable, Observable, WebApiEndPoints,
//    DateRange, LookupResponse, ResponseBase, RequestBase, DomainBase, HttpService, DataTable, Verbs, ParamThru
//} from "../service.imports";


import { WebApiEndPoints } from "../settings/webapiendpoints";
import { HttpService, Verbs, ParamThru } from "../pars/service";
import { DataTable } from "../pars/data";
import { Observable } from "rxjs/Observable";

import { GetSupplierReportRequest } from "../supplier/supplier-report.service";
import { Injectable } from "@angular/core";

@Injectable()
export class TestService {
    constructor(       
        private _httpService: HttpService) {
    }

    private apiUrl: string = WebApiEndPoints.apiServer;


    saveDataTable(request: DataTable): Observable<any> {
        return this._httpService.requestAs<DataTable>(
            Verbs.Put,
            WebApiEndPoints.apiServer + "test/SaveDataTable",
            request,
            ParamThru.Body);
    }

    getSupplierReportAsDataTable(getSupplierReportRequest: GetSupplierReportRequest):
        Observable<DataTable> {
        return this._httpService.requestAs<DataTable>(
            Verbs.Get,
            WebApiEndPoints.apiServer + "test/GetSupplierReportAsDataTable",
            getSupplierReportRequest,
            ParamThru.Uri);
    }
}