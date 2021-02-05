//import { Directive, TemplateRef, ViewContainerRef, Input } from '@angular/core';
//import { LcwGridColumnComponent } from "./lcw-grid-column.component";

//@Directive({
//    // tslint:disable-next-line:directive-selector
//    selector: '[lcwGridHeaderInternal]'    
//})
//export class LcwGridHeaderInternalDirective {

//    constructor(
//        private templateTef: TemplateRef<any>,
//        private viewContainerRef: ViewContainerRef
//    ) { }

//    @Input() set lcwGridHeaderInternal(column: LcwGridColumnComponent) {

//        if (column && this.viewContainerRef) {
//            this.viewContainerRef.createEmbeddedView(column.templateTef);
//        }
//    }

//}
