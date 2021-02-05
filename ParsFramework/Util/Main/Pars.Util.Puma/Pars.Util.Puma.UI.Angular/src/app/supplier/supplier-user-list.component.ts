import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { ReactiveFormsModule } from '@angular/forms';
import { Headers, Http } from '@angular/http';
import { Observable } from "rxjs/Observable";
import {
    SupplierService,
    GetSupplierRequest,
    GetSupplierResponse,
    GetSupplierUsersRequest,
    GetSupplierUsersResponse,
    SaveSupplierUserRequest,
    SaveSupplierUserResponse,
    SupplierUserExistsRequest,
    SupplierUserExistsResponse,
    GetSupplierClaimResponse,
    Supplier, SupplierUser, UserClaim
} from "./supplier.service";
import { EntityStates } from '../pars/data';
import { Claim } from '../claim/claim';
import { MessageText } from "../pars/core";
import { WebApiObservableService, HttpService } from "../pars/service";
import { PagedList, ColumnInfo, XModalComponent, NotifyMessage, NotifyMessageType } from "../common";
import { LcwGridService } from "../common/lcw-grid/lcw-grid.service";
import { LcwGridDataRequest } from "../common/lcw-grid/lcw-grid.ilcw-grid";
import { BooleanViews } from "../common/xgrid.column-info";
import { ComponentOptions } from '../settings/componentOptions';
import { FederationClaims } from "../services/federation.service";

@Component({
    moduleId: module.id,
    selector: "supplier-user-list",
    providers: [SupplierService, WebApiObservableService, HttpService, NotifyMessage],
    templateUrl: "supplier-user-list.component.html"

})
export class SupplierUserListComponent implements OnInit {
    isAdmin: boolean = false;
    isContactUserCreated: boolean = false;
    supplier: Supplier;

    get supplierUsers(): PagedList<SupplierUser> {
        return this!.xgridService.data as PagedList<SupplierUser>;
    }

    supplierClaims: Claim[] = [];
    selectedSupplierUser: SupplierUser;
    errorMessage: string;
    supplierCode: string;
    //dataSource: XGridDataSource;
    xgridService: LcwGridService;
    isSaveDisabled: boolean = false;
    isCreateContactUserDisabled: boolean = false;
    showModal: boolean = false;
    showFilter: boolean = false;
    filterStatus: number = 1;
    BooleanViews = BooleanViews;
    public notifyOptions = ComponentOptions.NotifyMessageOptions;

    @ViewChild("msgSuccessHeader") msgSuccessHeader: MessageText;
    @ViewChild("msgSuccessContent") msgSuccessContent: MessageText;
    @ViewChild("msgErrorHeader") msgErrorHeader: MessageText;
    @ViewChild("msgErrorContent") msgErrorContent: MessageText;
    @ViewChild("msgErrorNoClaim") msgErrorNoClaim: MessageText;
    @ViewChild("msgErrorMailUsed") msgErrorMailUsed: MessageText;

