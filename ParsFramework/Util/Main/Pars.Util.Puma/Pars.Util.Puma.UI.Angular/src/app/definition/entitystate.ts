export class EntityState {
    EntityStateRef: number;
    Name: string;
    Description: string;

    public static createNew(): EntityState {
        return new EntityState(), { EntityStateRef: 0, Name: "", Description: "" };
    }
}

export interface IEntityState {
    entityState: EntityState;
    // localeData: LocaleData.LocaleData;
    hasChanged: boolean;

}
