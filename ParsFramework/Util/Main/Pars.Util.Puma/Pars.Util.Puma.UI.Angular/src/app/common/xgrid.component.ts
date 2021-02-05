import {
    Component, Input, Output, EventEmitter, OnInit, OnChanges, SimpleChanges, ContentChildren,
    AfterContentInit, QueryList, Inject, forwardRef, ViewChild, ViewChildren, Pipe, PipeTransform,
    Injectable, ElementRef, ChangeDetectorRef, DoCheck, KeyValueDiffers, KeyValueDiffer, KeyValueChangeRecord,
    isDevMode, TemplateRef
} from '@angular/core';
import { Validators, FormGroup, FormControl, FormArray, FormBuilder, AbstractControl } from '@angular/forms';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';


import { Dictionary, CoreHelper, TsType } from '../pars/core';
import '../pars/core';
import {
    PagedList, DataTable, /*StatedList,*/ Row, Column, TableExporter, CsvTableFormatter,
    HtmlTableFormatter, TableExportService, ExportContent, ExportFormat
} from '../pars/data';

import { XGridPagingComponent } from './xgrid.paging.component';
import { ColumnInfo, BooleanViews, DateFormat } from './xgrid.column-info';
import { XGridColumnComponent } from './xgrid.xcolumn.component';
import { XGridService, IXGrid } from './xgrid.service';
import { IXGridSettings, XGridSettings } from './xgrid.settings';

export { PagedList, DataTable, ExportFormat, ExportContent } from '../pars/data';
export { ColumnInfo } from './xgrid.column-info';
export { XGridColumnComponent } from './xgrid.xcolumn.component';
export { XGridPagingComponent } from './xgrid.paging.component';
export { XGridService, XGridDataRequest } from './xgrid.service';


//import { FileSaver } from 'file-saver';


@Component({
    moduleId: module.id,
    selector: 'xgrid',
    templateUrl: 'xgrid.component.html',
    styleUrls: ['xgrid.component.css'],
    providers: [TableExportService]

})
export class XGridComponent implements IXGridSettings, OnInit, OnChanges, DoCheck, AfterContentInit, IXGrid {
    @ContentChildren(XGridColumnComponent)
    columnsArr: QueryList<XGridColumnComponent>;

    @Input() selectText: string = null;
    @Input() previousText: string = null;
    @Input() nextText: string = null;
    @Input() gridTitle: string;
    @Input() data: any;

    _dataSource: XGridDataSource

    get dataSource(): XGridDataSource {
        return this._dataSource;
    }

    @Input()
    set dataSource(value: XGridDataSource) {
        debugger;
        this._dataSource = value;
    }

    @Input() pageSize: number = 20;
    @Input() enablePaging: boolean = false;
    @Input() pagingOnTop: boolean = false;
    @Input() enableSearchBar: boolean = false;
    @Input() enableSelectColumn: boolean = false;
    @Input() showHeader: boolean = true;
    @Input() showLineNumbers: boolean = false;
    @Input() autoGenerateColumns: boolean = false;
    @Input() isEditable: boolean = false;
    @Input() excelExport: boolean = false;
    @Input() exportAs: ExportFormat = ExportFormat.ExcelCsvSemiColumn;
    @Input() exportFileName: string = null;

    @Input() csvSeparator;
    @Input() exportHiddenColumns: boolean = false;
    @Input() columnHeaderStyle: string = null;
    @Input() columnStyle: string = null;
    @Input() exportButtonStyle: string = null;
    @Input() exportButtonClass: string = null;
    @Input() booleanView: BooleanViews;
    @Input() horizontalScroll: boolean = true;
    @Input() verticalScroll: boolean = false;
    @Input() verticalScrollRowsToShow: number = 20;
    @Input() showSettings: boolean = false;
    @Input() gridService: XGridService;

    @Input() onColumnInitializing: (column: XGridColumnComponent) => void;

    @Output() selectFunc: EventEmitter<any> = new EventEmitter<any>();


    settingsDialogVisible: boolean = false;
    settings: XGridSettings;
    gridData: PagedList<any>;
    columnList: ColumnInfo[];
    xcolumnList: XGridColumnComponent[] = [];
    columnListFromMarkup: ColumnInfo[];
    @ViewChild('exportArea')
    exportArea: any;

    BooleanViews = BooleanViews;
    TsType = TsType;

    frows: FormArray;
    fgridForm: FormGroup;
    fpageNumber: FormControl;
    ftotalCount: FormControl;

    @ViewChildren(XGridColumnComponent)
    xgridcolumns: QueryList<XGridColumnComponent>;

    private isColumnsGenerated;
    tableId: string;
    pageNumber: number;
    private pageCount: number;

    //differ: KeyValueDiffer;

