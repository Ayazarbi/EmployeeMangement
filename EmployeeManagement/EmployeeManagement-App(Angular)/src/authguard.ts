import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Authservice } from './app/Authservice';
@Injectable({
  providedIn:"root"

})
export class authguard implements CanActivate{

  constructor(private _auth:Authservice,
              private route:Router){}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    if(this._auth.checklogin()){
      return true;
    }
    else{
    this.route.navigate(['/login']);
    return false;

    }

  }

}
