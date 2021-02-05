export class WebApiEndPoints {
    private static mAppUrl = document["appUrl"];
    static get apiServer(): string { return this.mAppUrl + "api/"; }
    static get ClaimList(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/get"; }
    static get EntityStates(): string { return "http://localhost/Pars.Util.Puma.Api.Definition/api/definition/entitystates"; }
    static get MemberStates(): string { return "http://localhost/Pars.Util.Puma.Api.Definition/api/definition/memberstates"; }

    static get ClaimPost(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/post"; }
    static get RolesOfClaim(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/RolesOfClaim/?claimRef="; }
    static get RolesByContainingName(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/RolesByContainingName/?text="; }
    static get RolesByStartingName(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/RolesByStartingName/?text="; }
    static get UserSearch(): string { return this.apiServer + "user/SearchUser?searchText="; }
    static get SaveRole(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/SaveRole"; }
    static get UserName(): string { return this.apiServer + "Federation/UserName"; }
    static get GetUserGroupsByClaimRef(): string { return "http://localhost/Pars.Util.Puma.Api.AuthManagement/api/claim/GetUserGroupsByClaimRef"; }



}