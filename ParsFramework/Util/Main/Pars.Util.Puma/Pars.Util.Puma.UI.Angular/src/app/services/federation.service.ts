import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

import { LookupResponse } from "../pars/service";
import { WebApiEndPoints } from "../settings/webapiendpoints";
import { UserContext } from "../pars/core";

const apiUrl: string = WebApiEndPoints.apiServer;

@Injectable()
export class FederationService {
    constructor(protected _http: Http) {
        this.getUserContext().subscribe(uc => FederationService._userContext = uc);
    }


    init(): Observable<boolean> {
        return this.getUserContext().map(uc => (FederationService._userContext = uc) != null);

    }

    getUserName(): Observable<string> {
        return this._http.get(apiUrl + "Federation/UserName")
            .map((response: Response) => <string>response.json());

    }

    getUserLanguage(): Observable<string> {
        return this._http.get(apiUrl + "Federation/Culture")
            .map((response: Response) => <string>response.json());

    }

    getUserContext(): Observable<UserContext> {
        return this._http.get(apiUrl + "Federation/UserContext")
            .map((response: Response) => <UserContext>response.json());

    }

    private static _userContext: UserContext = null;

    get userContext(): UserContext {
        if (FederationService._userContext === null) {
            this.getUserContext().subscribe((r: UserContext) => FederationService._userContext = r);
        }
        return FederationService._userContext;
    }


    /*
    private handleError(error: Response): void {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }*/
}



export class FederationClaims {
    public static get Vrp_TedPort_Admin(): string { return "Vrp_Tedport_Admin"; }
    public static get Vrp_TedPort_Yonetici(): string { return "Vrp_TedPort_Yonetici"; }
    public static get Vrp_TedPort_Finans(): string { return "Vrp_TedPort_Finans"; }
    public static get Vrp_TedPort_MusteriTemsilci(): string { return "Vrp_TedPort_MusteriTemsilci"; }
}