    constructor(private http: Http,
        private _fb: FormBuilder,
        private _tableExportService: TableExportService,
        private _chDetectorRef: ChangeDetectorRef,
        private _elementRef: ElementRef,
        private _differs: KeyValueDiffers) {
        this.tableId = 'xgrid_' + this.generateId();
        this.pageNumber = 1;
        this.pageCount = 1;
        this.columnList = new Array<ColumnInfo>();
        this.columnListFromMarkup = new Array<ColumnInfo>();

        //this.differ = this._differs.find({}).create(null);
    }

    get columnHeaderStyleInstance(): any {
        if (this.columnHeaderStyle == null)
            return null;
        return CoreHelper.cssStyleTextToJson(this.columnHeaderStyle);
    }

    get columnStyleInstance(): any {
        if (this.columnStyle == null)
            return null;
        return CoreHelper.cssStyleTextToJson(this.columnStyle);
    }

    get exportButtonStyleInstance(): any {
        if (this.exportButtonStyle == null)
            return null;
        return CoreHelper.cssStyleTextToJson(this.exportButtonStyle);
    }

    getData(): DataTable | PagedList<any> {
        return this.hasDataTable ? this.generateDataTableBack() : this.generateTypedValuesBack();
    }

    private logcnt: number = 0;
    private autoRefreshData: boolean = false;

    private get frowsLength() {
        return this.frows && this.frows.value ? this.frows.value.length : -1;
    }

    get hasData(): boolean {
        return ((this.gridService && this.gridService.data) || this.data);
    }

    ngDoCheck() {
        if (this.gridData && (this.autoRefreshData === true || this!.gridData!.length !== this.frowsLength) ||
            (this.gridService && this.gridService.data && (this.autoRefreshData === true || this!.gridService!.data!.length !== this.frowsLength)))
            this.initializeData(true);

        if (isDevMode()) {
            //console.log(`ngDoCheck(${this.logcnt++})`);
            //if (this.data)
            //    this.logChanges('this.data[0]', this.data[0]);
            //this.logChanges('data', this.data);
            //this.logChanges('gridData', this.gridData);
        }
    }

    //get autoRefreshData(): boolean {
    //    return this._autoRefreshData;
    //}

    //set autoRefreshData(value: boolean) {
    //    debugger;
    //    this._autoRefreshData = value;
    //}

    //setValueWithRefresh(action: () => void): void {
    //    action();
    //}

    private refresh() {
        //this.autoRefreshData = true;
        this._chDetectorRef.detectChanges();
        //return () => { this.autoRefreshData = true; };
    }

    //private logChanges(dataName: string, value: any): void {
    //    var changes = this.differ.diff(value);

    //    if (changes) {
    //        console.log(`${dataName}: changes detected`);
    //        changes.forEachChangedItem((r: KeyValueChangeRecord) => this.logChange(r, 'CHANGED'));
    //        changes.forEachAddedItem((r: KeyValueChangeRecord) => this.logChange(r, 'ADDED'));
    //        changes.forEachRemovedItem((r: KeyValueChangeRecord) => this.logChange(r, 'REMOVED'));

    //    } else {
    //        //console.log(`${dataName}: nothing changed`);
    //    }
    //}

    //private logChange(change: KeyValueChangeRecord, message: string): void {
    //    console.log('--->', message);
    //    console.log('    key           ', JSON.stringify(change.key));
    //    console.log('    currentValue  ', JSON.stringify(change.currentValue));
    //    console.log('    previousValue ', JSON.stringify(change.previousValue));
    //}

    ngOnChanges(changes: SimpleChanges): void {
        //console.log(`ngOnChanges(${this.logcnt++})`);


        for (let propName in changes) {
            //if ((propName === 'data' || propName === 'gridService')
            //    || (!this.initializeDataInProcess && propName === 'gridData' && this.gridData)
            //    || (propName === 'printable' && this.data) || (propName === 'printable' && this.gridService.data)
            //) {
            //    this.initializeData(propName === 'printable');
            //    if (this.data) {
            //        this.pageNumber = this.data.pageNumber;
            //        this.calcPageCount(this.data.totalCount);
            //    } else
            //        if (this.gridService && this.gridService.data) {
            //            this.pageNumber = this.gridService.data.pageNumber;
            //            this.calcPageCount(this.gridService.data.totalCount);
            //        }
            //    break;
            //}

            if ((propName === 'data' || propName === 'gridService') //this.gridService.data
                || (!this.initializeDataInProcess && propName === 'gridData' && this.gridData)) {
                this.initializeData(true);

                break;
            }
        }
    }

    ngAfterContentInit(): void {
        this.initializeData(false);
    }

