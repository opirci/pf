export class Lookup {
    Name: string;
    Ref: number;
    IsSelected: boolean = false;

    constructor(ref: number = 0, name: string = "") {
        this.Ref = ref;
        this.Name = name;
    }
}
