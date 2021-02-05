export class Lookup {
    get Name(): string {
        return this.Value;
    }
    set Name(value) {
        this.Value = value;
    }
    Ref: number;
    Value: string;
    IsSelected: boolean = false;

    constructor(ref: number = 0, name: string = "") {
        this.Ref = ref;
        this.Value = name;
        //this.Name = name;
    }
}
