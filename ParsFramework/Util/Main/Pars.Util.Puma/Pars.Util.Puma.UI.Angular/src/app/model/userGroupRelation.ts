import { MemberState } from '../definition/memberState'

export class UserGroupRelation {
    UserGroupRef: number;
    Name: string;
    Description: string;
    MemberState: MemberState;
    ValidFrom: Date;
    ValidTo: Date;
}