    ngOnInit(): void {
        const settings = XGridSettings.loadFromLocalStore();
        if (settings) {
            settings.setTo(this.settings);
            settings.setTo(this);
        }
        else {
            this.clearSettings();
        }

        if (this.dataSource && this.gridService || (!this.dataSource && !this.gridService)) {
            throw Error(`dataSource or gridService must not be provided`);
        }

        if (this.dataSource) {

            this.dataSource.getDataExecuted = (page: number, pageSize: number): void => {
                this.pageNumber = page;
                this.pageSize = pageSize;
                return null;
            }

            this.dataSource.searchDataExecuted = (searchText: string, searchMember: string, page: number, pageSize: number): void => {
                this.pageNumber = page;
                this.pageSize = pageSize;
                return null;
            };
        } else {
            this.gridService.init(this);
        }


    }

    dialogSettings: XGridSettings = null;
    showSettingsDailog(): void {
        this.settingsDialogVisible = true;
        this.dialogSettings = this.settings;
    }

    saveSettings(): void {
        this.settingsDialogVisible = false;
        this.settings = this.dialogSettings;
        this.settings.setTo(this);
        this.settings.saveToLocalStore();
        // SAVE TO STORE
    }


    setSettings(): void {
        this.booleanView = this.settings.booleanView;
        // this.settings.excelExport = this.excelExport;
        this.settings.exportAs = this.exportAs;
        this.settings.horizontalScroll = this.horizontalScroll;
        this.settings.pageSize = this.pageSize;
        this.settings.pagingOnTop = this.pagingOnTop;
        this.settings.showLineNumbers = this.showLineNumbers;
        this.settings.verticalScroll = this.verticalScroll;
        this.settings.verticalScrollRowsToShow = this.verticalScrollRowsToShow;
    }

    clearSettings(): void {
        this.settings = new XGridSettings();
        this.settings.booleanView = this.booleanView;
        // this.settings.excelExport = this.excelExport;
        this.settings.exportAs = this.exportAs;
        this.settings.horizontalScroll = this.horizontalScroll;
        this.settings.pageSize = this.pageSize;
        this.settings.pagingOnTop = this.pagingOnTop;
        this.settings.showLineNumbers = this.showLineNumbers;
        this.settings.verticalScroll = this.verticalScroll;
        this.settings.verticalScrollRowsToShow = this.verticalScrollRowsToShow;
    }

    private searchDataExecuted: IXGridSearchDataDelegate = (searchText: string, searchMember: string, page: number, pageSize: number): Observable<any> => {
        this.pageNumber = page;
        this.pageCount = pageSize;
        return null;
    };

    private generateDataTableBack(): DataTable {

        let dataColumns: Column[] = [];
        let dataColumn: Column;
        let dataRows: Row[] = [];
        let dataRow: Row;

        // generate columns
        for (var colInfo of this.columnList) {
            dataColumn = new Column();
            dataColumn.name = colInfo.fieldName;
            dataColumn.type = colInfo.dataTypeName;
            dataColumns.push(dataColumn);
        }

        for (let rw of this.frows.controls) {
            dataRow = new Row();
            let rwf: FormGroup = rw as FormGroup;
            for (let i in rwf.controls) {
                let ctl: FormControl = <FormControl>rwf.controls[i];
                if (ctl.dirty)
                    dataRow.isUpdated = true;
                dataRow.values.push(ctl.value);
            }
            dataRows.push(dataRow);
        }

        let dt: DataTable = new DataTable(
            dataColumns,
            dataRows,
            this.fgridForm.get('fpageNumber').value,
            this.fgridForm.get('ftotalCount').value);
        return dt;
    }

    private generateTypedValuesBack(): PagedList<any> {
        let items: any[] = [];
        let list: PagedList<any> = new PagedList<any>(items, this.fgridForm.get('fpageNumber').value, this.fgridForm.get('ftotalCount').value);
        for (let rw of this.frows.controls) {
            let fg: FormGroup = <FormGroup>rw;
            let isDirty: boolean = false;

            for (let i in fg.controls) {
                let ctl: FormControl = <FormControl>fg.controls[i];
                if (ctl.dirty) {
                    isDirty = true;
                    break;
                }
            }
            //list.pushWithState(fg.value, isDirty);
            list.push(fg.value);

        }

        return list;
    }

    private initializeDataInProcess: boolean = false;

    private buildColumnListFromMarkupDone: boolean = false;

    private buildColumnListFromMarkup(force: boolean): void {
        if ((this.buildColumnListFromMarkupDone === false || force) &&
            (this.columnListFromMarkup && this.columnListFromMarkup != null && this.columnListFromMarkup.length > 0)) {
            for (var cm of this.columnListFromMarkup) {
                let cfound: ColumnInfo = this.columnList.find(c => c.fieldName === cm.fieldName);
                if (!cfound || cfound === null) {
                    this.columnList.push(cm);
                } else {
                    this.columnList[this.columnList.indexOf(cfound)] = cm;
                }
            }
            for (let col of this.columnList) {
                col.defaultHeaderStyleText = this.columnHeaderStyle;
                col.defaultValueStyleText = this.columnStyle;
            }
            this.buildColumnListFromMarkupDone = true;
        }
    }

    //#region Form Generation     

