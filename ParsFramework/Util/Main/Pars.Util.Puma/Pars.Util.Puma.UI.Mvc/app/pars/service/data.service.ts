import { OnDestroy } from '@angular/core';
import "rxjs/add/operator/map";

import {
    MessageService, IMessageService, IProgressService,
    IProgressMessage, IMessage, IMessageShowService, MessageType
} from '../core';

import { XHttpService } from './xhttp.service';

export class DataService implements OnDestroy {

    protected messageService: IMessageService;
    messageContext: IProgressMessage;

    protected param: string;

    constructor(
        public xhttp: XHttpService,
        protected progressService: IProgressService = null,
        protected messageShowService: IMessageShowService = null) {
        this.messageService = this.xhttp.messageService;
        this.ngOnInit();
    }

    ngOnInit() {
        this.messageService.onSend
            .map((msg: IMessage) => {
                debugger;
                if (msg != null) {
                    switch (msg.action) {
                        case this.xhttp.actions.start:
                            this.progressService!.start(msg.source);
                            break;
                        case this.xhttp.actions.end:
                            this.progressService!.stop(msg.source);
                            break;
                        case this.xhttp.actions.success:
                            this.progressService!.stop(msg.source);
                            this.messageShowService.showMessage("", msg.message, MessageType.Success);
                            break;
                        case this.xhttp.actions.error:
                            this.progressService!.stop(msg.source);
                            this.messageShowService.showMessage("", msg.message, MessageType.Error);
                            break;
                        case this.xhttp.actions.warning:
                            this.progressService!.stop(msg.source);
                            this.messageShowService.showMessage("", msg.message, MessageType.Warning);
                            break;
                        default:
                            throw new Error(`Unimplemented Service action ${msg.action}`);                            
                    }
                }
            }).subscribe();
    }

    ngOnDestroy() {
        this.messageService.onSend.unsubscribe();
    }

    //new(xhttp: XHttpService, startStopService: IStartStopService, uIMessageService: IUIMessageService)=> T
    //{ new (xhttp: XHttpService, startStopService: IStartStopService, uIMessageService: IUIMessageService): T; }

    static create<T extends DataService>(x: { new (xhttp: XHttpService, progressService: IProgressService, messageShowService: IMessageShowService): T; },
        xhttp: XHttpService, progressService: IProgressService, messageShowService: IMessageShowService): DataServiceExecuter<T> {
        let xy = new x(xhttp, progressService, messageShowService);
        return new DataServiceExecuter<T>(xy);
    }
}

export class DataServiceExecuter<T extends DataService>
{
    constructor(private service: T)
    { }

    get execute(): T {
        return this.service;
    }

    executeWith(message: IProgressMessage): T {
        this.service.xhttp.messageContext = message;
        this.service.messageContext = message;

        return this.service;
    }
}

