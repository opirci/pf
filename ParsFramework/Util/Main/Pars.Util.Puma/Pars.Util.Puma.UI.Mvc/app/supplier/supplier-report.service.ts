
import {
    Injectable, Observable, WebApiEndPoints,
    DateRange, LookupResponse, ResponseBase, RequestBase, DomainBase, HttpService, DataTable, Verbs, ParamThru
} from "../service.imports";

//import { Injectable } from "@angular/core";
//import { Observable} from "rxjs";
//import {  Verbs, ParamThru, HttpService, LookupResponse, ResponseBase, RequestBase} from "../pars/service";
//import { WebApiEndPoints } from "../settings/webapiendpoints";
//import { DateRange, DomainBase, DataTable } from "../pars/data";


import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

@Injectable()
export class SupplierReportService {
    constructor(
        private _httpService: HttpService) {
    }

    getSupplierReport(getSupplierReportRequest: GetSupplierReportRequest):
        Observable<GetSupplierReportResponse> {
        return this._httpService.requestAs<GetSupplierReportResponse>(
            Verbs.Get,
            WebApiEndPoints.apiServer + "supplier/GetSupplierReport",
            getSupplierReportRequest,
            ParamThru.Uri);
    }
}

export class GetSupplierReportResponse extends ResponseBase {
    values: SupplierReportLine[];
    count: number;
}

export class SupplierUserType {
    isAdmin: boolean;
    isManager: boolean;
    isFinance: boolean;
    isRepresentative: boolean;
}


export class GetSupplierReportRequest extends RequestBase {
    supplierCode: string;
    supplierName: string;
    countries: number[];
    segments: number[];
    supplierType: number;
    userType: SupplierUserType;
    logOnActive: boolean;
    dateRange: DateRange;
    modifiedUser: string;
}

export class SupplierReportLine extends DomainBase {
    supplierCode: string;
    supplierName: string;
    country: string;
    segmentName: string;
    supplierType: string;

    subSegmentName: string;
    userName: string;
    userSurname: string;

    get userNameSurname(): string { return `${this.userName} ${this.userSurname}`; }

    userEmail: string;
    modifiedDate: Date;
    modifiedUser: string;
    isManager: boolean;
    isFinance: boolean;
    isRepresentative: boolean;
    logonState: boolean;
    constructor(obj: any = null) { super(obj); }
}