    private initForm(): void {
        try {
            let pn = this.gridData && this.gridData.pageNumber ? this.gridData.pageNumber : 0;
            let tc = this.gridData && this.gridData.totalCount ? this.gridData.totalCount : 0;
            this.fpageNumber = new FormControl(pn);
            this.ftotalCount = new FormControl(tc);
            this.frows = new FormArray([]);

            this.fgridForm = new FormGroup(
                {
                    fpageNumber: this.fpageNumber,
                    ftotalCount: this.ftotalCount,
                    frows: this.frows,
                });

            let i: number = 0;
            if (this.gridData)
                for (let item of this.gridData) {
                    let rw: FormGroup = this.addRow(item, i);
                    for (let columnInfo of this.columnList) {
                        this.addColumn(item, columnInfo, rw, i);
                    }
                    i++;
                }
        } catch (Exc) {
            console.error(`Error occured when initializing form in xgrid.\nError message :${Exc}`);
        }
    }

    private addRow(data: any, rowIndex: number): FormGroup {
        var fg = new FormGroup({});
        this.frows.setControl(rowIndex, fg);
        return fg;
    }

    addEventInfo(rowEventInfo: RowEventInfo, value: any): void {
        if (isDevMode()) {
            //console.log(`addEventInfo(Row:${rowEventInfo.row}, Col:${rowEventInfo.col}, Value:${value}, `);
        }
    }

    private addColumn(data: any, columnInfo: ColumnInfo, rw: FormGroup, rowIndex: number): void {
        let fcParam: any = {};
        fcParam['value'] = this.getColumnValue(data, columnInfo);
        fcParam['disabled'] = false; //!this.isEditableColumn(columnInfo);
        let fc: XFormControl = new XFormControl(fcParam);
        let rei: RowEventInfo = new RowEventInfo(rowIndex, columnInfo.fieldName);
        fc.eventInfo = rei;

        fc.valueChanges.distinctUntilChanged().subscribe(val => this.addEventInfo(rei, val));
        rw.addControl(columnInfo.fieldName, fc);
    }

    private getColumnValue(data: any, columnInfo: ColumnInfo, formated: boolean = false): string {
        return ColumnInfo.getColumnValue(data, columnInfo, formated);
    }

    private discoverColumnType(value: any, columnInfo: ColumnInfo): boolean {
        return ColumnInfo.discoverColumnType(value, columnInfo);
    }

    private get hasDataTable(): boolean {
        return (this.data && this.data instanceof DataTable) || (this.gridService && this.gridService.data && this.gridService.data instanceof DataTable);
    }

    private get theData(): any {
        if (this.data)
            return this.data;
        else if (this.gridService && this.gridService.data)
            return this.gridService.data;
        return null;
    }

    private initializeData(force: boolean): void {
        this.exportObject = null;
        if (!this.hasData) {
            this.gridData = null;
            if (this.gridService && this.gridService.data)
                this.gridService.data = null;
            return;
        }
        let dtx = this.theData;

        this.initializeDataInProcess = true;
        if (this.hasDataTable) {
            let dt: DataTable = dtx as DataTable;
            this.gridData = new PagedList(dt.rows, dt.pageNumber, dt.totalCount);
            force = true;
        }
        else { this.gridData = dtx; }

        this.pageNumber = dtx.pageNumber;
        this.calcPageCount(dtx.totalCount);

        this.checkAutoGenerateColumns(force);
        this.discoverColumnTypes();
        this.initForm();
        this.initializeDataInProcess = false;
        this.autoRefreshData = false;
    }

    ngAfterViewChecked() {
        if (this.horizontalScroll === true || this.verticalScroll === true) {
            const comp: HTMLElement = this._elementRef.nativeElement as HTMLElement;
            const tbl = comp.getElementsByTagName('table');
            let table: HTMLTableElement;
            if (tbl && tbl.length == 1) {
                table = tbl[0] as HTMLTableElement;
                const div = table.parentElement as HTMLDivElement;

                if (this.horizontalScroll === true) {
                    div.style.overflowX = 'auto';
                    div.style.whiteSpace = 'nowrap';
                }

                if (this.verticalScroll === true) {
                    const tblHdr = comp.getElementsByClassName('xgrid-header')
                    let tableHeight = -1;
                    let rowHeight = -1;
                    let tableHeaderHeight = -1;

                    tableHeight = table!.offsetHeight;
                    tableHeaderHeight = table!.tHead!.offsetHeight;
                    if (tblHdr && tblHdr.length == 1)
                        tableHeaderHeight += (<HTMLElement>tblHdr[0]).offsetHeight;
                    if (table.rows)
                        rowHeight = table.rows.length > 0 ? (<HTMLTableRowElement>table!.rows[0])!.offsetHeight : -1;

                    if (tableHeaderHeight != -1 && tableHeight != -1 && rowHeight != -1) {
                        const tableH = tableHeaderHeight + (rowHeight * (this.verticalScrollRowsToShow - 1));

                        table.parentElement.style.height = String(tableH) + 'px';
                        table.parentElement.style.overflowY = 'auto';
                    }
                }
            }
        }
    }

