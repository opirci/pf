import { Component, Input, Output, EventEmitter } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Lookup } from "../pars/data";
import { LookupResponse } from "../pars/service";

// import "./public/css/styles.css";
@Component({
    moduleId: module.id,
    selector: "autoComplete",
    templateUrl: "auto-complete.component.html",
    styleUrls: ["auto-complete.component.css"]
})
export class AutoCompleteComponent {
    public Items: Lookup[];
    // private searchTerms = new Rx.Subject<string>();
    @Input()
    public url: string;
    @Output() autoCompleteFunc: EventEmitter<any> = new EventEmitter<any>();
    //@ Output()
    public selectedItem: Lookup;
    constructor(private http: Http) {
        this.selectedItem = new Lookup();
    }

    search(term: string): void {
        if (term.length > 2) {
            this.getHttpResult(term);
        } else {
            this.Items = null;
        }
    }

    public getHttpResult(search: string): void {
        this.http.get(this.url + search)
            .subscribe((result: Response) => {
                let res: any = result.json();
                let lookupResponse: LookupResponse = res as LookupResponse;
                // if (res instanceof LookupResponse) {
                if (lookupResponse != null) {
                    this.Items = lookupResponse.values;
                } else {
                    this.Items = res;
                }
            });
    }

    selectItem(item: Lookup): void {
        this.selectedItem = item;
        this.Items = null;
        this.sendDataToParent();
    }

    sendDataToParent(): void {
        this.autoCompleteFunc.emit(
            {
                result: this.selectedItem
            }
        );
    }

}