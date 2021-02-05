import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ClaimComponent } from './claim/claim.component';
import { IndexComponent } from './index/index.component';
import { SupplierListComponent } from "./supplier/supplier-list.component";
import { SupplierListRouteGuard } from "./supplier/supplier-list.guard";
import { SupplierUserListComponent } from './supplier/supplier-user-list.component';
import { SupplierUserListRouteGuard } from "./supplier/supplier-user-list.guard";
import { SupplierReportComponent } from "./supplier/supplier-report.component";
import { SupplierReportRouteGuard } from "./supplier/supplier-report.guard";
import { SupplierUserLoginReportComponent } from "./supplier/supplier-user-login-report.component";
import { SupplierUserLoginReportRouteGuard } from "./supplier/supplier-user-login-report.guard";
import { LocalizeTableComponent } from "./dataLocalizer/localizeTable.component";
import { LocalizeRowComponent } from "./dataLocalizer/localizeRow.component";
import { FederationService } from "./services/federation.service";

import { AppComponent } from './ui/app.component';
import { UnAuthorizedComponent } from "./ui/unauthorized.component";
import { PageNotFoundComponent } from "./ui/page-not-found.component";
import { ErrorComponent } from "./ui/error.component";
import { TestComponent } from "./supplier/test.component";

const routes: Routes = [
    { path: '', redirectTo: 'Supplier', pathMatch: 'full' },
    { path: 'Claim', component: ClaimComponent },
    { path: 'Home/Supplier', redirectTo: 'Supplier', pathMatch: 'full' },
    { path: 'Home/SupplierDetail/:id', redirectTo: 'SupplierDetail/:id', pathMatch: 'full' },
    { path: 'Supplier', component: SupplierListComponent, canActivate: [SupplierListRouteGuard] },
    { path: 'SupplierDetail/:id', component: SupplierUserListComponent, canActivate: [SupplierUserListRouteGuard] },
    { path: 'SupplierReport', component: SupplierReportComponent, canActivate: [SupplierReportRouteGuard] },
    { path: 'SupplierUserLoginReport', component: SupplierUserLoginReportComponent, canActivate: [SupplierUserLoginReportRouteGuard] },
    { path: 'LocalizeTable', component: LocalizeTableComponent },
    { path: 'LocalizeRow/:objRef/:objId/:lngRef', component: LocalizeRowComponent },
    { path: 'Test', component: TestComponent },
    { path: 'Index', component: IndexComponent },
    { path: 'Unauthorized', component: UnAuthorizedComponent },
    { path: 'Error', component: ErrorComponent },
    { path: '**', component: PageNotFoundComponent },
];

export const AUTH_PROVIDERS = [SupplierListRouteGuard, SupplierUserListRouteGuard, SupplierReportRouteGuard, SupplierUserLoginReportRouteGuard, FederationService];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [AUTH_PROVIDERS]
})
export class AppRoutingModule { }


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/