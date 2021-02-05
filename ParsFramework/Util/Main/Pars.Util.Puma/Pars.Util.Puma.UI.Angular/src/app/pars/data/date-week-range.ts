import  { DateWeek, Range } from '../data';
export { DateWeek } from '../data';

//Date.prototype.getWeek = function ()  {
//    let onejan = new Date(this.getFullYear(), 0, 1);
//    return Math.ceil((((this - onejan) / 86400000) + onejan.getDay() + 1) / 7);
//}

/**
 * 
 */
export class DateWeekRange extends Range<DateWeek> {

    /**
     * 
     */
    public static now(): DateWeekRange {
        const now = DateWeek.now();
        return new DateWeekRange(now, now);
    }
}
