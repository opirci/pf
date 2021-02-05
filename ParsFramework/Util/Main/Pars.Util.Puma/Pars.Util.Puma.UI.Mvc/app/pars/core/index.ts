
//if (!String.prototype.trim) {
//    (() => {
//        // Make sure we trim BOM and NBSP
//        var rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g;
//        String.prototype.trim = function () {
//            return this.replace(rtrim, '');
//        };
//    })();
//}


//export { LogType, Loggers, LogItem, ILogger, ConsoleLogger, LocalStoreLogger, Logger } from './logger';
//export { Dictionary, KeyValuePair } from './dictionary';
//export { UserContext, UserContextService } from './user-context.service';
//export { CoreHelper } from './core-helper';
//export { ComponentBase } from '../component/component-base';
//export { MessageText, MessageData } from '../component/message-text';





////Object.prototype["nameOfInstance"] = (instance: any): string => {
////    if (instance == null)
////        return null;
////    var txt: string = instance.constructor.name;
////    txt = txt.replace("function ", "");
////    txt = txt.substring(0, txt.indexOf("("));
////    return txt;
////}

////Object.prototype["nameOfType"] = (instance: any): string => {
////    if (instance == null)
////        return null;
////    var txt: string = instance.toString();
////    txt = txt.replace("[Object ", "");
////    txt = txt.replace("]", "");
////    return txt;
////}


////export interface IJsonMetaData<T> {
////    name?: string,
////    clazz?: { new (): T }
////}


////http://stackoverflow.com/questions/22885995/how-do-i-initialize-a-typescript-object-with-a-json-object
////https://github.com/JohnWeisz/TypedJSON
////Object.assign(new Foo(), JSON.parse(fooJson)); http://stackoverflow.com/questions/29758765/json-to-typescript-class-instance
//export class Activator {
//    static createInstance<T>(creator: { new (): T; }): T {
//        return new creator();
//    }
//}

//export class InstanceLoader {
//    getInstance<T>(name: string, ...args: any[]): T {
//        var instance = Object.create(this.context[name].prototype);
//        instance.constructor.apply(instance, args);
//        return <T>instance;
//    }

//    constructor(private context: Object) { }
//}










