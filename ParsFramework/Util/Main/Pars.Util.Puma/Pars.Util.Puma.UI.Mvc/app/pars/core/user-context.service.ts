
import { Injectable } from '@angular/core';

export class UserContext {
    userName: string;
    domain: string;
    culture: string;
    languageRef: number;
    hrPersonelRef: number;
    userRef: number;
    claims: string[];

    constructor(context: UserContext) {
        this.userName = context.userName;
        this.domain = context.domain;
        this.culture = context.culture;
        this.languageRef = context.languageRef;
        this.hrPersonelRef = context.hrPersonelRef;
        this.userRef = context.userRef;
        this.claims = context.claims;
    }


    public hasClaim(claim: string): boolean {
        return this.claims.some(s => s === claim);
    }

    public hasAnyClaims(claims: string[]): boolean {
        return this.claims.some(s => claims.some(s1 => s === s1));
    }

    public hasAllClaims(claims: string[]): boolean {
        return this.claims.every(s => this.claims.some(s1 => s === s1));
    }
}



@Injectable()
export class UserContextService {
    private _userContext: UserContext = null;

    public get userContext(): UserContext {
        if (this._userContext === null) {
            this._userContext = new UserContext(document["userContext"]);
        }
        return this._userContext;
    }

    public hasClaim(claim: string): boolean {
        return this.userContext.claims.some(s => s === claim);
    }

    public hasAnyClaims(claims: string[]): boolean {
        return this.userContext.claims.some(s => claims.some(s1 => s === s1));
    }

    public hasAllClaims(claims: string[]): boolean {
        return claims.every(s => this.userContext.claims.some(s1 => s === s1));
    }
}