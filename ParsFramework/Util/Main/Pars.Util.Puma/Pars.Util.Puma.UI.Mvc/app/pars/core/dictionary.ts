export class Dictionary<T, T1> {
    items: KeyValuePair<T, T1>[] = [];

    add(key: T, value: T1): void {
        this.items.push(new KeyValuePair<T, T1>(key, value));
    }

    get(key: T): KeyValuePair<T, T1> {
        var founds = this.items.filter(t => t.key === key);
        if (founds != null && founds.length === 1)
            return founds[0];
        return null;
    }

    remove(key: T): boolean {
        var found: KeyValuePair<T, T1> = this.get(key);
        if (found != null)
            this.items.slice(this.items.indexOf(found), 1);
        return found != null;
    }
}

export class KeyValuePair<T, T1> {
    constructor(public key: T, public value: T1) { }
}
