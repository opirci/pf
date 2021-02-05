
export interface IPaging {
    pageNumber: number;
    totalCount: number;
}


export class NotifiableArray<T> extends Array<T>
{
    dirty: boolean = false;

    push(...elem: T[]): number {
        this.dirty = true;
        return super.push(...elem);
    }

    pop(): T {        
        this.dirty = true;
        return super.pop();
    }
}

/**
 * Array extra storing state of values with paging values
 */
export class PagedList<T> extends NotifiableArray<T>
    //extends StatedList<T, boolean>
    implements IPaging {    

    constructor(array: T[],
        public pageNumber: number,
        public totalCount: number = 0) {
        super();
        if (array)
            for (var item of array) {
                super.push(item);
            }
    }

    toJSON(): any {
        return this.slice();
    }

    /**
     * Checks values with old values and marks updated ones
     * @param valuesBefore array of values before 
     */
    markUpdateds(valuesBefore: T[]): void {
        if (!(valuesBefore && valuesBefore.length === this.length))
            return;

        //for (let i in valuesBefore) {
        //    if (!CoreHelper.compareObject(valuesBefore[i], this[i])) {
        //        this.states[i] = true;
        //    }

        //}
    }

    /**
    * Array extra storing state of values
    */
    //export class StatedList<T, TS> extends Array<T> {

    //    /**
    //     * States of members
    //     */
    //    states: Array<TS> = [];

    //    constructor() {
    //        super();
    //    }

    //    /**
    //     * Pushes value with state
    //     * @param value value to push
    //     * @param state state of value
    //     */
    //    public pushWithState(value: T, state: TS): number {
    //        this.states.push(state);
    //        return super.push(value);
    //    }
    //}
}