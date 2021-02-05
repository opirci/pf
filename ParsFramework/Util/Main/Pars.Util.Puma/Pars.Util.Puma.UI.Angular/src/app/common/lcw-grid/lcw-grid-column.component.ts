import {
    Component, Input, Inject, forwardRef, AfterViewInit, ElementRef, Renderer
} from "@angular/core";
import { ColumnInfo, BooleanViews } from "./lcw-grid.column-info";
import { LcwGridComponent } from "./lcw-grid.component";
import { TsType } from "../../pars/core";

@Component({
    moduleId: module.id,
    selector: "lcw-grid-column",
    template: "<ng-content></ng-content>",

})
export class LcwGridColumnComponent implements AfterViewInit {

    private calculateOrder(corder: number = -1): number {
        if (corder == -1)
            corder = this.order > 0 ? this.order : this._parent.columnList.length + 1;
        if (this._parent.columnList.some(c => c.columnOrder == corder))
            this.calculateOrder(corder + 1);
        return corder;
    }

    ngAfterViewInit() {
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
            this.group,
            this
        );

        this._parent.columnListFromMarkup.push(col);
        this._parent.xcolumnList.push(this);
        let textNode = this._elementRef.nativeElement.childNodes[0];
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
    @Input() group: string = null;
    constructor(
        @Inject(forwardRef(() => LcwGridComponent)) private _parent: LcwGridComponent,
        private _elementRef: ElementRef,
        private _renderer: Renderer,
        //private templateRef: TemplateRef<any>
    ) {

    }

    //get template(): TemplateRef<any>
    //{
    //    return this.templateRef;
    //}

    get hasTemplate(): boolean {
        return false;
        //return this.templateRef.elementRef ? true : false;
    }
}

