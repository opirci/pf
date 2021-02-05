import { Injectable, ElementRef, EventEmitter } from "@angular/core";
import { SpinnerComponent } from '../../common';
import { IProgressService } from '../core';

export class ProgressManagerService implements IProgressService {

    componentRef: ElementRef;
    private _htmlElement: HTMLElement;
    private get htmlElement(): HTMLElement {
        if (!this._htmlElement)
            this._htmlElement = this.componentRef.nativeElement as HTMLElement;
        return this._htmlElement;
    }

    constructor(componentRef: ElementRef) {
        this.componentRef = componentRef;
    }

    private buttons: HTMLButtonElement[] = [];
    private spinners: HTMLImageElement[] = [];

    private getElementsOfSource(source: string): void {
        this.buttons = [];
        this.spinners = [];
        debugger;

        const elements = this.htmlElement.getElementsByClassName(source);
        if (elements.length > 0)
            for (let i = 0; i < elements.length; i++) {
                let element: Element = elements.item(i);

                if (element.localName == 'button') {
                    this.buttons.push(<HTMLButtonElement>element);
                    continue;
                }

                if (element.localName == 'spinner') {
                    let img = <HTMLImageElement>element.childNodes[0];
                    img.className = element.className;
                    this.spinners.push(img);
                    continue;
                }

                //if (element.localName == 'img') {
                //    if (element.className.indexOf('xspinner') != -1) //TODO xspinner dependency
                //        this.spinners.push(<HTMLImageElement>element);
                //    continue;
                //}
            }
    }

    start(source: string): void {
        this.getElementsOfSource(source);
        for (let button of this.buttons) {
            if (button.className.indexOf(source) != -1)
                button.disabled = true;
        }
        for (let spinner of this.spinners) {
            if (spinner.className.indexOf(source) != -1)
                spinner.style.setProperty("display", "block")
        }
    }
    stop(source: string): void {
        this.getElementsOfSource(source);
        for (let button of this.buttons) {
            if (button.className.indexOf(source) != -1) {
                button.disabled = false;
            }
        }
        for (let spinner of this.spinners) {
            if (spinner.className.indexOf(source) != -1)
                spinner.style.setProperty("display", "none")
        }
    }
}
