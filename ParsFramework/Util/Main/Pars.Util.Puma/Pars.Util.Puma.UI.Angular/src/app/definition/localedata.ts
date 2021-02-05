export class LocaleData {
    LocaleRef: number;
    Value: string;
    HasChanged: boolean;

    constructor(localaRef: number, value: string, hasChanged: boolean) {
        this.LocaleRef = localaRef;
        this.Value = value;
        this.HasChanged = hasChanged;
    }

    public static createNew(): LocaleData {
        return new LocaleData(0, "", false);
    }
}