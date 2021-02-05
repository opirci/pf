import { CoreHelper } from '../core';

/**
 * 
 */
export class DateWeek {

    //private readonly minYear: number = 1900;
    //private readonly minWeek: number = 1;

    //private readonly maxYear: number = 2100;
    //private readonly maxWeek: number = 52;

    constructor(public year: number, public week: number) {
        //if (year < this.minYear || year > this.maxYear || week < this.minWeek || week > this.maxWeek)
        //    throw new Error(`Out of range when constructing DateWeek year=${year}, week=${week}`);
    }

    public static now(): DateWeek {
        return this.fromDate(new Date(new Date(Date.now()).setHours(0, 0, 0, 0)));
    }

    public static fromDate(date: Date): DateWeek {
        return new DateWeek(date.getFullYear(), CoreHelper.getWeek(date));
    }

    /**
     * owrd
     * @returns {} 
     */
    get startDate(): Date {
        return this.getDateOfISOWeek(this.year, this.week);
    }

    /**
     * 
     * @returns {} 
     */
    get endDate(): Date {
        return new Date(this.startDate.getDate() + 7);
    }

    private getDateOfISOWeek(year: number, week: number): Date {
        const simple: Date = new Date(year, 0, 1 + (week - 1) * 7);
        const dow: number = simple.getDay();
        let ISOweekStart: Date = simple;
        if (dow <= 4)
            ISOweekStart.setDate(simple.getDate() - simple.getDay() + 1);
        else
            ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());
        return ISOweekStart;
    }


}