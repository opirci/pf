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

import { LoginComponent } from './login/login.component';
import { AuthGuard } from './login/auth.guard';

const routes: Routes = [
    { path: '', redirectTo: 'Supplier', pathMatch: 'full', canActivate: [AuthGuard] },
    { path: 'Home/Claim', component: ClaimComponent , canActivate: [AuthGuard] },
    { path: 'Home/Supplier', redirectTo: 'Supplier', pathMatch: 'full', canActivate: [AuthGuard]  },
    { path: 'Home/SupplierDetail/:id', redirectTo: 'SupplierDetail/:id', pathMatch: 'full', canActivate: [AuthGuard] },
    { path: 'Supplier', component: SupplierListComponent, canActivate: [AuthGuard,SupplierListRouteGuard] },
    { path: 'SupplierDetail/:id', component: SupplierUserListComponent, canActivate: [AuthGuard,SupplierUserListRouteGuard] },
    { path: 'SupplierReport', component: SupplierReportComponent, canActivate: [AuthGuard,SupplierReportRouteGuard] },
    { path: 'SupplierUserLoginReport', component: SupplierUserLoginReportComponent, canActivate: [AuthGuard,SupplierUserLoginReportRouteGuard] },
    { path: 'LocalizeTable', component: LocalizeTableComponent , canActivate: [AuthGuard] },
    { path: 'LocalizeRow/:objRef/:objId/:lngRef', component: LocalizeRowComponent , canActivate: [AuthGuard] },
    { path: 'Test', component: TestComponent , canActivate: [AuthGuard] },
    { path: 'Index', component: IndexComponent , canActivate: [AuthGuard] },
    { path: 'Unauthorized', component: UnAuthorizedComponent , canActivate: [AuthGuard] },
    { path: 'Error', component: ErrorComponent , canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: '**', component: PageNotFoundComponent, canActivate: [AuthGuard] },
];

export const AUTH_PROVIDERS = [AuthGuard, SupplierListRouteGuard, SupplierUserListRouteGuard, SupplierReportRouteGuard, SupplierUserLoginReportRouteGuard, FederationService];

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