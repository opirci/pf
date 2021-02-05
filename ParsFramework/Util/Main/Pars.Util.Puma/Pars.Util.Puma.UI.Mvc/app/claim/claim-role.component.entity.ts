import { EntityState, IEntityState } from "../definition/entitystate";
import { LocaleData } from "../definition/localedata";
import { Lookup } from "../pars/data";

// import { MemberState } from "../definition/memberstate";
import { RequestBase, ResponseBase } from "../pars/service";
import { MemberState } from "../definition/memberstate";

export class GetRolesByClaimRefRequest extends RequestBase {
    value: number;
    pageNumber: number;
    pageSize: number;
}

export class GetRolesByClaimRefResponse extends ResponseBase {
    values: RoleRelation[];
    count: number;
}

export class GetRolesByStartingNameRequest {
    searchText: string;
}

export class GetRolesByStartingNameResponse extends ResponseBase {
    roles: Role[];
}

export class GetRolesByContainingNameRequest extends GetRolesByStartingNameRequest {
    searchText: string;
}

export class GetRolesByContainingNameResponse extends GetRolesByStartingNameResponse {
}

export interface IToLookup {
    toLookup(): Lookup;
}

export interface IAuditable {
    createdDate: Date;
    createdUserRef: number;
    modifiedDate: Date;
    modifiedUserRef: number;
}


export class Role implements IToLookup, IAuditable {
    roleRef: number;
    name: string;
    createdDate: Date;
    createdUserRef: number;
    modifiedDate: Date;
    modifiedUserRef: number;

    entityState: EntityState;
    localeData: LocaleData;

    constructor(roleRef: number, name: string, entityState: EntityState, localeData: LocaleData,
        createdDate: Date, createdUserRef: number, modifiedDate: Date, modifiedUserRef: number) {
        this.roleRef = roleRef;
        this.name = name;
        this.entityState = entityState;
        this.localeData = localeData;
        this.createdDate = createdDate;
        this.createdUserRef = createdUserRef;
        this.modifiedDate = modifiedDate;
        this.modifiedUserRef = modifiedUserRef;


    }

    public static createNew(): Role {
        return new Role(0, "", EntityState.createNew(), LocaleData.createNew(), new Date(), 0, new Date(), 0);
    }
    public toLookup(): Lookup {
        return new Lookup(this.roleRef, this.name);
    }


}

export class RoleRelation extends Role implements IEntityState {
    roleRef: number;
    relationRef: number;
    mappedToRef: number;
    hasChanged: boolean;
    validFrom: Date;
    validTo: Date;
    memberState: MemberState;

    public static createNew(): RoleRelation {
        return new RoleRelation(0, "", 0, 0, "Active", "Aktif", "", false, new Date(), 0, new Date(), 0);
    }

    constructor(roleRef: number, name: string, relationRef: number, entityStateRef: number,
        entityStateName: string, entityStateDescription: string, description: string, hasChanged: boolean,
        createdDate: Date,
        createdUserRef: number,
        modifiedDate: Date,
        modifiedUserRef: number
    ) {
        super(roleRef, name,
            {
                EntityStateRef: entityStateRef,
                Name: entityStateName,
                Description: entityStateDescription
            },
            new LocaleData(0, description, false),
            createdDate,
            createdUserRef,
            modifiedDate,
            modifiedUserRef

        );
        this.relationRef = relationRef;
        this.hasChanged = hasChanged;
    }
}



export class ConversionHelper {
    public static toLookup(data: IToLookup[]): Lookup[] {
        let list: Lookup[] = new Array<Lookup>();
        for (let item of data) {
            list.push(item.toLookup());
        }
        return list;
    };
}

