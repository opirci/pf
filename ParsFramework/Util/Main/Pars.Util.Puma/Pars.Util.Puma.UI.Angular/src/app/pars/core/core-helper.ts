import { Dictionary } from './dictionary';
import { TsType } from './tstype';


/**
 * Core utility functions helper class
 */
export class CoreHelper {
    private static clearFunction(text: string): string {
        if (text.indexOf("function") >= 0) {
            text = text.replace("function ", "");
            text = text.substring(0, text.indexOf("("));
        }
        return text;
    }

    private static clearObject(text: string): string {
        if (text.indexOf("[Object ") >= 0) {
            text = text.replace("[Object ", "");
            text = text.replace("]", "");
        }
        return text;
    }

    static nameOfInstance(instance: any): string {
        if (instance == null)
            return null;
        return this.clearFunction(this.clearObject(instance.constructor.name));
    }

    static nameOfType(instance: any): string {
        if (instance == null)
            return null;
        return this.clearFunction(this.clearObject(instance.toString()));
    }

    static copyFromJsonObject(instance: any, jsonObject: any): void {
        if (jsonObject != null)
            for (var prop in jsonObject)
                instance[prop] = jsonObject[prop];
    }

    //static getTypeName(inputClass: any): string {
    //    var funcNameRegex = /function (.{1,})\(/;
    //    var results = (funcNameRegex).exec((<any>inputClass).constructor.toString());
    //    return (results && results.length > 1) ? results[1] : "";
    //}

    static cssStyleTextToJson(text: string): any {
        if (text == null)
            return null;
        let styles: string[] = text.split(';'),
            i = styles.length,
            json: any = {},
            style: string[],
            k: string,
            v: string;

        while (i--) {
            style = styles[i].split(':');
            k = this.trim(style[0]);
            v = this.trim(style[1]);
            if (k.length > 0 && v.length > 0) {
                json[k] = v;
            }
        }
        return json;
    }

    static cssClassTextToJson(text: string): any {
        if (text == null)
            return null;
        text = text.replace('  ', ' ');
        let cssClass: string[] = text.split(' '),
            i = cssClass.length,
            json: any = {};
        for (let style of cssClass)
            json[style] = true;
        return json;
    }

    /**
     * Trims text
     * @param text text to trim
     */
    static trim(text: string): string {
        if (text != null) {
            var txt = "";
            for (var i = 0; i < text.length; i++) {
                if (text[i] == " ")
                    continue;
                txt += text[i];
            }
            text = txt;
            txt = "";
            for (var i = text.length - 1; i >= 0; i--) {
                if (text[i] == " ")
                    continue;
                txt = text[i] + txt;
            }
            text = txt;
        }
        return text;

        // Make sure we trim BOM and NBSP
        //var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;

        //return text.replace(rtrim, "");

    }

    /**
     * Returns text as separate words from a camel case word
     * @param propName
     */
    static camelCaseToWords(text: string): string {
        if (text == null)
            return null;
        return text
            .replace(/([A-Z])/g, ' $1')
            .replace(/^./, (str: string): string => str != null ? str.toUpperCase() : null);
    }

    static getWeek(dt: Date): number {
        //var numDate = date.valueOf();
        //let tdt = new Date(numDate);
        //var dayn = (date.getDay() + 6) % 7;
        //tdt.setDate(tdt.getDate() - dayn + 3);
        //var firstThursday = tdt.valueOf();
        //tdt.setMonth(0, 1);
        //if (tdt.getDay() !== 4) {
        //    tdt.setMonth(0, 1 + ((4 - tdt.getDay()) + 7) % 7);
        //}
        //return 1 + Math.ceil((firstThursday - numDate) / 604800000);

        var tdt = new Date(dt.valueOf());
        var dayn = (dt.getDay() + 6) % 7;
        tdt.setDate(tdt.getDate() - dayn + 3);
        var firstThursday = tdt.valueOf();
        tdt.setMonth(0, 1);
        if (tdt.getDay() !== 4) {
            tdt.setMonth(0, 1 + ((4 - tdt.getDay()) + 7) % 7);
        }
        return 1 + Math.ceil((firstThursday - tdt.valueOf()) / 604800000);
    }

    static readonly dateMin: Date = new Date(946674000000);

    static isBoolean(value: any): boolean {
        return typeof value === "boolean";
    }

    static isNumber(value: any): boolean {
        return !isNaN(parseFloat(value)) && isFinite(value);
    }

    //static isNumber(value: any): boolean {
    //    if (typeof value === "number")
    //        return true;
    //    const num = Number(value);
    //    return !isNaN(num) && typeof num === "number";
    //}
    /**
     * Returns true if value is a valid Date
     * @param value value to check
     */
    static isDate(value: any): boolean {
        //if (typeof value === "date")
        //    return true;
        let date: Date = new Date(Date.parse(value))
        return date.toString() != 'Invalid Date' && date != this.dateMin;
    }

    /**
     * 
     * @param value
     */
    static getTsType(value: any): TsType {
        if (value == null || value == undefined)
            return TsType.undefined;

        let ret: TsType;
        if (this.isBoolean(value)) {
            ret = TsType.boolean;
        }
        else if (this.isNumber(value)) {
            ret = TsType.number;
        } else if (this.isDate(value)) {
            ret = TsType.date;
        } else if (typeof value === "string") {
            if (this.isNumber(value)) {
                ret = TsType.number;
            }
            else if (this.isDate(value)) {
                ret = TsType.date;
            }
            else {
                ret = TsType.string;
            }

        } else if (typeof value === "object") {
            ret = TsType.any;
        }
        return ret;
    }

    static checkWithParentValue<T>(parentValue: T, value: T, defaultValue: T): T {
        if (value == undefined || value == null) {
            if (parentValue == undefined || parentValue == null) {
                return defaultValue;
            }
            else {
                return parentValue;
            }
        }
        return value;
    }
    
    /**
     * Returns keys and values in a text marked with separators
     * @param text Text to separate values from     
     * @param keyValueSeparator Separator char between keys and values
     * @param itemSepatator Separator char between items
     */
    static asDictionary(text: string, keyValueSeparator: string, itemSepatator: string): Dictionary<string, string> {
        let dict: Dictionary<string, string> = new Dictionary<string, string>();
        var pairs: string[] = text.split(itemSepatator);
        for (var pair of pairs) {
            var arr = pair.split(keyValueSeparator);
            dict.add(arr[0], arr[1]);
        }
        return dict;
    }

    static compareArray(array1: any[], array2: any[]): boolean {
        if (array1 == null || array2 == null)
            return false;
        if (array1.length !== array2.length)
            return false;
        for (let i = 0; i < array1.length; i++) {
            if (!this.compareObject(array1[i], array2[i]))
                return false;
        }
        return true;
    }

    static compareObject(obj1: any, obj2: any): boolean {
        let equals: boolean = true;
        for (let prop in obj1) {
            if (obj1.hasOwnProperty(prop)) {
                if (!obj2.hasOwnProperty(prop) || (obj1[prop] != obj2[prop])) {
                    equals = false;
                    break;
                }
            }
        }
        return equals;
    }

    static hasEnum(enum1: any, enum2: any): boolean {
        return (enum1 != null && enum2 != null && ((<number>enum1) | (<number>enum2)) === <number>enum1);
    }


}