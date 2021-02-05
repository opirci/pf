
import { Lookup } from '../data';
export { Lookup } from '../data';

export class LookupList {

    constructor(
        public defaultSelectMember: boolean = true,
        public selectedValue = new Lookup(),
        public values: Lookup[] = [],
        public readState: ReadState = ReadState.NotRead
    ) { }

    clearSelectedValue(): void { this.selectedValue = new Lookup(); }

    get isReading(): boolean { return this.readState === ReadState.Reading; }
}

export enum ReadState {
    NotRead,
    Reading,
    Read,
    Error
}