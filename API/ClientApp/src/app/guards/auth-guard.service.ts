import { ThisReceiver } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private _router: Router, private _jtwHelper: JwtHelperService) { }

  canActivate() {
    const token = localStorage.getItem('jwt');

    if (token && !this._jtwHelper.isTokenExpired(token)) {
      return true;
    }

    this._router.navigate(['login'])
    return false;
  }
}
