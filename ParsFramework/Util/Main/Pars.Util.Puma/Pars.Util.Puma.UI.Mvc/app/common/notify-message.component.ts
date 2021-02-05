import { Injectable } from '@angular/core';
import { SimpleNotificationsComponent, NotificationsService } from 'angular2-notifications';
import { IMessageShowService, MessageType } from '../pars/core';

@Injectable()
export class NotifyMessage implements IMessageShowService {
    constructor(private notifyService: NotificationsService) {

    }

    createMessage(nType: NotifyMessageType, messageHeader, messageContent) {
        switch (nType) {
            case NotifyMessageType.Success:// "Success":
                let a = this.notifyService.success(messageHeader, messageContent, { id: 123 });
                break;
            case NotifyMessageType.Alert: // "Alert":
                this.notifyService.alert(messageHeader, messageContent);
                break;
            case NotifyMessageType.Error: // "Error":
                this.notifyService.error(messageHeader, messageContent);
                break;
            case NotifyMessageType.Info: // "Info":
                this.notifyService.info(messageHeader, messageContent);
                break;
            case NotifyMessageType.Bare: // "Bare":
                this.notifyService.bare(messageHeader, messageContent);
                break;
        }
    }

    showMessage(title: string, message: string, type: MessageType): void {       
        let itype: NotifyMessageType;
        switch (type) {
            case MessageType.Error: itype = NotifyMessageType.Error;
                break;
            case MessageType.Info: itype = NotifyMessageType.Info;
                break;
            case MessageType.Success: itype = NotifyMessageType.Success;
                break;
            case MessageType.Warning: itype = NotifyMessageType.Info;
                break;
        }

        this.createMessage(itype, title, message);
    }
}

export enum NotifyMessageType {
    Success = 1,
    Alert,
    Error,
    Info,
    Bare
}