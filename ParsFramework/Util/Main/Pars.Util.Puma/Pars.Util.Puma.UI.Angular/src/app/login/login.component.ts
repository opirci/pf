import { Component, OnInit, } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Http, Headers, RequestOptions, Response, URLSearchParams } from '@angular/http';
import { Observable } from "rxjs/Observable";

@Component({
    moduleId: module.id,
    templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {
    model: any = {};
    loading = false;
    error = '';

    constructor(
        private route: ActivatedRoute, private router: Router, private http: Http) { }

    ngOnInit() {
        debugger;

        let routeFragment = this.route.snapshot.fragment;

        let params = new URLSearchParams(routeFragment.split('&')[0]);
        let access_token = params.get('access_token');
        sessionStorage.setItem('access_token', access_token);
        this.router.navigate(['/Index']);

    }    
    
}