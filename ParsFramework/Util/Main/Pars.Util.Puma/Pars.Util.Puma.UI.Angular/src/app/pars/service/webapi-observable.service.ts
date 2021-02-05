import { Injectable, ElementRef, EventEmitter } from "@angular/core";
import { Http, Response, Headers, RequestOptions, RequestMethod, URLSearchParams }
    from "@angular/http";
import { Observable } from "rxjs/Observable";

// Observable class extensions
import "rxjs/add/observable/of";
import "rxjs/add/observable/throw";

// Observable operators
import "rxjs/add/operator/catch";
import "rxjs/add/operator/debounceTime";
import "rxjs/add/operator/distinctUntilChanged";
import "rxjs/add/operator/do";
import "rxjs/add/operator/filter";
import "rxjs/add/operator/map";
import "rxjs/add/operator/switchMap";

@Injectable()
export class WebApiObservableService {
    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {
        this.headers = new Headers({
            "Content-Type": "application/json",
            "Accept": "q=0.8;application/json;q=0.9"
        });
        this.options = new RequestOptions({ headers: this.headers });
    }

    getAs<T>(url: string, param: any): Observable<T> {
        return this.get(url, param).map(o => <T>o);
    }

    get(url: string, param: any): Observable<any> {
        let params: URLSearchParams = new URLSearchParams();
        for (var key in param) {
            if (param.hasOwnProperty(key)) {
                let val = param[key];
                params.set(key, val);
            }
        }
        this.options = new RequestOptions({ headers: this.headers, search: params });
        return this.http
            .get(url, this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    putAs<T>(url: string, param: any): Observable<T> {
        return this.put(url, param).map(o => <T>o);
    }

    put(url: string, param: any): Observable<any> {
        let body = JSON.stringify(param);
        return this.http
            .put(url, body, this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleError(error: any) {
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : "Server error";
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}

export enum Verbs {
    Get = 0,
    Post = 1,
    Put = 2,
    Delete = 3
}

export enum ParamThru {
    Uri,
    Body
}