    private checkAutoGenerateColumns(force: boolean = false): void {
        this.buildColumnListFromMarkup(force);
        if (this.autoGenerateColumns === true || this.hasDataTable) {
            if (!this.isColumnsGenerated || force) {
                let generatedColumns: ColumnInfo[] = null;
                if (this.data) {
                    generatedColumns = this.generateColumnsFromData(this.columnList, this.data);
                } else if (this.gridService.data) {
                    generatedColumns = this.generateColumnsFromData(this.columnList, this.gridService.data);
                }

                if (this.columnList.length > 0 && generatedColumns != null) {
                    for (var col of this.columnList) {
                        let genCol: ColumnInfo = null;
                        if (col.fieldName != null) {
                            genCol = generatedColumns.find(f => f.fieldName === col.fieldName);
                        } else if (col.columnOrder > 0) {
                            genCol = generatedColumns.find(f => f.columnOrder === col.columnOrder);
                        }
                        if (genCol == null) {
                            if (!this.hasDataTable)
                                console.error('Error autogenerating columns\nColumn not found' + col.toString());
                            continue;
                        }
                        if (col.headerName)
                            genCol.headerName = col.headerName;

                        if (col.isHidden)
                            genCol.isHidden = col.isHidden;

                        if (col.isEditable)
                            genCol.isEditable = col.isEditable;

                        if (col.conversions)
                            genCol.conversions = col.conversions;

                        if (col.format)
                            genCol.format = col.format;
                    }
                }
                if (generatedColumns != null) {
                    this.isColumnsGenerated = true;
                    this.columnList = generatedColumns;
                }
            }
        }
    }

    private discoverColumnTypes(): void {
        let colsNotDiscovered: ColumnInfo[] = [];
        let colsToRun = this.columnList;
        //const hasDt = this.hasDataTable;
        if (this!.gridData!.length > 0) {
            for (let grdData of this.gridData) {
                for (let col of colsToRun) {
                    if (!this.discoverColumnType(grdData, col))
                        colsNotDiscovered.push(col);
                }
                if (colsNotDiscovered.length == 0)
                    break;
                colsToRun = colsNotDiscovered;
                colsNotDiscovered = [];
            }
        }
    }

    private cliTypeToTypeScriptType(typeName: string): TsType {
        for (let type of this.typesMap)
            if (type[1] == typeName)
                return <TsType>type[2];
        throw Error(`Unknow cli type ${typeName}`);
    }

    private typesMap: any[][] = [
        ['char', 'System.Char', TsType.string],
        ['string', 'System.String', TsType.string],
        ['bool', 'System.Boolean', TsType.boolean],
        ['byte', 'System.Byte', TsType.number],
        ['sbyte', 'System.SByte', TsType.number],
        ['decimal', 'System.Decimal', TsType.number],
        ['double', 'System.Double', TsType.number],
        ['float', 'System.Single', TsType.number],
        ['int', 'System.Int32', TsType.number],
        ['uint', 'System.UInt32', TsType.number],
        ['long', 'System.Int64', TsType.number],
        ['ulong', 'System.UInt64', TsType.number],
        ['short', 'System.Int16', TsType.number],
        ['ushort', 'System.UInt16', TsType.number],
        ['object', 'System.Object', TsType.any]];

