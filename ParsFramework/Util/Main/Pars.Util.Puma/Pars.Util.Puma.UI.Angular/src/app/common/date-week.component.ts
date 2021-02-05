import { Component, Input, Inject, forwardRef, AfterContentInit, ElementRef, Renderer, OnInit } from "@angular/core";

//import { CoreHelper, DateWeekRange, DateWeek } from "../pars/index";
//export { DateWeekRange, DateWeek } from "../pars/index";

import { CoreHelper } from "../pars/core";
export { CoreHelper } from "../pars/core";
import { DateWeekRange, DateWeek } from "../pars/data";
export { DateWeekRange, DateWeek } from "../pars/data";

@Component({
    moduleId: module.id,
    selector: "date-week",
    templateUrl: "date-week.component.html"
})
export class DateWeekComponent implements OnInit {
    //@Input()
    //range: DateWeekRange = DateWeekRange.between<DateWeek>(DateWeek.now(), DateWeek.now());

    @Input()
    startWeek: number;
    @Input()
    startYear: number;
    @Input()
    endWeek: number;
    @Input()
    endYear: number;
    
    ngOnInit() {
        debugger;
    }
    clicked(): void
    {
        console.log(`startYear: ${this.startYear}, startWeek: ${this.startWeek}, endYear: ${this.endYear}, endWeek: ${this.endWeek}`);
    }


    go(): void {
        let dx: DateWeekRange = DateWeekRange.between(new DateWeek(1999, 5), new DateWeek(1999, 5));
    }
}