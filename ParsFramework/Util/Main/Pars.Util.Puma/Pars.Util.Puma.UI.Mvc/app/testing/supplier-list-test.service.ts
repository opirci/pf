import { DataService, XHttpService } from "../pars/service";
import { IProgressService, IMessageShowService } from '../pars/core';
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { Observable } from "rxjs/Observable";

import { LookupResponse, ResponseBase, RequestBase, WebApiObservableService, HttpService, Verbs, ParamThru } from "../pars/service";

export class XSupplierService extends DataService {
    constructor(
        public xhttp: XHttpService,
        protected progressService: IProgressService = null,
        protected messageShowService: IMessageShowService = null) {
        super(xhttp, progressService, messageShowService);
    }

    private apiUrl: string = WebApiEndPoints.apiServer;

    getSupplierTypesAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getSupplierTypesAsLookup", this.apiUrl + "definition/suppliertypes");

    }

    getSuppliersFitered(getSuppliersFilteredRequest: GetSuppliersFilteredRequest):
        Observable<GetSuppliersFilteredResponse> {
        if (!getSuppliersFilteredRequest.ageSexGroups)
            getSuppliersFilteredRequest.ageSexGroups = [];
        if (!getSuppliersFilteredRequest.businessLines)
            getSuppliersFilteredRequest.businessLines = [];
        if (!getSuppliersFilteredRequest.subSegments)
            getSuppliersFilteredRequest.subSegments = [];

        return this.xhttp.getAs<GetSuppliersFilteredResponse>("getSuppliersFitered",            
            this.apiUrl + "supplier/getsuppliersfiltered",
            getSuppliersFilteredRequest,
            ParamThru.Uri);

        //return this._webApiObservableService
        //    .getAs<GetSuppliersFilteredResponse>(this.apiUrl +
        //    "supplier/getsuppliersfiltered",
        //    getSuppliersFilteredRequest);

    }
}


export class GetSuppliersFilteredRequest extends RequestBase {
    code: string;
    name: string;
    hasNoUsers: boolean;
    supplierType: number;
    segment: number;
    subSegments: number[];
    businessLines: number[];
    ageSexGroups: number[];
    pageNumber: number;
    pageSize: number;
}


export class GetSuppliersFilteredResponse extends ResponseBase {
    values: Supplier[];
    count: number;
}

export class Supplier {
    supplierRef: number;
    partyRef: number;
    supplierCode: string;
    name: string;
    contactFirstName: string;
    contactLastName: string;
    contactMail: string;
}

export class XDefinitionService extends DataService {
    constructor(
        public xhttp: XHttpService,
        protected progressService: IProgressService = null,
        protected messageShowService: IMessageShowService = null) {
        super(xhttp, progressService, messageShowService);
    }

    private apiUrl: string = WebApiEndPoints.apiServer;

    getSupplierTypesAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getSupplierTypesAsLookup", this.apiUrl + "definition/suppliertypes");

    }

    getSegmentsAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getSegmentsAsLookup", this.apiUrl + "definition/segments");

    }

    getSubSegmentsAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getSubSegmentsAsLookup", this.apiUrl + "definition/subsegments");

    }

    getBusinessLinesAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getBusinessLinesAsLookup", this.apiUrl + "definition/businesslines");

    }

    getAgeSexGroupsAsLookup(): Observable<LookupResponse> {
        return this.xhttp.getAs<LookupResponse>("getAgeSexGroupsAsLookup", this.apiUrl + "definition/agesexgroups");

    }
}
