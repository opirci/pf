
import { CoreHelper } from './core-helper';

export enum LogType {
    Log,
    Warning,
    Error
}

export enum Loggers {
    Console = 1,
    LocalStore = 2,
    Hada = 4
}

export class LogItem {
    public timeStamp: number;

    constructor(
        public logKey: string,
        public message: string,
        public title: string,
        public type: LogType) {
        this.timeStamp = Date.now();
    }

    public toString = (): string => {
        return `[${this.logKey}][${new Date(this.timeStamp).toLocaleString()}],[${this.type}][${this.title}][${this.message}]`;
    };
}

export interface ILogger {
    log(item: LogItem): void;
}

export class ConsoleLogger implements ILogger {
    log(item: LogItem): void {

        var message: string = item.toString();
        switch (item.type) {
            case LogType.Log:
                console.log(message);
                break;
            case LogType.Warning:
                console.warn(message);
                break;
            case LogType.Error:
                console.error(message);
                break;
            default:
        }
    }
}


export class LocalStoreLogger implements ILogger {
    log(item: LogItem): void {

        let logs: LogItem[] = [];
        let data: string = localStorage.getItem(item.logKey);
        if (data === null) {
            data = "";
        } else {
            logs = JSON.parse(data);
        }
        logs.push(item);
        localStorage.setItem(item.logKey, JSON.stringify(logs));
    }
}


export class Logger {
    private loggersInstances: ILogger[] = [];
    private _suspended: boolean = false;

    get isSuspended(): boolean {
        return this._suspended;
    }

    suspend(): void {
        this._suspended = true;
    }

    resume(): void {
        this._suspended = false;
    }

    private writeToLoggers(item: LogItem, loggersx: Loggers): void {
        //debugger;
        if (loggersx == null)
            loggersx = this.loggers;
        var logIt: boolean = false;
        for (var logger of this.loggersInstances) {
            logIt = false;
            if (loggersx == null) {
                logIt = true;

            } else {
                switch (CoreHelper.nameOfInstance(logger)) {
                    case CoreHelper.nameOfType(ConsoleLogger):
                        if (CoreHelper.hasEnum(loggersx, Loggers.Console))
                            logIt = true;
                        break;
                    case CoreHelper.nameOfType(LocalStoreLogger):
                        if (CoreHelper.hasEnum(loggersx, Loggers.LocalStore))
                            logIt = true;
                        break;;
                    default:
                }
            }
            if (logIt) {
                logger.log(item);
            }
        }
    }

    log(message: string, title: string = null, logger: Loggers = null): void {
        this.writeToLoggers(new LogItem(this.logKey, message, title, LogType.Log), logger);
    }

    warn(message: string, title: string = null, logger: Loggers = null): void {
        this.writeToLoggers(new LogItem(this.logKey, message, title, LogType.Warning), logger);
    }

    error(message: string, title: string = null, logger: Loggers = null): void {
        this.writeToLoggers(new LogItem(this.logKey, message, title, LogType.Error), logger);
    }

    constructor(private logKey: string, private loggers: Loggers) {
        if (CoreHelper.hasEnum(loggers, Loggers.Console)) {
            this.loggersInstances.push(new ConsoleLogger());
        }

        if (CoreHelper.hasEnum(loggers, Loggers.LocalStore)) {
            this.loggersInstances.push(new LocalStoreLogger());
        }
    } 
}