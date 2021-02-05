import { Component, Input, ElementRef, OnInit, ViewContainerRef } from "@angular/core";
import { CoreHelper } from '../pars/core'

export enum SpinnerSize {
    small = 20,
    medium = 50,
    large = 200
}

@Component({
    moduleId: module.id,
    selector: "spinner",
    template: `<img [hidden]='!enabled' class='xspinner' [ngClass]="classText" src="app/common/images/{{fileName}}"/>`
})
export class SpinnerComponent {
    @Input()
    enabled: boolean = false;
    @Input()
    size: SpinnerSize;
    spinnerSize: typeof SpinnerSize = SpinnerSize;
    classText: string;
    get fileName(): string {
        if (!this.size)
            this.size = SpinnerSize.small;
        var fileName = isNaN(this.size) ? this.spinnerSize[this.size] : +this.size;
        var ret = `spinner${fileName}.gif`;
        return ret;
    }

    constructor(private _elementRef: ElementRef, protected viewContainer: ViewContainerRef) {

    }

    ngOnInit() {
        let element: HTMLElement = this._elementRef.nativeElement;
        
        //console.warn(`classList.value ${element.classList}`)
        //console.warn(`classList.className ${element.className}`)
        this.classText = this._elementRef.nativeElement.classList.value;
    }
}

//import { Directive, Renderer, Host, ElementRef } from '@angular/core';

//@Directive({
//    selector: "[styled]"
//})
//export class StyledDirective {
//    constructor(public el: ElementRef, public renderer: Renderer, @Host() parent: Component) {
//        console.warn(parent.styles);
//        for (var style of parent.styles)
//            renderer.setElementStyle(el.nativeElement, "", style);
//    }
//}