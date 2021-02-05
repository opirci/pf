import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

//import { LookupResponse, ResponseBase, RequestBase, DomainBase, WebApiObservableService, HttpService, Verbs, ParamThru, DataTable } from "../pars/index";

import { DomainBase, DataTable } from "../pars/data";
import { LookupResponse, ResponseBase, RequestBase, WebApiObservableService, HttpService, Verbs, ParamThru } from "../pars/service";
import { Claim } from '../claim/claim';
import { WebApiEndPoints } from "../settings/webapiendpoints";

import { DataService, XHttpService } from "../pars/service";
import { IProgressService, IMessageShowService } from '../pars/core';

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

        return this.xhttp.requestAs<GetSuppliersFilteredResponse>("getSuppliersFitered",
            Verbs.Get,
            this.apiUrl + "supplier/getsuppliersfiltered",
            getSuppliersFilteredRequest,
            ParamThru.Uri);


        //return this._webApiObservableService
        //    .getAs<GetSuppliersFilteredResponse>(this.apiUrl +
        //    "supplier/getsuppliersfiltered",
        //    getSuppliersFilteredRequest);

    }
}



@Injectable()
export class SupplierService {
    constructor(protected _http: Http,
        private _webApiObservableService: WebApiObservableService,
        private _httpService: HttpService) {
    }

    private apiUrl: string = WebApiEndPoints.apiServer;


    getSuppliersFitered(getSuppliersFilteredRequest: GetSuppliersFilteredRequest):
        Observable<GetSuppliersFilteredResponse> {
        if (!getSuppliersFilteredRequest.ageSexGroups)
            getSuppliersFilteredRequest.ageSexGroups = [];
        if (!getSuppliersFilteredRequest.businessLines)
            getSuppliersFilteredRequest.businessLines = [];
        if (!getSuppliersFilteredRequest.subSegments)
            getSuppliersFilteredRequest.subSegments = [];

        return this._httpService.requestAs<GetSuppliersFilteredResponse>(
            Verbs.Get,
            this.apiUrl + "supplier/getsuppliersfiltered",
            getSuppliersFilteredRequest,
            ParamThru.Uri);


        //return this._webApiObservableService
        //    .getAs<GetSuppliersFilteredResponse>(this.apiUrl +
        //    "supplier/getsuppliersfiltered",
        //    getSuppliersFilteredRequest);

    }

    getSupplier(getSupplierRequest: GetSupplierRequest): Observable<GetSupplierResponse> {
        return this._webApiObservableService
            .getAs<GetSupplierResponse>(this.apiUrl + "supplier/getSupplier", getSupplierRequest);
    }

    getSupplierUsers(getSupplierUsersRequest: GetSupplierUsersRequest): Observable<GetSupplierUsersResponse> {

        return this._webApiObservableService
            .getAs<GetSupplierUsersResponse>(this.apiUrl + "supplier/getSupplierUsers", getSupplierUsersRequest);
    }

    getSupplierClaims(): Observable<GetSupplierClaimResponse> {
        return this._webApiObservableService
            .getAs<GetSupplierClaimResponse>(this.apiUrl + "supplier/getSupplierClaims", null);
    }

    saveSupplierUser(request: SaveSupplierUserRequest): Observable<SaveSupplierUserResponse> {
        return this._webApiObservableService
            //.putAs<SaveSupplierUserResponse>(this.apiUrl + "supplier/saveSupplierUser", request, ParamThru.Body);
            .putAs<SaveSupplierUserResponse>(this.apiUrl + "supplier/saveSupplierUser", request);
    }

    userExists(request: SupplierUserExistsRequest): Observable<SupplierUserExistsResponse> {
        return this._httpService
            .putAs<SupplierUserExistsResponse>(this.apiUrl + "supplier/supplierUserExists", request, ParamThru.Body);
    }

    supplierUserExists(request: SupplierUserExistsRequest): Observable<SupplierUserExistsResponse> {
        return this._webApiObservableService
            .putAs<SupplierUserExistsResponse>(this.apiUrl + "supplier/supplierUserExists", request);
    }

    validateUser(request: ValidateUserRequest): Observable<ValidateUserResponse> {
        return this._webApiObservableService
            .putAs<ValidateUserResponse>(this.apiUrl + "supplier/validateUser", request);
    }
}

export class Supplier {
    supplierRef: number;
    partyRef: number;
    supplierCode: string;
    name: string;
    contactFirstName: string;
    contactLastName: string;
    contactMail: string;
    countryRef: number;
}

export class SupplierUser extends DomainBase {
    userRef: number;
    firstName: string;
    lastName: string;
    mail: string;
    partyRef: number;
    isAdmin: boolean;
    isActive: boolean;
    hasChanged: boolean;
    claims: UserClaim[];
    hasManagerClaim: boolean;
    hasRepClaim: boolean;
    hasFinanceClaim: boolean;

    get fullName(): string {
        return this.firstName + " " + this.lastName;
    }

    constructor(obj: any = null) {
        super(obj);
    }
}

export class UserClaim extends DomainBase {
    userClaimRef: number;
    userRef: number;
    claimRef: number;
    name: string;
}

export class GetSuppliersFilteredResponse extends ResponseBase {
    values: Supplier[];
    count: number;
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

export class GetSupplierRequest extends RequestBase {
    supplierCode: string;
}

export class GetSupplierResponse extends ResponseBase {
    value: Supplier;
    isAdmin: boolean;
}

export class GetSupplierUsersRequest extends RequestBase {
    partyRef: number;
    status: number;
}

export class GetSupplierUsersResponse extends ResponseBase {
    values: SupplierUser[];
}

export class GetSupplierClaimResponse extends ResponseBase {
    values: Claim[];
}

export class SaveSupplierUserRequest extends RequestBase {
    value: SupplierUser;
    //countryRef: number;
}

export class SaveSupplierUserResponse extends ResponseBase {
    value: SupplierUser;
}

export class SupplierUserExistsRequest extends RequestBase {
    mail: string;
}

export class SupplierUserExistsResponse extends ResponseBase {
    value: boolean;
}



export class ValidateUserRequest extends RequestBase {
    userRef: number;
    supplierCode: string;
}

export class ValidateUserResponse extends ResponseBase {
    isValid: boolean;
}



