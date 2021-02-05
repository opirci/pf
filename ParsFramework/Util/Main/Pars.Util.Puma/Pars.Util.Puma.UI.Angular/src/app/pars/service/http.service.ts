import { Injectable, ElementRef, EventEmitter } from "@angular/core";
import { Http, Response, Headers, RequestOptions, RequestMethod, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";
import { Verbs, ParamThru } from './webapi-observable.service';

@Injectable()
export class HttpService {
    headers: Headers;
    options: RequestOptions;


    constructor(private http: Http) {
        this.headers = new Headers({
            "Content-Type": "application/json",
            "Accept": "q=0.8;application/json;q=0.9"
        });
        this.options = new RequestOptions({ headers: this.headers });
    }

    withHeader(header: any): HttpService {
        this.headers = new Headers(header);
        return this;
    }

    //request(url: string, param: any, paramThru: HttpServiceParamThru = HttpServiceParamThru.Body): Observable<any> {
    //    return this.request(Verbs.Get, url, param, paramThru);
    //}

    request(verb: Verbs, url: string, param: any, paramThru: ParamThru = ParamThru.Body): Observable<any> {
        let params: URLSearchParams = new URLSearchParams();
        let body: any = null;
        if (paramThru === ParamThru.Uri) {
            for (var key in param) {
                if (param.hasOwnProperty(key)) {
                    let val = param[key];
                    params.set(key, val);
                }
            }
        } else {
            body = JSON.stringify(param) || {};
        }

        let requestMethod: RequestMethod;
        switch (verb) {
            case Verbs.Get:
                requestMethod = RequestMethod.Get;
                break;
            case Verbs.Post:
                requestMethod = RequestMethod.Post;
                break;
            case Verbs.Put:
                requestMethod = RequestMethod.Put;
                break;
            case Verbs.Delete:
                requestMethod = RequestMethod.Delete;
                break;
            default:
        }

        let options: RequestOptions = new RequestOptions({ headers: this.headers, search: params, body: body, method: requestMethod });

        return this.http.request(url, options).map(this.extractData)
            .catch(this.handleError);
    }

    get(url: string, param: any = null, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(Verbs.Get, url, param, paramThru);
    }

    put(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(Verbs.Put, url, param, paramThru);
    }

    post(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(Verbs.Post, url, param, paramThru);
    }

    delete(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(Verbs.Delete, url, param, paramThru);
    }

    requestAs<T>(verb: Verbs, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(verb, url, param, paramThru).map(o => <T>o);
    }

    getAs<T>(url: string, param: any = null, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(Verbs.Get, url, param, paramThru).map(o => <T>o);
    }

    putAs<T>(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(Verbs.Put, url, param, paramThru)
            .map(
            o => {
                //debugger;


                let val: T;
                try {
                    val = <T>o;
                } catch (ex) {
                    return null;
                }
                return val;
            });
    }

    postAs<T>(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(Verbs.Post, url, param, paramThru).map(o => <T>o);
    }

    deleteAs<T>(url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(Verbs.Delete, url, param, paramThru).map(o => <T>o);
    }

    private extractData(res: Response) {
        //debugger;
        if (res.status == 204) {
            return {};
        }
        let body = res.json();
        return body || {};
    }

    private handleError(error: any) {
        //debugger;
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : "Server error";
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}
