// import { Injectable } from '@angular/core';
// import { SupplierReportRouteGuard } from "./supplier-report.guard";
// import { UserContextService } from "../pars/core";
// import { Router } from "@angular/router";

// @Injectable()
// export class SupplierUserLoginReportRouteGuard extends SupplierReportRouteGuard {
//      constructor(private ucService: UserContextService, private router: Router) { 
//          super(ucService,router);
//      }
// }


import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, NavigationExtras, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { FederationService, FederationClaims } from "../services/federation.service";
import { UserContextService } from "../pars/core";

let navigationExtras: NavigationExtras = {
    preserveQueryParams: true,
    preserveFragment: true
};


@Injectable()
export class SupplierUserLoginReportRouteGuard implements CanActivate {
    constructor(private ucService: UserContextService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let authorized: boolean = this.ucService.userContext.hasClaim(FederationClaims.Vrp_TedPort_Admin);
        if (!authorized)
            this.router.navigate(["Unauthorized"], navigationExtras);
        return authorized;
    }
}

