import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions, RequestMethod, URLSearchParams } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";
import { Verbs, ParamThru } from './webapi-observable.service';
import { HttpService } from './http.service';
import { ResponseBase } from './response-base';
import { MessageService, IProgressMessage, CoreHelper } from '../core';

export class ServiceActions {
    readonly start: string = "start";
    readonly end: string = "end";
    readonly success: string = "success";
    readonly warning: string = "warning";
    readonly error: string = "error";
    readonly exception: string = "exception";
}

@Injectable()
export class XHttpService {
    actions: ServiceActions = new ServiceActions();
    messageService: MessageService = null;

    messageContext: IProgressMessage;

    static create(http: Http, messageService: MessageService = null): XHttpService {

        let service: XHttpService = new XHttpService(http);
        if (messageService != null)
            service.messageService = messageService;
        return service;
    }

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {
        this.headers = new Headers({
            "Content-Type": "application/json",
            "Accept": "q=0.8;application/json;q=0.9"
        });
        this.options = new RequestOptions({ headers: this.headers });
        this.messageService = new MessageService();
    }

    request(action: string, verb: Verbs, url: string, param: any, paramThru: ParamThru = ParamThru.Body): Observable<any> {

        let msgTmp: IProgressMessage;
        if (this.messageContext != null) {
            msgTmp = {
                errorMessage: this.messageContext.errorMessage,
                exceptionMessage: this.messageContext.exceptionMessage,
                successMessage: this.messageContext.successMessage,
                warningMessage: this.messageContext.warningMessage,
                action: this.messageContext.action
            }
            this.messageContext = null;
        }
        if (msgTmp.action != null)
            action = msgTmp.action;
        
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

        this.messageService.send("", action, this.actions.start);
       
        return this.http.request(url, options)
            .map(r => this.extractData(r, action, msgTmp))
            .catch(err => this.handleError(err, action, msgTmp));
    }

    get(action: string, url: string, param: any = null, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(action, Verbs.Get, url, param, paramThru);
    }

    put(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(action, Verbs.Put, url, param, paramThru);
    }

    post(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(action, Verbs.Post, url, param, paramThru);
    }

    delete(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<any> {
        return this.request(action, Verbs.Delete, url, param, paramThru);
    }

    requestAs<T>(action: string, verb: Verbs, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(action, verb, url, param, paramThru).map(o => <T>o);
    }

    getAs<T>(action: string, url: string, param: any = null, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(action, Verbs.Get, url, param, paramThru).map(o => <T>o);
    }

    putAs<T>(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(action, Verbs.Put, url, param, paramThru)
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

    postAs<T>(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(action, Verbs.Post, url, param, paramThru).map(o => <T>o);
    }

    deleteAs<T>(action: string, url: string, param: any, paramThru: ParamThru = ParamThru.Uri): Observable<T> {
        return this.request(action, Verbs.Delete, url, param, paramThru).map(o => <T>o);
    }

    private extractData(res: Response, action: string, msg: IProgressMessage) {
        debugger;
        this.messageService.send("", action, this.actions.end);
        let successMessage: string = msg!.successMessage;
        let errorMessage: string = msg!.errorMessage;
        let warningMessage: string = msg!.warningMessage;
        let success: boolean = true;
        let body = {};

        if (res.status != 204) {
            body = res.json();
            let respBase: ResponseBase = new ResponseBase();
            CoreHelper.copyFromJsonObject(respBase, body);
           
            debugger;
            if (respBase != null) {
                let r: ResponseBase = new ResponseBase();
                r = respBase;
                if (r.hasErrors) {
                    if (errorMessage != null) {
                        this.messageService.send(errorMessage, action, this.actions.error);                        
                    }
                    else {
                        this.messageService.send(r.errorMessagesText, action, this.actions.error);
                    }
                    success = false;
                }
                if (r.hasWarnings) {
                    if (warningMessage != null) {
                        this.messageService.send(warningMessage, action, this.actions.warning);
                    }
                    else {
                        this.messageService.send(r.warningMessagesText, action, this.actions.warning);
                    }
                    success = false;
                }
            }
            if (success == true && successMessage != null) {
                this.messageService.send(successMessage, action, this.actions.success);
            }
        }
        return body || {};
    }

    private handleError(error: any, action: string, msg: IProgressMessage) {
        debugger;
        let errorMessage = msg!.errorMessage;        
        this.messageService.send("", action, this.actions.end);

        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : "Server error";
        console.error(errMsg);
        errorMessage = errorMessage == null ? errMsg : errorMessage;

        this.messageService.send(errorMessage, action, this.actions.exception);
        return Observable.throw(errMsg);
    }
}