    private generateColumnsFromData(declaredColumns: ColumnInfo[], data: any): ColumnInfo[] {
        var columns: ColumnInfo[] = [];
        if (data != null) {
            if (this.hasDataTable) {
                let datatable: DataTable = data as DataTable;
                let colsAdded: Column[] = [];
                if (datatable.rows.length > 0) {
                    let colNum: number = 0;
                    for (let column of declaredColumns) {
                        let col: Column = datatable.columns.find(c => c.name == column.fieldName);
                        if (col != null) {
                            column.columnIndex = datatable.columns.indexOf(col);
                            colsAdded.push(col);
                            column.dataTypeName = col.type;
                            // add header if not defined in declared columns
                            if (!column.headerName)
                                column.headerName = CoreHelper.camelCaseToWords(column.fieldName);
                            columns.push(column);
                            colNum++;
                        }
                    }

                    if (this.autoGenerateColumns === true) {
                        for (let column of datatable.columns) {
                            if (colsAdded.find(c => c.name == column.name) != null)
                                continue;
                            colNum++;
                            let ci: ColumnInfo = new ColumnInfo(
                                datatable.columns.indexOf(column),
                                CoreHelper.camelCaseToWords(column.name),
                                column.name,
                                null,
                                null,
                                colNum,
                                false,
                                this.isEditable,
                                this.cliTypeToTypeScriptType(column.type),
                                column.type,
                                this.columnHeaderStyle,
                                this.columnStyle,
                                null,
                                null,
                                null,
                                null,
                            );

                            //let ciFound: ColumnInfo = this.columnList.find(c => c.fieldName == column.name);
                            //let ciFound: ColumnInfo = null;
                            //let ci: ColumnInfo = null;
                            //if (ciFound != null) {
                            //    const headerName = ciFound.headerName ? ciFound.headerName : CoreHelper.camelCaseToWords(column.name);
                            //    const fieldName = ciFound.fieldName;
                            //    const format = ciFound.format ? DateFormat[ciFound.format].toString() : null;
                            //    const conversions = ciFound.conversions;
                            //    const columnOrder = ciFound.columnOrder ? ciFound.columnOrder : colNum; //?????
                            //    const isHidden = ciFound.isHidden ? ciFound.isHidden : false;
                            //    const isEditable = ciFound.isEditable ? ciFound.isEditable : this.isEditable;
                            //    const dataType = ciFound.dataType ? ciFound.dataType : column.type;
                            //    const htmlContent = ciFound.htmlContent ? ciFound.htmlContent : null;
                            //    const headerStyleText = ciFound.headerStyleText ? ciFound.headerStyleText : this.columnHeaderStyle;
                            //    const valueStyleText = ciFound.valueStyleText ? ciFound.valueStyleText : this.columnStyle;
                            //    const booleanViewAs = ciFound.booleanViewAs ? ciFound.booleanViewAs : null;

                            //    ci = new ColumnInfo(headerName, fieldName, format, conversions, columnOrder, isHidden, isEditable, dataType, htmlContent, headerStyleText, valueStyleText, booleanViewAs);
                            //}
                            //else {
                            //    ci = new ColumnInfo(CoreHelper.camelCaseToWords(column.name), column.name, null, null, colNum, false, this.isEditable, column.type, null, this.columnHeaderStyle, this.columnStyle, null);
                            //}
                            columns.push(ci);
                        }
                    }
                }
            } else {
                //TODO 
                let colNum: number = 0;
                //for (let column of declaredColumns) {

                //    columns.push(column);
                //    colNum++;
                //}

                if (data.length > 0) {
                    var row = data[0];

                    for (let column in row) {
                        colNum++;
                        var ci: ColumnInfo = new ColumnInfo(null, CoreHelper.camelCaseToWords(column), column, null, null, colNum, false, this.isEditable, /*CoreHelper.getTsType()*/ null, null, this.columnHeaderStyle, this.columnStyle, null, null, null, null);
                        columns.push(ci);
                    }
                    let methods: string[] = this.getMethods(row);
                    if (methods && methods.length > 0) {
                        for (var method of methods) {
                            colNum++;
                            var ci: ColumnInfo = new ColumnInfo(null, CoreHelper.camelCaseToWords(method), method, null, null, colNum, false, false, null, ColumnInfo.typeOfMethod, this.columnHeaderStyle, this.columnStyle, null, null, null, null);
                            columns.push(ci);
                        }
                    }
                }
            }
            this.setGridDefaultsToColumns(columns);
            return columns;
        }
    }

    setGridDefaultsToColumns(columns: ColumnInfo[]): void {
        for (let column of columns)
            this.setGridDefaultsToColumn(column);
    }

    setGridDefaultsToColumn(column: ColumnInfo): void {
        if (!(column.defaultHeaderStyleText && column.defaultHeaderStyleText != null) && (this.columnHeaderStyle && this.columnHeaderStyle != null)) {
            column.defaultHeaderStyleText = this.columnHeaderStyle;
        }

        if (!(column.defaultValueStyleText && column.defaultValueStyleText != null) && (this.columnStyle && this.columnStyle != null)) {
            column.defaultValueStyleText = this.columnStyle;
        }

        if (!(column.isEditable && column.isEditable != null) && (this.isEditable && this.isEditable != null)) {
            column.isEditable = this.isEditable;
        }
    }

    private addSearchParameter(value: string): void {
        this.pageNumber = 1;
        if (this.dataSource)
            this.dataSource.searchData(value, null, 1, this.pageCount);
        else
            if (this.gridService)
                this.gridService.dataRequest.emit({ searchText: value, pageNumber: 1, pageSize: this.pageCount });
    }

    private getDatas(pageNumber?: number): void {
        const pgNum = pageNumber ? pageNumber : this.pageNumber;
        if (this.dataSource)
            this.dataSource.getData(pgNum, this.pageSize);
        else
            if (this.gridService)
                this.gridService.dataRequest.emit({ pageNumber: pgNum, pageSize: this.pageSize });
    }

    private calcPageCount(itemsSize: number): void {
        this.pageCount = Math.floor(itemsSize / this.pageSize) + 1;
    }

    //#endregion

    //#region Utility methods

    private generateId(): string {
        var text = '';
        var possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));
        return text;
    }

    private getMethods(obj: any): string[] {
        var ret: string[] = [];
        for (var prop in obj) {
            //  typeof is inconsitent, duck type for accuracy
            //  This could also be written to follow the internal IsCallable algorithm
            //  http://es5.github.com/#x9.11
            if (obj[prop] && obj[prop].constructor &&
                obj[prop].call && obj[prop].apply) {
                ret.push(prop);
            }
        }
        return ret;
    }

    private getAllFunctions(value: any): any[] {
        var x = Object.getOwnPropertyNames(value.constructor);
        var myfunctions = [];
        for (var l in value) {
            var hop = value.hasOwnProperty(l);
            var isFunc = value[l] instanceof Function;
            var val = value[l];
            if (value.hasOwnProperty(l) &&
                val instanceof Function) {
                myfunctions.push(val);
            }
        }
        return myfunctions;
    }
    //#endregion


    //#region Event methods

    get showSelectColumn(): boolean {
        return (this.data != null || this.gridService.data != null) && this.columnList != null && this.columnList.length > 0 && this.enableSelectColumn == true;
    }

    private previousPage(): void {
        if (this.pageNumber === 1) {
            return;
        }
        this.pageNumber--;
        this.getDatas();
    }

    private nextPage(): void {
        if (this.pageNumber === this.pageCount) {
            return;
        }
        this.pageNumber++;
        this.getDatas();
    }

    private select(selectedData: any): void {
        let row = this.frows.value[selectedData];
        if (this.selectFunc)
            this.selectFunc.emit(
                {
                    result: row,
                    grid: this
                }
            );
        else if (this.gridService && this.gridService.onSelect) {
            this.gridService.onSelect.emit(
                {
                    value: row,
                    grid: this
                }
            );
        }
    }

    private pageNumberChange(page: number): void {       
        this.getDatas(page);
    }

    private generateExportObject(): string[][] {

        if (this.exportObject == null) {
            let tableArr: string[][] = [];
            let lineArr: string[] = [];
            let col: number = 0;
            let line: number = 0;
            if (this.showLineNumbers == true) {
                lineArr[col++] = '#';
            }
            for (let columnInfo of this.columnList) {
                if (columnInfo.isHidden !== true || this.exportHiddenColumns === true)
                    lineArr[col++] = columnInfo.headerName;
            }
            tableArr[line++] = lineArr;
            col = 0;
            for (let item of this.gridData) {
                lineArr = [];
                if (this.showLineNumbers == true)
                    lineArr[col++] = String(((this.pageNumber - 1) * this.pageSize) + line);
                for (let columnInfo of this.columnList) {
                    if (columnInfo.isHidden !== true || this.exportHiddenColumns === true) {
                        let vl = this.getColumnValue(item, columnInfo, true);
                        lineArr[col++] = vl ? String(vl) : '';
                    }
                }
                tableArr[line++] = lineArr;
            }
            this.exportObject = tableArr;
        }
        return this.exportObject;
    }

    private exportObject: string[][] = [];

    exportAsStyle(format: ExportFormat): string {
        return this.exportAs === format ? 'disabled btn-default' : '';
    }

    ExportFormat = ExportFormat;

    setexportTo(format: ExportFormat, result: any) {
        this.exportAs = format;
    }

    //export(format: ExportFormat, fileName?: string): string {
    //    let fileExtension: string = ''
    //    let respHeader: string = '';
    //    let exp: TableExporter = null;

    //    switch (this.exportAs) {
    //        case ExportFormat.ExcelXlsx:
    //            exp = new TableExporter(new HtmlTableFormatter({ headerStyle: this.columnHeaderStyle, cellStyle: this.columnStyle }));
    //            fileExtension = 'xls';
    //            respHeader = 'data:application/vnd.ms-excel';
    //            break;
    //        case ExportFormat.ExcelCsvComma:
    //        case ExportFormat.ExcelCsvSemiColumn:
    //            const sep = this.exportAs == ExportFormat.ExcelCsvComma ? ',' : ';';
    //            exp = new TableExporter(new CsvTableFormatter({ separator: sep }));
    //            // exp = new TableExporter(new CsvTableFormatter({ separator: this.csvSeparator }));
    //            fileExtension = 'csv';
    //            respHeader = 'data:text/csv;charset=UTF-8';
    //            break;
    //        default:
    //            throw Error(`Undefined export type ${this.exportAs}`);
    //    }

    //    let content: string = '';

    //    content = exp.export(this.generateExportObject());
    //    content = '\uFEFF' + content;

    //    //if (!fileName)
    //    //    return content;

    //    this.makePrintable();

    //    if (fileName == null) {
    //        fileName = (this.gridTitle ? this.gridTitle : 'Report').replace(/[^a-z0-9öçşığüÖÇŞİĞÜ]/gi, '_');
    //    }

    //    fileName += '.' + fileExtension;

    //    //let datax = new Blob([tab_text], { type: 'data:application/vnd.ms-excel' });
    //    //FileSaver.saveAs(datax, fileName);


    //    console.warn('excel created');

    //    const blob = new Blob([content], [respHeader]);
    //    if (navigator.msSaveBlob) {
    //        console.warn('excel blob');
    //        navigator.msSaveBlob(blob, fileName);
    //    }
    //    else {
    //        console.warn('excel link');
    //        const link = document.createElement('a');
    //        if (link.download !== undefined) {
    //            const url = URL.createObjectURL(blob);
    //            link.setAttribute('href', url);
    //            link.setAttribute('download', fileName);
    //            link.style.setProperty('visibility', 'hidden');
    //            document.body.appendChild(link);
    //            link.click();
    //            document.body.removeChild(link);
    //        }
    //    }
    //    console.warn('excel finished');

    //    //const ua = window.navigator.userAgent;
    //    //const msie = ua.indexOf('MSIE ');
    //    //if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If Internet Explorer
    //    //{
    //    //    const win = window.open();
    //    //    const doc = win.document.open(respHeader, 'replace');
    //    //    doc.write(tab_text);
    //    //    doc.close();
    //    //    doc.execCommand('SaveAs', true, fileName);
    //    //    win.close();
    //    //}
    //    //else {
    //    //    const a = document.createElement('a');
    //    //    a.href = respHeader + ',' + encodeURIComponent('\uFEFF' + tab_text);
    //    //    a.download = fileName;
    //    //    a.click();
    //    //}
    //    return null;
    //}

    export(format: ExportFormat): ExportContent {
        return this._tableExportService.export(
            this.generateExportObject(),
            this.exportAs,
            {
                headerStyle: this.columnHeaderStyle,
                cellStyle: this.columnStyle
            });
    }

    exportTo(format?: ExportFormat, filename?: string): void {
        if (!(this.data || this.gridService.data))
            return;
        const exportContent: ExportContent = this.export(format ? format : this.exportAs);

        let fileName = filename ? filename : this.exportFileName;

        if (fileName == null) {
            fileName = (this.gridTitle ? this.gridTitle : 'Report').replace(/[^a-z0-9öçşığüÖÇŞİĞÜ]/gi, '_');
        }

        fileName += '.' + exportContent.fileExtension;

        const blob = new Blob([exportContent.content], [exportContent.header]);
        if (navigator.msSaveBlob) {
            navigator.msSaveBlob(blob, fileName);
        }
        else {
            const link = document.createElement('a');
            if (link.download !== undefined) {
                const url = URL.createObjectURL(blob);
                link.setAttribute('href', url);
                link.setAttribute('download', fileName);
                link.style.setProperty('visibility', 'hidden');
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }
        }
    }


    submitGrid(row: any): void {
        debugger;
        if (isDevMode()) {
            //console.log(`submitGrid(row: any) ${row}`);
            //console.log(`submitGrid(row: any) ${this.fgridForm.value}`);
        }
    }

    checkit(element: any): void {
        debugger;
    }
    //#endregion
}





