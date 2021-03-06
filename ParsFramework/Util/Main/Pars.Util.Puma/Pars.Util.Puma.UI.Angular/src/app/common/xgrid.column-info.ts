﻿import { CoreHelper, Dictionary, TsType } from "../pars/core";
import { Row } from "../pars/data";
import { } from '../pars/core';
import { XGridColumnComponent } from "./xgrid.xcolumn.component";

export enum BooleanViews {
    checkBox,
    image,
    text
}

export class ColumnInfo {

    checkBooleanView(parentValue: BooleanViews): BooleanViews {
        return CoreHelper.checkWithParentValue(parentValue, this.booleanView, BooleanViews.checkBox);
    }

    get format(): DateFormat {
        let ret: DateFormat = DateFormat.dateTime;
        switch (this.formatText) {
            case DateFormat[DateFormat.date].toString():
                ret = DateFormat.date;
                break;
            case DateFormat[DateFormat.time].toString():
                ret = DateFormat.time;
                break;
            case DateFormat[DateFormat.dateTime].toString():
                ret = DateFormat.dateTime;
                break;
        }
        return ret;
    }

    set format(value: DateFormat) {
        let ret: DateFormat = DateFormat.dateTime;
        switch (value) {
            case DateFormat.date:
                this.formatText = DateFormat[DateFormat.date].toString();
                break;
            case DateFormat.time:
                this.formatText = DateFormat[DateFormat.time].toString();
                break;
            case DateFormat.dateTime:
                this.formatText = DateFormat[DateFormat.dateTime].toString();
                break;
        }
    }

    get headerStyleInstance(): any {
        return CoreHelper.cssStyleTextToJson(this.headerStyleText || this.defaultHeaderStyleText);
    }

    get valueStyleInstance(): any {
        return CoreHelper.cssStyleTextToJson(this.valueStyleText || this.defaultValueStyleText);
    }

    static readonly typeOfMethod: string = "method";

    defaultHeaderStyleText: string = null;
    defaultValueStyleText: string = null;

    constructor(
        public columnIndex: number,
        public headerName: string,
        public fieldName: string,
        private formatText: string,
        public conversions: string,
        public columnOrder: number,
        public isHidden: boolean,
        public isEditable: boolean,
        public dataType: TsType,
        public dataTypeName: string,
        public htmlContent: any,
        public headerStyleText: string,
        public valueStyleText: string,
        public booleanView: BooleanViews,
        public headerPopoverName: string,
        public columnComponent: XGridColumnComponent
    ) {

    }

    clone(): ColumnInfo {
        return new ColumnInfo(
            this.columnIndex,
            this.headerName,
            this.fieldName,
            this.formatText,
            this.conversions,
            this.columnOrder,
            this.isHidden,
            this.isEditable,
            this.dataType,
            this.dataTypeName,
            this.htmlContent,
            this.headerStyleText,
            this.valueStyleText,
            this.booleanView,
            this.headerPopoverName,
            this.columnComponent
        );
    }

    public toString = (): string => {
        var json: string = JSON.stringify(this);
        return `ColumnInfo { ${json} }`;
    }

    static discoverColumnType(value: any, columnInfo: ColumnInfo): boolean {
        let val: any = this.getColumnValue(value, columnInfo, false);
        if (!columnInfo.dataType || (columnInfo.dataType && columnInfo.dataType == TsType.undefined)) {
            columnInfo.dataType = CoreHelper.getTsType(val);
        }
        return columnInfo.dataType != TsType.undefined;
    }

    static getColumnValue(data: any, columnInfo: ColumnInfo, formated: boolean): any {
        var col = columnInfo.clone();
        if (col.fieldName.indexOf(".") > 0) {
            let firstNode: string = col.fieldName.split(".")[0];
            col.fieldName = col.fieldName.replace(firstNode + ".", "");
            return this.getColumnValue(this.getMemberValue(data, null, firstNode), col, formated);
        } else {
            return this.getMemberValueFormated(data, col, formated);
        }
    }

    static getMemberValue(data: any, columnInfo: ColumnInfo, memberName: string = null): any {
        let value: any = "ERROR";

        if (memberName == null)
            memberName = columnInfo.fieldName;

        let row: Row = data as Row;

        if (!(data instanceof Row) || row == null) {
            value = data[memberName];
        } else {
            //value = row.values[columnInfo.columnOrder - 1];
            value = row.values[columnInfo.columnIndex];
        }
        return value;
    }

    static getMemberValueFormated(data: any, columnInfo: ColumnInfo, formated: boolean): any {
        let value: any = this.getMemberValue(data, columnInfo);
        value = formated ? FormatChecker.check(value, columnInfo) : value;
        return value;
    }
}

//#region FormatCheckers

interface ICheckFormat {
    check(value: any, columnInfo: ColumnInfo): any;
}

class FormatChecker {
    //static checkers: ICheckFormat[] = [new BooleanFormatChecker(), new TestFormatChecker()];
    private static checkers: Array<ICheckFormat> = new Array<ICheckFormat>();

    private static isInited: boolean = false;

    private static init() {
        this.checkers.push(new BooleanFormatChecker());
        //this.checkers.push(new TestFormatChecker());
        this.checkers.push(new DateFormatChecker());
    }

    public static check(value: any, columnInfo: ColumnInfo): any {
        if (!this.isInited) {
            this.init();
            this.isInited = true;
        }

        var ret: string = null;
        if (columnInfo.conversions && columnInfo.conversions != null) {
            let conversions: Dictionary<string, string> = CoreHelper.asDictionary(columnInfo.conversions, "=", ";");

            let valStr: string = value == null ? "" : value.toString();
            let foundPair = conversions.get(valStr);
            ret = foundPair != null ? foundPair.value : valStr;
        }
        else {
            for (var checker of this.checkers) {
                ret = checker.check(value, columnInfo);
                if (ret != null)
                    break;
            }
        }
        return ret != null ? ret : value;
    }
}

class BooleanFormatChecker implements ICheckFormat {
    public check(value: any, columnInfo: ColumnInfo): any {
        if (!(columnInfo.dataType && columnInfo.dataType != TsType.boolean)) {
            if (typeof (value) == "boolean") {
                if (value === true) {
                    return "True";
                } else {
                    return "False";
                }
            }
        }
        return null;
    }
}

export enum DateFormat {
    date,
    time,
    dateTime
}

class DateFormatChecker implements ICheckFormat {

    public check(value: any, columnInfo: ColumnInfo): any {
        if ((columnInfo.dataType == undefined || columnInfo.dataType == null) || columnInfo.dataType != TsType.date)
            return value;
        if (!CoreHelper.isDate(value))
            return value;
        let dateVal: Date = new Date(value);
        let ret: string = "";
        switch (columnInfo.format) {
            case DateFormat.date:
                ret = dateVal.toLocaleDateString();
                break;
            case DateFormat.time:
                ret = dateVal.toLocaleTimeString();
                break;
            case DateFormat.dateTime:
                ret = dateVal.toLocaleString();
                break;
        }
        return ret;
    }
}

class TestFormatChecker implements ICheckFormat {

    public check(value: any, columnInfo: ColumnInfo = null): any {
        //if (typeof (value) === "number")
        //    return +value + "Hoo";
        return null;
    }
}

//#endregion