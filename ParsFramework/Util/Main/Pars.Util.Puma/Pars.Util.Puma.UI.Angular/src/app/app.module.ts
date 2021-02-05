﻿import { NgModule, TemplateRef } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ComponentBase, MessageText, UserContextService } from "./pars/core";
import { HttpService, WebApiObservableService } from "./pars/service";

import { AppComponent } from "./ui/app.component";
import { ClaimComponent } from "./claim/claim.component";
import { ClaimRoleComponent, LanguageChangerComponent } from "./claim/claim-role.component";
import { ClaimUserComponent } from "./claim/claim-user.component";
import { ClaimUserGroupComponent } from "./claim/claim-usergroup.component";
import { IndexComponent } from './index/index.component';

import { SupplierListComponent } from "./supplier/supplier-list.component";
import { SupplierUserListComponent } from "./supplier/supplier-user-list.component";
import { SupplierReportComponent } from "./supplier/supplier-report.component";
import { SupplierUserLoginReportComponent } from "./supplier/supplier-user-login-report.component";
import { UnAuthorizedComponent } from "./ui/unauthorized.component";
import { PageNotFoundComponent } from "./ui/page-not-found.component";
import { ErrorComponent } from "./ui/error.component";

import { LocalizeTableComponent, SelectionComponent } from "./dataLocalizer/localizeTable.component";
import { LocalizeRowComponent } from "./dataLocalizer/localizeRow.component";

import { DefinitionService } from "./services/definition.service";
import { FederationService } from "./services/federation.service";
import { SupplierService } from "./supplier/supplier.service";
import { SupplierUserLoginReportService } from "./supplier/supplier-user-login-report.service";

import { AppRoutingModule } from "./app-routing.module";
import { HttpModule } from "@angular/http";
import { SimpleNotificationsModule } from "angular2-notifications";

import { PagingComponent } from "./common/paging.component";
import { UserSearchComponent } from "./common/user-search.component";
import { AutoCompleteComponent } from "./common/auto-complete.component";
import { SpinnerComponent } from "./common/spinner.component";
import { GridComponent, GridColumnComponent } from "./common/grid.component";
import { XGridComponent, XGridColumnComponent, XGridPagingComponent } from "./common/xgrid.component";
import { LcwGridComponent, LcwGridColumnComponent, LcwGridSummaryComponent, LcwGridAutoGeneratedColumnComponent } from "./common/lcw-grid/lcw-grid.component";
import { LcwGridPagingComponent } from "./common/lcw-grid/lcw-grid.paging.component";
import { LcwExportButtonComponent } from "./common/lcw-export-button.component";

import { XModalComponent } from "./common/xmodal.component";
import { DateWeekComponent } from "./common/date-week.component";

//import { TestingModule } from './testing/testing.module';
import { TestComponent } from "./supplier/test.component";
import { ChildSelectComponent } from "./supplier/child-select.component";

//import { PopoverModule } from "ngx-popover";
import { TableExportService } from './pars/data';
//import { LcwGridHeaderInternalDirective } from "./common/lcw-grid/lcw-grid-header-internal.directive";
import { LoginComponent } from './login/login.component';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        //BrowserAnimationsModule,
        SimpleNotificationsModule,
        /*TestingModule,*/
        //PopoverModule,
        AppRoutingModule, //THIS MODULE MUST BE THE LAST IN IMPORTS DUE TO IT HAS ROUTING WILDCARDS
    ],
    declarations: [
        ComponentBase,
        UnAuthorizedComponent,
        PageNotFoundComponent,
        AppComponent,
        ClaimComponent,
        ClaimRoleComponent,
        ClaimUserComponent,
        ClaimUserGroupComponent,
        SupplierListComponent,
        SupplierUserListComponent,
        SupplierReportComponent,
        SupplierUserLoginReportComponent,
        SpinnerComponent,
        PagingComponent,
        GridComponent,
        GridColumnComponent,
        XGridComponent,
        XGridColumnComponent,
        XGridPagingComponent,
        LoginComponent,

        LcwGridComponent,
        LcwGridColumnComponent,
        LcwGridPagingComponent,
        LcwGridSummaryComponent,
        LcwGridAutoGeneratedColumnComponent,
        LcwExportButtonComponent,
        //LcwGridHeaderInternalDirective,

        XModalComponent,
        UserSearchComponent,
        AutoCompleteComponent,
        DateWeekComponent,
        LanguageChangerComponent,
        MessageText,
        ErrorComponent,
        IndexComponent,
        LocalizeTableComponent,
        LocalizeRowComponent,
        SelectionComponent,
        TestComponent,
        ChildSelectComponent
    ],
    //exports:[LcwGridHeaderInternalDirective],
    providers: [
        TableExportService,
        UserContextService,
        DefinitionService,
        SupplierService,
        SupplierUserLoginReportService,
        FederationService,
        HttpService,
        WebApiObservableService,
        //LcwGridHeaderInternalDirective
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }