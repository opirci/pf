import { Component, Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Claim, ClaimListResponse } from './claim';
import { EntityState } from '../definition/entitystate'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
//import { ComponentOptions } from '../settings/componentOptions';
import { WebApiEndPoints } from '../settings/webapiendpoints';

//import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'claim-user',
    templateUrl: 'claim-user.component.html',
    styleUrls: ['claim.component.css']
})
export class ClaimUserComponent {
    public startDate: Date;
    //public myDatePickerInlineOptions = ComponentOptions.DatePickerOptions;
    private selectedTextInline: string = '';
    private border: string = 'none';
    private locale: string = '';
    public userSearchUrl = WebApiEndPoints.UserSearch;

    constructor(private http: Http) {
        //this.startDate = new Date("2016-12-08");
        this.locale = "tr";
    }

    onDateChanged(event: any) {
        //console.log('onDateChanged(): ', event.date, ' - jsdate: ', new Date(event.jsdate).toLocaleDateString(), ' - formatted: ', event.formatted, ' - epoc timestamp: ', event.epoc);
        if (event.formatted !== '') {
            //  this.selectedTextInline = 'Formatted: ' + event.formatted + ' - epoc timestamp: ' + event.epoc;
            //  this.border = '1px solid #CCC';
        }
        else {
            this.selectedTextInline = '';
            this.border = 'none';
        }
    }

    setUser(event: any) {

        //event.result.Name
        //event.result.Ref
    }
    
}

    