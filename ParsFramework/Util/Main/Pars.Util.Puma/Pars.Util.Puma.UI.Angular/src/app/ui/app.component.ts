import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { WebApiEndPoints } from './../settings/webapiendpoints';
import { FederationService, FederationClaims } from "./../services/federation.service";
import { Observable } from "rxjs/Observable";
import { HttpService, WebApiObservableService } from "./../pars/service";
import { UserContextService } from "../pars/core";


//import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'my-app',
   // templateUrl: 'app.component.html',
    templateUrl: 'app.component.supplier.html',
    providers: [FederationService, HttpService, WebApiObservableService],
})
export class AppComponent implements OnInit {

    userName: string;
    userLanguage: string;
    supplierUrl: string;

    constructor(/*private federationService: FederationService, private ucService: UserContextService*/) {
        //console.log("entered constructor of AppComponent");
    }

    ngOnInit(): void {
        //console.log("entered constructor ngOnInit of AppComponent");
        //Observable.forkJoin(
        //    this.federationService.init(),
        //    this.federationService.getUserName(),
        //    this.federationService.getUserLanguage()
        //)
        //    .subscribe(results => {
        //        this.userName = results[1];
        //        this.userLanguage = results[2];
        //        document["locale"] = this.userLanguage;
        //    });

        //if (this.ucService.hasClaim(FederationClaims.Vrp_TedPort_Admin)) {
        //    this.supplierUrl = "http://auth-supplierportal.lcwaikiki.com";
        //} else {
        //    this.supplierUrl = "http://supplierportal.lcwaikiki.com";
        //}
    }
}
