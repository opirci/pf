export class WebApiEndPoints {
    public static get ClaimList(): string { return "https://puma.lcwaikiki.com/Web/api/claim/get"; }
    public static get EntityStates(): string { return "https://puma.lcwaikiki.com/Web/api/definition/entitystates"; }
    public static get ClaimPost(): string { return "https://puma.lcwaikiki.com/Web/api/claim/post"; }
    public static get RolesOfClaim(): string { return "https://puma.lcwaikiki.com/Web/api/claim/RolesOfClaim/?claimRef="; }
    public static get RolesByContainingName(): string { return "https://puma.lcwaikiki.com/Web/api/claim/RolesByContainingName/?text="; }
    public static get RolesByStartingName(): string { return "https://puma.lcwaikiki.com/Web/api/claim/RolesByStartingName/?text="; }
    public static get UserSearch(): string { return "https://puma.lcwaikiki.com/Web/api/user/SearchUser?searchText="; }
    public static get SaveRole(): string { return "https://puma.lcwaikiki.com/Web/api/claim/SaveRole"; }
    public static get UserName(): string { return "https://puma.lcwaikiki.com/Web/api/Federation/UserName"; }
    public static get apiServer(): string { return "https://puma.lcwaikiki.com/Web/api/"; }
    
}