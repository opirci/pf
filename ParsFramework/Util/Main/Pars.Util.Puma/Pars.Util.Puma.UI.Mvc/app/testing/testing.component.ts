//import { Component, ElementRef, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
//import { Http } from '@angular/http';
//import { XHttpService, DataService, DataServiceExecuter, ResponseBase } from '../pars/service';
//import { MessageType, IMessageShowService, MessageService, IMessageService, IProgressService, IProgressMessage, ProgressManagerService } from '../pars/core';
//import { NotifyMessage } from "../common";
//import { Observable } from 'rxjs/Observable';
//import 'rxjs/add/operator/map';

//import { WebApiEndPoints } from "../settings/webapiendpoints";

//import { ComponentOptions } from '../settings/componentOptions';


//@Component({
//    moduleId: module.id,
//    selector: 'testing',
//    templateUrl: 'testing.component.html',
//    providers: [XHttpService, NotifyMessage]
//})
//export class TestingComponent implements OnInit {

//    notifyOptions = ComponentOptions.NotifyMessageOptions;

//    private spinner: ProgressManagerService;
//    private executer: DataServiceExecuter<MyService>;

//    constructor(protected xhttp: XHttpService,
//        private notify: NotifyMessage,
//        private elementRef: ElementRef,
//        protected viewContainer: ViewContainerRef) {

       

//        //let service: MyService = new MyService(this.xhttp, this.spinner, this.notify);
//        //let serExc = new DataServiceExecuter(service);
//        //serExc.execute.testIt().subscribe();
//        //service.deleteData("param").subscribe();
//        //service.deleteData("param").subscribe();
//    }

//    ngOnInit() {
//        debugger;

//        this.spinner = new ProgressManagerService(this.elementRef);
//        this.executer = MyService.create(MyService, this.xhttp, this.spinner, this.notify);
       

//        const i = this.viewContainer.length;
//    }

//    list()
//    {
//        debugger;
//        this.executer.executeWith({ successMessage: "success message", errorMessage: "error message" }).testIt().subscribe();
//    }
//}

//export class MyService extends DataService {
//    private apiUrlBase: string = WebApiEndPoints.apiServer;
//    constructor(public xhttp: XHttpService, startStopService: IProgressService, uIMessageService: IMessageShowService) {
//        super(xhttp, startStopService, uIMessageService);
//    }

//    testIt(): Observable<TestResponse> {
//        //arguments.callee.caller.toString()
//        return this.xhttp.getAs<TestResponse>("testIt", this.apiUrlBase+'test/testit');
//    }

//    deleteData(param: string): Observable<string> {
//        return this.xhttp.getAs<string>("deleteData", "url", "data");
//    }
//}

//export class TestResponse extends ResponseBase {
//    Name: string;
//}
//{

//}
