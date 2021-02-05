import { Injectable, ElementRef, EventEmitter, OnInit } from "@angular/core";

export interface IMessageService {
    onSend: EventEmitter<IMessage>;
    // source: "getData", action:"start"
    // message: "Error when loading", source: "getData", action:"error"
    send(message: string, source: string, action: any): void;
}

@Injectable()
export class MessageService implements IMessageService {
    onSend: EventEmitter<IMessage> = new EventEmitter<IMessage>();

    send(message: string, source: string, action: any): void { 
        this.onSend.emit({ message: message, source: source, action: action });
    }
}

export interface IMessage {
    message: string;
    source: string;
    action: any;
}

