
/**
 * 
 */
export class Range<T> {

    /**
     * 
     * @param from
     */
    public static from<T>(from: T): Range<T> {
        return new Range(from, null);
    }

    /**
     * 
     * @param until
     */
    public static until<T>(until: T): Range<T> {
        return new Range(null, until);
    }

    public static between<T>(from: T, until: T): Range<T> {
        return new Range(from, until);
    }

    /**
     * 
     * @returns {} 
     */
    get isFrom(): boolean {
        return this.start != null && this.end == null;
    }

    /**
     * 
     * @returns {} 
     */
    get isUntil(): boolean {
        return this.start == null && this.end != null;
    }

    /**
     * 
     * @returns {} 
     */
    get isBetween(): boolean {
        return this.start != null && this.end != null;
    }

    /**
     * 
     * @returns {} 
     */
    get isNoRange(): boolean {
        return this.start == null && this.end == null;
    }

    constructor(public start: T, public end: T) { }

}


