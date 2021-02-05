import { Component, OnInit, } from '@angular/core';
import { MessageData } from './message-text';


@Component({
    moduleId: module.id,
    template: ""
})
export class ComponentBase implements OnInit {

    protected messages: Array<MessageData> = [];

    constructor() {
        //console.log("Entered ctor of ComponentBase");
    }

    templateText(key: string): string {
        var element: HTMLInputElement = <HTMLInputElement>document.getElementById(key);
        if (element != null)
            return element.value;
        return null;
        //console.log("Entered templateText of ComponentBase");

        //var result: MessageText[] = this.messageTexts.filter(text => text.name === key);
        //if (result && result.length > 0) {
        //    return result[0].value;
        //}
        //var msg: string = `${key}' not found for in message texts`;
        //console.warn(msg);
        //return null;
    }

    addMessageText(messageText: MessageData): void {
        //console.log("Entered addMessageText of ComponentBase");
        if (!this.messages.some(text => text.name ===
            messageText.name))
            this.messages.push(messageText);
        else {
            console.warn(`Multiple message text key added '${messageText.name}'`);
        }
    }

    ngOnInit(): void {
        //this.messageTexts = new Array<MessageText>();
        //console.log("Entered ngOnInit of ComponentBase");
    }
}