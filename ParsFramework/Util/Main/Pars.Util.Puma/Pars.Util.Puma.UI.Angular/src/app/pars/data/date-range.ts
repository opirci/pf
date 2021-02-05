

/**
 * 
 */
export class DateRange {
    /**
     * 
     * @param from
     */
    static from(from: Date): DateRange {
        return new DateRange(from, null);
    }

    /**
     * 
     * @param until
     */
    static until(until: Date): DateRange {
        return new DateRange(null, until);
    }

    /**
     * 
     * @param from
     * @param until
     */
    static between(from: Date, until: Date): DateRange {
        return new DateRange(from, until);
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

    constructor(public start: Date, public end: Date) { }
}

