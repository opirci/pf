import { EntityState } from '../definition/entitystate'
import { LocaleData } from '../definition/localedata'

export class ClaimListResponse {
    Claims: Claim[];
    Count: number;
}

export class Claim {
    ClaimRef: number;
    EntityState: EntityState;
    LocaleData: LocaleData;
    Name: string;
    HasChanged: boolean;

    constructor(claimref,entityStateRef,entityStateName,name,hasChanged) {
        this.ClaimRef = claimref;
        this.EntityState = new EntityState();
        this.EntityState.EntityStateRef = 1;
        this.EntityState.Name = "Active";
        this.EntityState.Description = "Aktif";
        this.Name = name;
        this.HasChanged = hasChanged;
        this.LocaleData = new LocaleData(0, '', false);
    }
}
