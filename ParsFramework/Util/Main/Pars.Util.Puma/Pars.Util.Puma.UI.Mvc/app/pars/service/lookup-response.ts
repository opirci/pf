import { Lookup } from '../data';
import { ResponseBase } from './response-base';
export { Lookup } from '../data';
export { ResponseBase } from './response-base';


//export class LookupResponse extends ResponseBaseGeneric<data.Lookup[]> {}

export class LookupResponse extends ResponseBase {
    values: Lookup[];

    constructor(values: Lookup[] = null) {
        super();
        this.values = values;
    }
}