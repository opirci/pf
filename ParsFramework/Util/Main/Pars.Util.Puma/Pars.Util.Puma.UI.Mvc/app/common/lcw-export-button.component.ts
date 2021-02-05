
import { Component, Input, Output, Inject, forwardRef, AfterViewInit, ElementRef, EventEmitter, ViewChild } from '@angular/core';
import { CoreHelper } from '../pars/core';
import { ExportFormat } from '../pars/data';


const selector = 'lcw-export-button';

@Component({
    moduleId: module.id,
    selector: selector,
    templateUrl: 'lcw-export-button.component.html',
})
export class LcwExportButtonComponent implements AfterViewInit {
    @Input() buttonStyle: string;
    @Input() buttonClass: string;
    @Input() itemStyle: string;
    @Input() itemClass: string;
    @Input() text: string;
    @Output() export: EventEmitter<ExportFormat> = new EventEmitter<ExportFormat>();
    exportAs: ExportFormat = ExportFormat.ExcelCsvSemiColumn;
    ExportFormat = ExportFormat;

    @ViewChild('btn') button: ElementRef;

    constructor(private _elementRef: ElementRef) {
    }

    ngAfterViewInit(): void {
        //const styl = this._elementRef.nativeElement.attributes["style"];
        //if (styl != null)
        //    this.buttonStyle = styl.value;

        //const cls = this._elementRef.nativeElement.attributes["class"];
        //if (cls != null)
        //    this.buttonClass = cls.value;

        if (this.text)
            this.button.nativeElement.textContent = this.text;
    }


    get exportButtonStyleInstance(): any {
        return (this.buttonStyle) ? CoreHelper.cssStyleTextToJson(this.buttonStyle) : undefined;
    }

    get exportButtonClassInstance(): any {
        return CoreHelper.cssClassTextToJson(this.buttonClass ? this.buttonClass : 'btn btn-default');
    }

    exportTo(): void {
        this.export.emit(this.exportAs);
    }

    exportAsClassInstance(format: ExportFormat): any {
        let cls: any = {};
        if (this.itemClass) {
            cls = CoreHelper.cssClassTextToJson(this.itemClass);
        }

        if (this.exportAs === format)
            cls.disabled = true;
        return cls;
    }

    exportAsStyleInstance(): any {
        return this.itemClass ? CoreHelper.cssStyleTextToJson(this.itemClass) : {};
    }

    setexportTo(format: ExportFormat): void {
        this.exportAs = format;
    }
}