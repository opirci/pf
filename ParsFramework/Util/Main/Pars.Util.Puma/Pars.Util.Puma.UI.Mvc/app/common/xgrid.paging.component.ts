import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
    moduleId: module.id,
    selector: "pagingx",
    templateUrl: "xgrid.paging.component.html"
})
export class XGridPagingComponent {
    @Input()
    previousText: string = null;
    @Input()
    nextText: string = null;
    @Output()
    onNext: EventEmitter<void> = new EventEmitter<void>();
    @Output()
    onPrevious: EventEmitter<void> = new EventEmitter<void>();
    @Input()
    pageNumber: number;
    @Input()
    pageCount: number;
    @Input()
    hidePageNumbers: boolean;
    @Output()
    gotoPage: EventEmitter<number> = new EventEmitter<number>();
    @Input()
    totalCount: number;

    private pageNumberChange(): void {
        this.gotoPage.emit(this.pageNumber);
    }

    nextPage(): void {
        this.onNext.emit();
    }


    previousPage(): void {
        this.onPrevious.emit();
    }
}