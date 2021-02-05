import { CanActivate, ActivatedRouteSnapshot, ActivatedRoute, RouterStateSnapshot, Router, NavigationExtras } from '@angular/router';
import { Injectable } from '@angular/core';
import { FederationService, FederationClaims } from "../services/federation.service";
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from "rxjs/Rx";
import { UserContextService } from "../pars/core";
import {
    SupplierService,
    ValidateUserRequest, ValidateUserResponse
} from "./supplier.service";

let navigationExtras: NavigationExtras = {
    preserveQueryParams: true,
    preserveFragment: true
};


@Injectable()
export class SupplierUserListRouteGuard implements CanActivate {

    constructor(private ucService: UserContextService, private router: Router, private activatedRoute: ActivatedRoute, private supplierService: SupplierService) {
        console.log("entered constructor of SupplierUserListRouteGuard");
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
       
        let request = new ValidateUserRequest();
        request.userRef = this.ucService.userContext.userRef;
        request.supplierCode = route.params["id"];

        return this.supplierService.validateUser(request)
            .map(response => {
                if ((response.isValid && this.ucService.hasClaim(FederationClaims.Vrp_TedPort_Yonetici)) || this.ucService.hasClaim(FederationClaims.Vrp_TedPort_Admin))
                    return true;
                this.router.navigate(["Unauthorized"], navigationExtras);
                return false;
            })
            .first();
    }
}