//#region XGridDataSource 

export interface IXGridGetDataDelegate {
    (page: number, pageSize: number): void;
}

export interface IXGridSearchDataDelegate {
    (searchText: string, searchMember: string, page: number, pageSize: number): void;
}

export interface IXGridDataSource {
    getData(page: number, pageSize: number): void;
    searchData(searchText: string, searchMember: string, page: number, pageSize: number): void;
}

export class XGridDataSource implements IXGridDataSource {

    getDataExecuted: IXGridGetDataDelegate;
    searchDataExecuted: IXGridSearchDataDelegate;

    getData(page: number, pageSize: number): void {
        if (this.getDataExecuted) {
            this.getDataExecuted(page, pageSize);
        }
        return this.getDataDelegate(page, pageSize);
    }


    searchData(searchText: string, searchMember: string, page: number, pageSize: number): void {
        if (this.searchDataExecuted) {
            this.searchDataExecuted(searchText, searchMember, page, pageSize);
        }
        return this.searchDataDelegate(searchText, searchMember, page, pageSize);
    }

    constructor(private getDataDelegate: IXGridGetDataDelegate, private searchDataDelegate: IXGridSearchDataDelegate) {

    }
}

export class XFormControl extends FormControl {
    eventInfo: RowEventInfo;
}

export class RowEventInfo {

    constructor(public row: number, public col: string) { }
}
//#endregion 



//import { Directive, Renderer, Host, ElementRef } from '@angular/core';

//@Directive({
//    selector: '[styled]'
//})
//export class StyledDirective {
//    constructor(public el: ElementRef, public renderer: Renderer, @Host() parent: Component) {
//        console.warn(parent.styles);
//        for (var style of parent.styles)
//            renderer.setElementStyle(el.nativeElement, '', style);
//    }
//}

//@Pipe({ name: 'dataintercept', pure: false })
//export class XGridDataPipe implements PipeTransform {
//    transform(value, ...args: any[]): any {
//        return value;
//    }
//}
