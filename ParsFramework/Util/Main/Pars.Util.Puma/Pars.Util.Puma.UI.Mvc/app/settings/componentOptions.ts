export class ComponentOptions {
    //public static get DatePickerOptions(): any {
    //    return {
    //        todayBtnTxt: "Today",
    //        dateFormat: "yyyy-mm-dd",
    //        firstDayOfWeek: "mo",
    //        sunHighlight: true,
    //        inline: false,
    //        height: "30px",
    //        width: "200px",
    //        selectionTxtFontSize: "18px",
    //        alignSelectorRight: false,
    //        indicateInvalidDate: true,
    //        showDateFormatPlaceholder: true,
    //        editableMonthAndYear: true,
    //        disableUntil: { year: 0, month: 0, day: 0 },
    //        disableDays: [{ year: 0, month: 0, day: 0 }]
    //    };
    //}

    public static get NotifyMessageOptions(): any {
        return {
            timeOut: 2000,
            lastOnBottom: true,
            clickToClose: true,
            maxLength: 0,
            maxStack: 7,
            showProgressBar: false,
            pauseOnHover: true,
            preventDuplicates: false,
            preventLastDuplicates: "visible",
            rtl: false,
            animate: "scale",
            position: ["right", "bottom"]
        };
    }
}