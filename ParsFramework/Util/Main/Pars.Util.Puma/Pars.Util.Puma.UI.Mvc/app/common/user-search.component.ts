import { Component, OnInit } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import * as Rx from 'rxjs/Rx';
import { WebApiEndPoints} from "../settings/webapiendpoints";

//import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'userSearch',
    templateUrl: 'user-search.component.html',
    styleUrls: ['user-search.component.css']
})
export class UserSearchComponent {
    public Users: Lookup[];
    private searchTerms = new Rx.Subject<string>();
    public url = "http://localhost:57057/api/user/SearchUser?searchText=";
    public SelectedUser: Lookup;
    constructor(private http: Http) {
        this.SelectedUser = new Lookup();
    }

    search(term: string): void {
        if (term.length > 2) {
            this.getHttpResult(term);
        }
    }


    //ngOnInit(): void {
    //     this.searchTerms
    //        .debounceTime(300)        // wait for 300ms pause in events
    //        .distinctUntilChanged()   // ignore if next search term is same as previous
    //         .switchMap(term => term ? this.userSearch(term) : Observable.of<Lookup[]>([]))
    //        .catch(error => {
    //             TODO: real error handling
    //            console.log(error);
    //            return Observable.of<Lookup[]>([]);
    //         });
    //}

    //userSearch(term: string): Observable<Lookup[]> {
    //    return this.http
    //        .get(this.url + term)
    //        .map((r: Response) => r.json() as Lookup[]);
    //}

    public getHttpResult(search) {
        this.http.get(WebApiEndPoints.UserSearch + search)
            .subscribe(result => {
                this.Users = result.json();
                }
            );
        }

    selectUser(user: Lookup): void {
        this.SelectedUser = user;
        this.Users = null;
    }

}

export class Lookup {
    Name: string;
    Ref: number;
}