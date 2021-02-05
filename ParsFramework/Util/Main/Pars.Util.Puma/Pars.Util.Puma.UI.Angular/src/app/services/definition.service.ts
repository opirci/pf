import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { HttpService, Verbs, ParamThru } from "../pars/service";
import { LookupResponse } from "../pars/service";
import { DataTable } from "../pars/data";
import { GetDatabasesRequest, GetTablesRequest, GetTableColumnRequest } from "../dataLocalizer/contracts";
const apiUrl: string = WebApiEndPoints.apiServer;

@Injectable()
export class DefinitionService {
    constructor(protected _http: Http, private _httpService: HttpService) {

    }

    getSegmentsAsLookupCache: LookupResponse;
    getSubSegmentsAsLookupCache: LookupResponse;
    getBusinessLinesAsLookupCache: LookupResponse;
    getAgeSexGroupsAsLookupCache: LookupResponse;
    getSupplierTypesAsLookupCache: LookupResponse;


    getSegmentsAsLookup(): Observable<LookupResponse> {
        if (this.getSegmentsAsLookupCache) {
            console.warn("getSegmentsAsLookup from CACHE");
            return Observable.of(this.getSegmentsAsLookupCache);
        }

        return this._http.get(apiUrl + "definition/segments")
            .map((response: Response) => this.getSegmentsAsLookupCache = <LookupResponse>response.json());
    }

    getSubSegmentsAsLookup(): Observable<LookupResponse> {
        if (this.getSubSegmentsAsLookupCache)
            return Observable.of(this.getSubSegmentsAsLookupCache);

        return this._http.get(apiUrl + "definition/subsegments")
            .map((response: Response) => <LookupResponse>response.json());
    }

    getBusinessLinesAsLookup(): Observable<LookupResponse> {
        if (this.getBusinessLinesAsLookupCache)
            return Observable.of(this.getBusinessLinesAsLookupCache);

        return this._http.get(apiUrl + "definition/businesslines")
            .map((response: Response) => <LookupResponse>response.json());
    }

    getAgeSexGroupsAsLookup(): Observable<LookupResponse> {
        if (this.getAgeSexGroupsAsLookupCache)
            return Observable.of(this.getAgeSexGroupsAsLookupCache);

        return this._http.get(apiUrl + "definition/agesexgroups")
            .map((response: Response) => <LookupResponse>response.json());
    }

    getSupplierTypesAsLookup(): Observable<LookupResponse> {
        if (this.getSupplierTypesAsLookupCache)
            return Observable.of(this.getSupplierTypesAsLookupCache);

        return this._http.get(apiUrl + "definition/suppliertypes")
            .map((response: Response) => <LookupResponse>response.json());
    }

    getServers(): Observable<LookupResponse> {
        return this._http.get(apiUrl + "definition/servers")
            .map((response: Response) => <LookupResponse>response.json());
    }

    getDatabases(request: GetDatabasesRequest): Observable<LookupResponse> {
        return this._httpService
            .getAs<LookupResponse>(apiUrl + "definition/databases", request);
    }

    getTables(request: GetTablesRequest): Observable<LookupResponse> {
        return this._httpService
            .getAs<LookupResponse>(apiUrl + "definition/tables", request);
    }

    getColumns(request: GetTableColumnRequest): Observable<LookupResponse> {
        return this._httpService
            .getAs<LookupResponse>(apiUrl + "definition/columns", request);
    }

    getLanguages(): Observable<LookupResponse> {
        return this._http.get(apiUrl + "definition/languages")
            .map((response: Response) => <LookupResponse>response.json());
    }

    /*
    private handleError(error: Response): void {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }*/
}
