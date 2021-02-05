import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
 
@Injectable()
export class AuthGuard implements CanActivate {
 
    constructor(private router: Router) { }
 
    canActivate() {
        if (sessionStorage.getItem('access_token')) {
            return true;
        }
 
        window.location.href = "https://stsdev.lcwaikiki.com/sts/issue/oauth2/authorize?client_id=Harmony&scope=urn:userinfo,claims&redirect_uri=https://localhost:4200/login&response_type=token";
        return false;
    }
}