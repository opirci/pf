import { CoreHelper } from '../core';

export enum EntityStates {
    None = 0,
    Detached = 1,
    Unchanged = 2,
    Added = 4,
    Deleted = 8,
    Modified = 16
}

export class DomainBase {
    HasChanged: boolean;
    DataEntityState: EntityStates;

    constructor(obj: any = null) {
        if (obj != null)
            CoreHelper.copyFromJsonObject(this, obj);
    }
}
