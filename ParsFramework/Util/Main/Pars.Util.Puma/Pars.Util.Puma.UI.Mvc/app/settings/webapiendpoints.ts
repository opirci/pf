export class WebApiEndPoints {
    private static mAppUrl = document["appUrl"];
    static get apiServer(): string { return this.mAppUrl + "api/"; }
    static get ClaimList(): string { return this.apiServer + "claim/get"; }
    static get EntityStates(): string { return this.apiServer + "definition/entitystates"; }
    static get MemberStates(): string { return this.apiServer + "definition/memberStates"; }

    static get ClaimPost(): string { return this.apiServer + "claim/post"; }
    static get RolesOfClaim(): string { return this.apiServer + "claim/RolesOfClaim/?claimRef="; }
    static get RolesByContainingName(): string { return this.apiServer + "claim/RolesByContainingName/?text="; }
    static get RolesByStartingName(): string { return this.apiServer + "claim/RolesByStartingName/?text="; }
    static get UserSearch(): string { return this.apiServer + "user/SearchUser?searchText="; }
    static get SaveRole(): string { return this.apiServer + "claim/SaveRole"; }
    static get UserName(): string { return this.apiServer + "Federation/UserName"; }
    static get GetUserGroupsByClaimRef(): string { return this.apiServer + "claim/GetUserGroupsByClaimRef"; }



}