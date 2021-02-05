export class ResponseBase {
    errorMessages: string[];
    warningMessages: string[];

    get hasErrors(): boolean {
        return this.errorMessages != null && this.errorMessages.length > 0;
    }

    get hasWarnings(): boolean {
        return this.warningMessages != null && this.warningMessages.length > 0;
    }

    get warningMessagesText(): string {
        let text: string = "";
        if (this.hasWarnings) {
            for (let item of this.warningMessages)
                text += item + "\n";
        }
        return text;
    }

    get errorMessagesText(): string {
        let text: string = "";
        if (this.hasErrors) {
            for (let item of this.errorMessages) {
                text += item + "\n";
            }
        }
        return text;
    }

    get succeeded(): boolean {
        return this.hasErrors;
    }    
}

export class ResponseBaseGeneric<T> extends ResponseBase {
    value: T;

    constructor(value: T = null) {
        super();
        this.value = value;
    }
}