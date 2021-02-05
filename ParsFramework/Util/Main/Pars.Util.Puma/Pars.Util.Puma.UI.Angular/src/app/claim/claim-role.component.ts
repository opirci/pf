import { Component, Input } from "@angular/core";
import { Headers, Http, Response } from "@angular/http";
import { EntityState, IEntityState } from "../definition/entitystate";
import { WebApiEndPoints } from "../settings/webapiendpoints";

import "rxjs/add/operator/toPromise";

import { NotifyMessage, NotifyMessageType } from "../common/notify-message.component";
// import './public/css/styles.css';


import { getTranslationProviders } from "../i18n-providers";
import { AppModule } from "../app.module";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

@Component({
    moduleId: module.id,
    selector: "language-changer",
    template: `
        <div class="form-group">
            <button type="button" class="btn btn-success" 
            (click)="switchLanguage();">{{ isEnglish ? "Change Language to Turkish" : "Dili İngilizceye Çevir"}}</button>
        </div>`
})
export class LanguageChangerComponent {
     /* tslint:disable */
    isEnglish = document["locale"] === "en";
    static enProviders: Object[] = null;
    static trProviders: Object[] = null;
    currProvider: Object[] = null;
    switchLanguage(): void {
        this.isEnglish = document["locale"] === "en";
        document["locale"] = (this.isEnglish = !this.isEnglish) ? "en" : "tr";
    /* tslint:enable*/
        getTranslationProviders().then(providers => {
            if (this.isEnglish) {
                if (LanguageChangerComponent.enProviders == null)
                { LanguageChangerComponent.enProviders = providers; }
                this.currProvider = LanguageChangerComponent.enProviders;
            } else {
                if (LanguageChangerComponent.trProviders == null)
                { LanguageChangerComponent.trProviders = providers; }
                this.currProvider = LanguageChangerComponent.trProviders;
            }
            const options = { providers: this.currProvider };
            platformBrowserDynamic().bootstrapModule(AppModule, options);
        });
    }
}




import { GetRolesByClaimRefRequest, GetRolesByClaimRefResponse, RoleRelation, Role } from "./claim-role.component.entity";
import { Lookup } from "../pars/data";
import { ComponentOptions } from "../settings/componentOptions";
import { ColumnInfo } from "../common/grid.component";


@Component({
    moduleId: module.id,
    selector: "claim-role",
    templateUrl: "claim-role.component.html",
    styleUrls: ["claim.component.css"],
    providers: [NotifyMessage]
})
export class ClaimRoleComponent  {


    entityStates: EntityState[];
    protected headers = new Headers({ "Content-Type": "application/json" });
    Msg_SuccessHeader = "Bilgilendirme";
    Msg_SuccessContent = "Kayıt başarıyla gerçekleşti";
    Msg_ErroHeader = "Hata";
    Msg_ErrorContent = "Bir hata oluştu.";

  
    public getEntityStates(): void {
        this.http.get(WebApiEndPoints.EntityStates)
            .subscribe((result: Response) => {
                this.entityStates = result.json();
            }
            );
    }

    protected handleError(error: any): Promise<any> {
        console.error("An error occurred", error); // for demo purposes only
        this.notifyService.createMessage(NotifyMessageType.Error, this.Msg_ErroHeader, this.Msg_ErrorContent + " : " + error.message);
        return Promise.reject(error.message || error);
    }

    public selectedEntityChanged(entity: IEntityState, esName: string): void {
        let esRef: number = this.entityStates.filter((x: EntityState) => x.Description === esName)[0].EntityStateRef;
        entity.entityState.EntityStateRef = esRef;
        entity.entityState.Description = esName;
        entity.hasChanged = true;
    }












    @Input()
    claimRef: number;

    roleRelations: RoleRelation[];
    selectedEntityState: IEntityState;
    selectedRole: Role;
    selectedRoleRelation: RoleRelation;
    notifyOptions = ComponentOptions.NotifyMessageOptions;

    getRolesByClaimRefRequest: GetRolesByClaimRefRequest;
    getRolesByClaimRefResponse: GetRolesByClaimRefResponse;

    searchContaining: boolean;

    gridJsonDataName: string = "values";

    columnList: ColumnInfo[] = [
        /*{ headerName: "Name", fieldName: "name" },
        { headerName: "Explanation", fieldName: "localeData.Value" },
        { headerName: "Member State", fieldName: "memberState.Name" },
        { headerName: "Valid From", fieldName: "validFrom" },
        { headerName: "Valid To", fieldName: "validTo" },
        { headerName: "Created Date", fieldName: "createdDate" },
        { headerName: "Created User", fieldName: "createdUserRef" },
        { headerName: "Modified Date", fieldName: "modifiedDate" },
        { headerName: "Modified User", fieldName: "modifiedUserRef" },*/

    ];
    getDataFromPaging(evt: any): void {
        this.roleRelations = evt.result;
    }

    selectFromGrid(evt: any): void {
        this.selectedRoleRelation = evt.result;
        this.selectedRole = evt.result;
    }

    get roleSearchUrl(): string {
        return this.searchContaining ? WebApiEndPoints.RolesByContainingName : WebApiEndPoints.RolesByStartingName;
    }

    get roleRelationsUrl(): string {
        return WebApiEndPoints.RolesOfClaim + <any>this.claimRef;
    }


     constructor(protected http: Http, protected notifyService: NotifyMessage) {
        this.getEntityStates();
        this.newRoleRelation();
    }

    public newRoleRelation(): void {
        this.selectedRoleRelation = RoleRelation.createNew();
        this.selectedRole = Role.createNew();
    }

    setRole(event: any): void {
        let selectedLookup: Lookup = event.result as Lookup;
        this.selectedRole = Role.createNew();
        this.selectedRole.name = selectedLookup.Name;
        this.selectedRole.roleRef = selectedLookup.Ref;
    }

    public new(): void {
        this.selectedRoleRelation = RoleRelation.createNew();
    }

    public save(): void {
        let hasNameChanged: boolean = document.getElementById("name").className.includes("ng-dirty");
        var hasDescriptionChanged: boolean = document.getElementById("description").className.includes("ng-dirty");
        this.selectedRoleRelation.hasChanged = this.selectedRoleRelation.hasChanged ? this.selectedRoleRelation.hasChanged : hasNameChanged;
        this.selectedRoleRelation.localeData.HasChanged = hasDescriptionChanged;
        let json: string = JSON.stringify(this.selectedRoleRelation);
        this.http.post(WebApiEndPoints.SaveRole,
            json,
            { headers: this.headers })
            .toPromise()
            .then((res: Response) => {
                this.selectedRoleRelation = res.json();
                this.notifyService.createMessage(NotifyMessageType.Success, this.Msg_SuccessHeader, this.Msg_SuccessContent);
            }
            )
            .catch(this.handleError);
    }
}

