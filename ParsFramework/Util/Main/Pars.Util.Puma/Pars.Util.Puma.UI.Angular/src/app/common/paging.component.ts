import { Component, Input, Injectable, Output, EventEmitter, OnInit } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
//import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'paging',
    templateUrl: 'paging.component.html',
    styleUrls: ['paging.component.css']
})
export class PagingComponent {
    public pageSize: number;
    public pageNumber: number;
    public pageCount: number;
    public resultObject: any;
    @Input()
    public url: string;
    @Output() pageFunc: EventEmitter<any> = new EventEmitter<any>();
    @Input()
    public jsonDataName: string;
    private queryString: string;

    constructor(private http: Http) {
        this.pageSize = 10;
        this.pageNumber = 1;
        this.pageCount = 10;
        this.queryString = "";
    }

    ngOnInit() {
        this.getHttpResult();
    }

    public previousPage() {
        if (this.pageNumber == 1) {
            return;
        }
        this.pageNumber--;
        this.getHttpResult();
    }

    public nextPage() {
        if (this.pageNumber == this.pageCount) {
            return;
        }
        this.pageNumber++;
        this.getHttpResult();

    }

    public pageNumberChange() {
        this.getHttpResult();
    }

    public addSearchParameter(value) {
        this.queryString = value == "" ? "" : "&searchtext=" + value;
        this.pageNumber = 1;
        this.pageNumberChange();
    }

    public getHttpResult() {
        this.http.get(this.url + '?pNumber=' + this.pageNumber + '&pSize=' + this.pageSize + this.queryString)
            .subscribe(result => {
                this.resultObject = result.json()[this.jsonDataName];
                    this.pageCount = Math.floor(result.json()['Count'] / this.pageSize) + 1;
                    this.sendDataToParent();
                 }
            );

    }

    sendDataToParent() {
        this.pageFunc.emit(
            {
                result: this.resultObject
            }
        );
    }
}