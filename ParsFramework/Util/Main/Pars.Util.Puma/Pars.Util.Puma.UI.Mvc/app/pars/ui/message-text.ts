import { Component, OnInit, forwardRef, AfterContentInit, Input, Inject } from '@angular/core';
import { ComponentBase } from './component-base';

@Component({
    moduleId: module.id,
    selector: "msg-text",
    //template: "",
    template: `<input type="hidden" name="{{name}}" id="{{name}}" value="{{value}}"/>`,
    providers: [ComponentBase]
})
export class MessageText implements OnInit, AfterContentInit {
    ngOnInit(): void {
        //console.log("Entered ngOnInit of MessageTextComponent");
        //this._parent.addMessageText(new MessageText(this.name, this.value));
    }

    ngAfterContentInit(): void {
        //console.log("Entered ngAfterContentInit of MessageTextComponent");
        //this._parent.addMessageText(new MessageText(this.name, this.value));
    }

    @Input() name: string;
    @Input() value: string;
    constructor( @Inject(forwardRef(() => ComponentBase)) private _parent: ComponentBase) {
        //console.log("Entered ctor of MessageTextComponent");
    }

    public toString = (): string => {
        return this.value;
    };
}



export class MessageData {
    constructor(public name: string, public value: string) { }
}
