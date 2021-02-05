import { Component, Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Claim, ClaimListResponse } from './claim';
import { EntityState } from '../definition/entitystate';
import { MemberState } from '../definition/memberState';
import { UserGroupRelation } from '../model/userGroupRelation'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';
import { WebApiEndPoints } from "../settings/webapiendpoints";

//import './public/css/styles.css';
@Component({
    moduleId: module.id,
    selector: 'claim-usergroup',
    templateUrl: 'claim-usergroup.component.html',
    styleUrls: ['claim.component.css']
})
export class ClaimUserGroupComponent {

    private userGroupRelations: UserGroupRelation[];
    private memberStates: MemberState[];
    private selectedUserGroupRelation: UserGroupRelation;
    public jsonDataName = "UserGroups";

    public getDataFromPaging(evt) {
        this.userGroupRelations = evt.result;

    }

    constructor(private http: Http) {
        //this.getEntityStates();
        this.newUserGroupRelation();
    }

    public newUserGroupRelation() {
        this.selectedUserGroupRelation = new UserGroupRelation();
        this.selectedUserGroupRelation.UserGroupRef = 0;
        this.selectedUserGroupRelation.Name = "";
        this.selectedUserGroupRelation.Description = "";

        var memberState = new MemberState();
        memberState.MemberStateRef = 1;
        memberState.Name = "Valid";
        this.selectedUserGroupRelation.MemberState = memberState;

        //this.selectedUserGroupRelation.ValidFrom = Date.UTC()
        //this.selectedUserGroupRelation.ValidTo = Date.now();
    }

    public getMemberStates() {
        this.http.get(WebApiEndPoints.MemberStates
        )
            .subscribe(result => {
                this.memberStates = result.json();
            }
            );
    }


}

