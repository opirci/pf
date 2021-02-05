export interface IMessageShowService {
    showMessage(title: string, message: string, type: MessageType): void;
}

export enum MessageType {
    Success,
    Info,
    Warning,
    Error
}