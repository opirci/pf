import {
    Component, Input, Inject, forwardRef, AfterContentInit, AfterViewInit, ElementRef,
    Renderer, ViewChildren, ContentChild, QueryList, ContentChildren, TemplateRef
} from "@angular/core";
import { ColumnInfo, BooleanViews } from "./xgrid.column-info";
import { XGridComponent } from "./xgrid.component";
import { CoreHelper, TsType } from "../pars/core";

@Component({
    moduleId: module.id,
    selector: "xcolumn,xgrid-column",
    template: "<ng-content></ng-content>"
})
export class XGridColumnComponent implements AfterContentInit {

    private calculateOrder(corder: number = -1): number {
        if (corder == -1)
            corder = this.order > 0 ? this.order : this._parent.columnList.length + 1;
        if (this._parent.columnList.some(c => c.columnOrder == corder))
            this.calculateOrder(corder + 1);
        return corder;
    }

    ngAfterContentInit(): void {
       
    }

    ngAfterViewInit()
    {
        if (this._parent.onColumnInitializing) {
            this._parent.onColumnInitializing(this);
        }

        const styl = this._elementRef.nativeElement.attributes["style"];
        const valueStyle = styl == null ? null : styl.value;
        let corder: number = this.calculateOrder();
        let col: ColumnInfo = new ColumnInfo(
            null,
            this.headerName,
            this.fieldName,
            this.format,
            this.conversions,
            corder,
            this.isHidden,
            this.isEditable ? this.isEditable : this._parent.isEditable,
            this.dataType ? this.dataType : null,
            null,
            this._elementRef.nativeElement,
            this.headerStyle,
            valueStyle,
            this.booleanView ? this.booleanView : this._parent.booleanView,
            this.headerPopoverName,
            this
        );

        this._parent.columnListFromMarkup.push(col);
        this._parent.xcolumnList.push(this);
        //debugger;
        let textNode = this._elementRef.nativeElement.childNodes[0];
        //let conChild = this.templateRef;
        //this._renderer.set
    }

    @Input() headerName: string;
    @Input() fieldName: string;
    @Input() format: string;
    @Input() conversions: string;
    @Input() order: number;
    @Input() isHidden: boolean = false;
    @Input() isEditable: boolean = false;
    @Input() dataType: TsType;
    @Input() booleanView: BooleanViews = BooleanViews.checkBox;
    @Input() headerStyle: string = null;
    @Input() headerPopoverName: string = null;
    constructor(
        @Inject(forwardRef(() => XGridComponent)) private _parent: XGridComponent,
        private _elementRef: ElementRef,
        private _renderer: Renderer
    ) {
    }
}