    claims: string[] = [FederationClaims.Vrp_TedPort_Yonetici, FederationClaims.Vrp_TedPort_Finans, FederationClaims.Vrp_TedPort_MusteriTemsilci];

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http, private supplierService: SupplierService, private notifyService: NotifyMessage, private route: ActivatedRoute, private router: Router, private webApiObservableService: WebApiObservableService) {

        this.supplier = new Supplier();
        this.newSupplierUser();

        this.xgridService = LcwGridService.create();
    }


    ngOnInit(): void {
        //this.dataSource = new XGridDataSource(this.getDataDelegate, this.searchDataDelegate);
        this.xgridService.onDataRequest.subscribe((ctx: LcwGridDataRequest) => this.getSupplierUsers());
        this.getSupplier();
        this.getSupplierClaims();
    }

    public newSupplierUser() {
        this.selectedSupplierUser = new SupplierUser();
        this.selectedSupplierUser.userRef = 0;
        this.selectedSupplierUser.hasManagerClaim = false;
        this.selectedSupplierUser.hasFinanceClaim = false;
        this.selectedSupplierUser.hasRepClaim = false;
    }

    public createContactUser() {
        this.isCreateContactUserDisabled = true;
        this.newSupplierUser();
        this.selectedSupplierUser.firstName = this.supplier.contactFirstName;
        this.selectedSupplierUser.lastName = this.supplier.contactLastName;
        this.selectedSupplierUser.mail = this.supplier.contactMail;
        this.selectedSupplierUser.hasManagerClaim = true;
        this.selectedSupplierUser.isActive = true;
        this.checkIfUserExistsAndSave();
    }

    public getSupplier() {
        this.supplierCode = this.route.snapshot.params['id'];
        var request = new GetSupplierRequest();
        request.supplierCode = this.supplierCode;

        this.supplierService.getSupplier(request)
            .subscribe(
            (response: GetSupplierResponse) => {
                this.supplier = response.value;
                this.isAdmin = response.isAdmin;
                this.getSupplierUsers();
                this.checkIfUserExists(this.supplier.contactMail);
            },
            (error: any) => this.errorMessage = error);
    }

    public getSupplierClaims(): void {
        this.supplierService.getSupplierClaims()
            .subscribe(
            (response: GetSupplierClaimResponse) => {
                this.supplierClaims = response.values;
            },
            (error: any) => this.errorMessage = error);
    }

    public setClaimsToUsers() {
        this.supplierUsers.forEach(x => {
            x.hasManagerClaim = false;
            x.hasFinanceClaim = false;
            x.hasRepClaim = false;

            x.claims.forEach(c => {
                if (c.name === this.claims[0])
                    x.hasManagerClaim = true;
                if (c.name === this.claims[1])
                    x.hasFinanceClaim = true;
                if (c.name === this.claims[2])
                    x.hasRepClaim = true;
            });
        });
    }

    public getSupplierUsers() {
        //this.dataSource.getData(0, 0);

        let request: GetSupplierUsersRequest = new GetSupplierUsersRequest();
        request.partyRef = this.supplier.partyRef;
        request.status = this.filterStatus;

        this.supplierService.getSupplierUsers(request)
            .map((response: GetSupplierUsersResponse) => {
                this.xgridService.data = new PagedList<SupplierUser>(this.fromJson(response.values), 0);
                //this.supplierUsers = new PagedList<SupplierUser>(this.fromJson(response.values), 0);
                //return this.supplierUsers;
            })
            .subscribe(() => this.setClaimsToUsers());
    }

    fromJson(supplierUsers: SupplierUser[]): SupplierUser[] {
        let arr: Array<SupplierUser> = [];
        for (var supItem of supplierUsers) {
            arr.push(new SupplierUser(supItem));
        }
        return arr;
    }

    //private getDataDelegate: IXGridGetDataDelegate = (page: number, pageSize: number): void => {
    //    let request: GetSupplierUsersRequest = new GetSupplierUsersRequest();
    //    request.partyRef = this.supplier.partyRef;
    //    request.status = this.filterStatus;

    //    this.supplierService.getSupplierUsers(request)
    //        .map((response: GetSupplierUsersResponse) => {
    //            this.supplierUsers = new PagedList<SupplierUser>(this.fromJson(response.values), 0);
    //            return this.supplierUsers;
    //        })
    //        .subscribe(() => this.setClaimsToUsers());
    //}

    //private searchDataDelegate: IXGridSearchDataDelegate =
    //(searchText: string, searchMember: string, page: number, pageSize: number): void => { this.getDataDelegate(page, pageSize); }

    selectFromGrid(evt: any): void {
        let userRef = (<SupplierUser>evt.result).userRef;
        this.selectedSupplierUser = this.supplierUsers.find(x => x.userRef === userRef);
        this.showModal = !this.showModal;
    }

    public save() {
        if (!this.selectedSupplierUser.hasManagerClaim &&
            !this.selectedSupplierUser.hasFinanceClaim &&
            !this.selectedSupplierUser.hasRepClaim) {
            this.notifyService.createMessage(NotifyMessageType.Error, this.msgErrorHeader, this.msgErrorNoClaim);
            return;
        }
        var isNewEntity = this.selectedSupplierUser.userRef === 0;
        this.isSaveDisabled = true;
        var elements: string[] = ["firstName", "lastName", "mail", "status"];

        if (!isNewEntity) {
            this.selectedSupplierUser.hasChanged = elements.some(prop => {
                return document.getElementById(prop).className.includes("ng-dirty");
            });
        }

        var request: SaveSupplierUserRequest = new SaveSupplierUserRequest();
        var _response: SaveSupplierUserResponse;

        var tempClaims: UserClaim[] = [];

        this.selectedSupplierUser.partyRef = this.supplier.partyRef;
        request.value = this.selectedSupplierUser;
        this.supplierClaims.forEach(c => {
            var userClaim: UserClaim = new UserClaim();
            let existingClaim: UserClaim;
            var claim: UserClaim[] = null;
            if (this.selectedSupplierUser.claims) {
                claim = this.selectedSupplierUser.claims.filter(x => x.name === c.Name);
                existingClaim = (claim != null && claim.length > 0) ? claim[0] : null;
            }
            let exists = existingClaim != null;

            userClaim.userRef = this.selectedSupplierUser.userRef;
            userClaim.claimRef = c.ClaimRef;
            userClaim.name = c.Name;
            userClaim.DataEntityState = EntityStates.Unchanged;

            if (this.isClaimChecked(c.Name) && exists) {
                userClaim.DataEntityState = EntityStates.Unchanged;
                userClaim.userClaimRef = existingClaim.userClaimRef;
            } else if (!this.isClaimChecked(c.Name) && exists) {
                userClaim.DataEntityState = EntityStates.Deleted;
                userClaim.userClaimRef = existingClaim.userClaimRef;
            }
            else if (this.isClaimChecked(c.Name) && !exists) {
                userClaim.DataEntityState = EntityStates.Added;
            } else {
                userClaim.userClaimRef = -1;
            }

            tempClaims.push(userClaim);
        });

        this.selectedSupplierUser.claims = tempClaims;
        this.selectedSupplierUser.isAdmin = this.isClaimChecked(FederationClaims.Vrp_TedPort_Yonetici);

        this.supplierService.saveSupplierUser(request)
            .subscribe(
            (response: SaveSupplierUserResponse) => {
                _response = response;
                this.selectedSupplierUser.userRef = response.value.userRef;
                this.selectedSupplierUser.claims.forEach(c => {
                    c.DataEntityState = EntityStates.Unchanged;
                    let cl = response.value.claims.find(clm => clm.claimRef === c.claimRef);
                    c.userClaimRef = cl.userClaimRef;
                });


                if ((isNewEntity && this.selectedSupplierUser.isActive && this.filterStatus !== 2) ||
                    (isNewEntity && !this.selectedSupplierUser.isActive && this.filterStatus !== 1)) {
                    this.supplierUsers.push(this.selectedSupplierUser);
                }

                if ((!isNewEntity && !this.selectedSupplierUser.isActive && this.filterStatus !== 2) ||
                    (!isNewEntity && this.selectedSupplierUser.isActive && this.filterStatus !== 1)) {
                    let index = this.supplierUsers.indexOf(this.supplierUsers
                        .find(x => x.userRef === this.selectedSupplierUser.userRef));
                    this.supplierUsers.splice(index, 1);
                }

                this.getSupplierUsers();
                this.newSupplierUser();
                if (this.showModal)
                    this.showModal = !this.showModal;
                this.notifyService.createMessage(NotifyMessageType.Success,
                    this.msgSuccessHeader,
                    this.msgSuccessContent);
                this.isSaveDisabled = false;
                if (this.isCreateContactUserDisabled) {
                    this.isContactUserCreated = true;
                    this.isCreateContactUserDisabled = false;
                }
            },
            (error: any) => {
                debugger;
                this.errorMessage = error;
                this.isSaveDisabled = false;
            });

    }

    public isClaimChecked(value: string): boolean {

        switch (value) {
            case FederationClaims.Vrp_TedPort_Yonetici:
                return this.selectedSupplierUser.hasManagerClaim;
            case FederationClaims.Vrp_TedPort_MusteriTemsilci:
                return this.selectedSupplierUser.hasRepClaim;
            case FederationClaims.Vrp_TedPort_Finans:
                return this.selectedSupplierUser.hasFinanceClaim;
        }
    }

    //triggers the save() function!
    public checkIfUserExistsAndSave() {

        if (!this.selectedSupplierUser.mail) {
            return;
        }

        var request = new SupplierUserExistsRequest();
        request.mail = this.selectedSupplierUser.mail;

        this.supplierService.supplierUserExists(request)
            .subscribe(
            (response: SupplierUserExistsResponse) => {
                if (response.value && this.selectedSupplierUser.userRef === 0) {
                    this.notifyService.createMessage(NotifyMessageType.Error,
                        this.msgErrorHeader,
                        this.msgErrorMailUsed);
                    return;
                } else {
                    this.save();
                }
            },
            (error: any) => this.errorMessage = error);
    }


    public checkIfUserExists(mail: string) {
        if (!mail)
            return false;

        var request = new SupplierUserExistsRequest();

        request.mail = mail;

        this.supplierService.supplierUserExists(request)
            .subscribe(
            (response: SupplierUserExistsResponse) => {
                this.isContactUserCreated = response.value;
            },
            (error: any) => this.errorMessage = error);
    }

    private handleError(error: any): Promise<any> {
        //console.error('An error occurred', error); // for demo puşrposes only
        this.notifyService.createMessage(NotifyMessageType.Error, this.msgErrorHeader, this.msgErrorContent + " : " + error.message);
        return Promise.reject(error.message || error);
    }
}