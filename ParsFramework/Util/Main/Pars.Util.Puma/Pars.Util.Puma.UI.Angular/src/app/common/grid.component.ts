import {
    Component, Input, Output, EventEmitter, OnInit, ContentChildren,
    AfterContentInit, QueryList, Inject, forwardRef, AfterViewChecked
} from "@angular/core";

import { Http, Response, Headers } from "@angular/http";
import "rxjs/add/operator/toPromise";
// import "./public/css/styles.css";

@Component({
    moduleId: module.id,
    selector: "column",
    template: ""
})
export class GridColumnComponent implements AfterContentInit {
    ngAfterContentInit(): void {
        this._parent.columnList.push({
            headerName: this.headerName,
            fieldName: this.fieldName,
            invokeMethod: this.invokeMethod,
            allowSorting: this.allowSorting
        });
    }

    @Input()
    headerName: string;
    @Input()
    fieldName: string;
    @Input()
    invokeMethod: string;
    @Input()
    allowSorting: boolean;
    constructor( @Inject(forwardRef(() => GridComponent)) private _parent: GridComponent) {
        //this._parent.columnList.push({
        //    headerName: this.headerName,
        //    fieldName: this.fieldName
        //});
        // alert("headerName:" + this.headerName + ",fieldName:" + this.fieldName + ", length:" + <any>this._parent.columnList.length);
    }
}

@Component({
    moduleId: module.id,
    selector: "grid",
    templateUrl: "grid.component.html",
    styleUrls: ["grid.component.css"]
})
export class GridComponent implements OnInit, AfterViewChecked {
    @ContentChildren(GridColumnComponent) columnsArr: QueryList<GridColumnComponent>;
    // columns = [];
    // ngAfterContentInit() {

    //    debugger;
    //    if (this.columnsArr != null) {
    //        this.columns = this.columnsArr.toArray();
    //        alert(this.columns.length);
    //    } else {
    //        alert("columns is null");
    //    }
    //    if (this.columns != null) {
    //        this.columnList = new Array<ColumnInfo>();
    //        let column: any = null;
    //        for (column in this.columns) {
    //            this.columnList.push(
    //                {
    //                    headerName: column.headerName,
    //                    fieldName: column.fieldName
    //                });
    //        }
    //    }
    // }


    @Input()
    pageSize: number = 10;
    @Input()
    selectText: string = "Select";
    public  pageNumber: number;
    public  pageCount: number;
    public resultObject: any;
    @Input()
    url: string;
    @Output() pageFunc: EventEmitter<any> = new EventEmitter<any>();
    @Input()
    jsonDataName: string;
    public queryString: string;
    public sortQueryString: string;

    @Input()
    columnList: ColumnInfo[];
    @Output() selectFunc: EventEmitter<any> = new EventEmitter<any>();
    @Input()
    gridTitle: string;
    @Input()
    enableSelectColumn: string;
    @Input()
    enablePaging: string;

    searchText: string = "";
    isDataLoading: boolean = true;
    @Input()
    parent: any;

    constructor(private http: Http) {
        this.pageNumber = 1;
        this.pageCount = 10;
        this.queryString = "";
        this.sortQueryString = "";
        this.columnList = new Array<ColumnInfo>();
        // alert(this.columnList.length);
        
    }

    ngOnInit(): void {
        this.getHttpResult();
    }

    ngAfterViewChecked() {
        
            //this.highlightText();
        
    }
    getMember(data: any, memberName: string): any {
        let value: any = null;
        try {
            value = data[memberName];
            //if (this.searchText != null && this.searchText != "" && (typeof value == "string")) {
            //    value = value.replace(this.searchText, "<mark>" + this.searchText + "</mark>");
            //} 
        } catch (e) {
            value = "ERROR";
        }
        return value;
    }
    sortDirectionList: { [s: string]: string; } = {}
    SortClick(headerName: string)
    {
        //debugger;
        var direction = this.sortDirectionList[headerName];
        if (direction == "undefined" || direction == "desc") {
            direction = "asc";
        }
        else 
        { direction = "desc"; }
        this.sortDirectionList[headerName] = direction;
        this.sortQueryString = "&sk=" + headerName + "&sd=" + direction;
        this.pageNumber = 1;
        this.pageNumberChange();
    }
    getColumnValue(data: any, fieldName: string, invokeMethod: string): string {
        if (fieldName.indexOf(".") > 0) {
            let firstNode: string = fieldName.split(".")[0];
            return this.getColumnValue(this.getMember(data, firstNode), fieldName.replace(firstNode + ".", ""), invokeMethod);
        } else {
            //debugger;
            var result = this.getMember(data, fieldName);
            if (typeof invokeMethod != "undefined" && invokeMethod != "")
            {
                var proto = Object.getPrototypeOf(this.parent);
                if (proto) {
                    var str = proto[invokeMethod].call(proto,result);
                    result = str;
                }
            }
            return result;
            
        }
    }

    /* tslint:disable */
    previousPage(): void {
        if (this.pageNumber === 1) {
            return;
        }
        this.pageNumber--;
        this.getHttpResult();
    }

    nextPage(): void {
        if (this.pageNumber === this.pageCount) {
            return;
        }
        this.pageNumber++;
        this.getHttpResult();
    }

    addSearchParameter(value: string): void {
        this.queryString = value === "" ? "" : "&searchtext=" + value;
        this.pageNumber = 1;
        this.searchText = value;
        this.pageNumberChange();
    }

    select(selectedData: any): void {
        this.selectFunc.emit(
            {
                result: selectedData
            }
        );
    }
    /* tslint:enable */

    createAuthorizationHeader(headers: Headers): void {
        headers.append('Authorization', 'Bearer ' + sessionStorage.getItem('access_token'));
    }

    pageNumberChange(): void {
        this.getHttpResult();
    }
    getHttpResult(): void {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);

        this.isDataLoading = true;
        let mark: string = this.url.indexOf("?") > 0 ? "&" : "?";
        this.http.get(this.url + mark + "PageNumber=" + this.pageNumber + "&PageSize=" + this.pageSize + this.queryString + this.sortQueryString, { headers: headers })
            .subscribe((result: Response) => {
                this.resultObject = result.json()[this.jsonDataName];
                 /* tslint:disable */
                this.pageCount = Math.floor(result.json()["Count"] / this.pageSize) + 1;
                 /* tslint:enable */
               // this.sendDataToParent();
                this.isDataLoading = false;
            });
    }

    highlightText() {
        if (this.searchText != null && this.searchText != "") {
            var elements = document.getElementsByClassName("tr-mark");
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].innerHTML.indexOf("<mark>") < 0) {
                    elements[i].innerHTML = elements[i].innerHTML
                        .replace(new RegExp('(' + this.searchText + ')', 'ig'), '<b><mark>$1</mark></b>');
                }
            }
        }
    }

    sendDataToParent(): void {
        this.pageFunc.emit(
            {
                result: this.resultObject
            }
        );
    }
}

export class ColumnInfo {
    headerName: string;
    fieldName: string;
    invokeMethod: string;
    allowSorting: boolean;
}