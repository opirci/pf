import { Component, Input } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Claim } from './claim';
import { EntityState } from '../definition/entitystate'
// import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
// import { SimpleNotificationsComponent, NotificationsService } from 'angular2-notifications';
import { WebApiEndPoints } from '../settings/webapiendpoints';
import { ComponentOptions } from '../settings/componentOptions';
import { NotifyMessage, NotifyMessageType } from '../common/notify-message.component';
import { GridComponent, ColumnInfo } from '../common/grid.component';

import { LanguageChangerComponent } from "./claim-role.component";

// import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'claim',
    templateUrl: 'claim.component.html',
    styleUrls: ['claim.component.css'],
    providers: [NotifyMessage]
})
export class ClaimComponent {
    @Input()
    public claims: Claim[];
    public selectedClaim: Claim;
    public entityStates: EntityState[];
    private headers = new Headers({ 'Content-Type': 'application/json' });
    public url = WebApiEndPoints.ClaimList;
    public jsonDataName = "Claims";
    //public myDatePickerInlineOptions = ComponentOptions.DatePickerOptions;
    public notifyOptions = ComponentOptions.NotifyMessageOptions;
    public columnList: ColumnInfo[];
    public gridTitle: string;
    //public thisObject: ClaimComponent;

    private Msg_SuccessHeader = "Bilgilendirme";
    private Msg_SuccessContent = "Kayıt başarıyla gerçekleşti";
    private Msg_ErroHeader = "Hata";
    private Msg_ErrorContent = "Bir hata oluştu.";

    c1: ColumnInfo;
    c2: ColumnInfo;
    c3: ColumnInfo;
    c4: ColumnInfo;

    constructor(private http: Http, private notifyService: NotifyMessage) {
        this.getEntityStates();
        this.newClaim();
        this.locale = "tr";
       // this.thisObject = this;
        //this.c1 = new Gridcomponent.ColumnInfo();
        //this.c1.fieldName = "ClaimRef";
        //this.c1.headerName = "ClaimRef";

        //this.c2 = new Gridcomponent.ColumnInfo();
        //this.c2.fieldName = "Name";
        //this.c2.headerName = "Name";

        //this.c3 = new Gridcomponent.ColumnInfo();
        //this.c3.fieldName = "LocaleData.Value";
        //this.c3.headerName = "Description";

        //this.c4 = new Gridcomponent.ColumnInfo();
        //this.c4.fieldName = "EntityState.Description";
        //this.c4.headerName = "EntityStateName";

        //this.columnList = [this.c1, this.c2, this.c3, this.c4];
        this.gridTitle = "Claim List";
    }

    get selectedClaimRef(): number {
        return this.selectedClaim.ClaimRef;
    }

    public getDataFromPaging(evt) {
        this.claims = evt.result;

    }

    public selectFromGrid(evt) {
        this.selectedClaim = evt.result;
    }

    public getEntityStates() {
        this.http.get(WebApiEndPoints.EntityStates)
            .subscribe(result => {
                this.entityStates = result.json();
            }
            );

    }

    public save() {
        var hasNameChanged = document.getElementById("name").className.includes("ng-dirty");
        var hasDescriptionChanged = document.getElementById("description").className.includes("ng-dirty");
        this.selectedClaim.HasChanged = this.selectedClaim.HasChanged ? this.selectedClaim.HasChanged : hasNameChanged;
        this.selectedClaim.LocaleData.HasChanged = hasDescriptionChanged;
        var json = JSON.stringify(this.selectedClaim);
        this.http.post(WebApiEndPoints.ClaimPost,
            json,
            { headers: this.headers })
            .toPromise()
            .then(res => {
                this.selectedClaim = res.json();
                this.notifyService.createMessage(NotifyMessageType.Success, this.Msg_SuccessHeader, this.Msg_SuccessContent);
            }
            )
            .catch(this.handleError);
    }

    public newClaim() {
        this.selectedClaim = new Claim(0, 0, '', '', false);
    }


    select(claim: Claim): void {
        this.selectedClaim = claim;
        this.selectedClaim.HasChanged = false;
        this.selectedClaim.LocaleData.HasChanged = false;
    }

    selectedEntityChanged(esName) {
        var esRef = this.entityStates.filter(x => x.Description == esName)[0].EntityStateRef;
        this.selectedClaim.EntityState.EntityStateRef = esRef;
        this.selectedClaim.EntityState.Description = esName;
        this.selectedClaim.HasChanged = true;
    }

    public test(code) {
        alert(code);
    }
    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        this.notifyService.createMessage(NotifyMessageType.Error, this.Msg_ErroHeader, this.Msg_ErrorContent + " : " + error.message);
        return Promise.reject(error.message || error);
    }

    private selectedTextInline: string = '';
    private border: string = 'none';
    private locale: string = '';

    public returnState(state: string): string{
        debugger;
        var result = "";
        if (state == "Aktif")
        {
            result = "ActiveThis";
        }
        if (state == "Pasif") {
            result = "PassiveThat";
        }
        return result;
    }

    onDateChanged(event: any) {
        // console.log('onDateChanged(): ', event.date, ' - jsdate: ', new Date(event.jsdate).toLocaleDateString(), 
        //             ' - formatted: ', event.formatted, ' - epoc timestamp: ', event.epoc);
        if (event.formatted !== '') {
            //  this.selectedTextInline = 'Formatted: ' + event.formatted + ' - epoc timestamp: ' + event.epoc;
            //  this.border = '1px solid #CCC';
        }
        else {
            this.selectedTextInline = '';
            this.border = 'none';
        }
    }


}


