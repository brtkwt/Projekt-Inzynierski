import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorisationGuard implements CanActivate {

  constructor(private ruter: Router, private acService:AccountService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.acService.loggedUserSource$.pipe(

      map(inside => {
        if(inside){
          return true;

        }
        else{
          this.ruter.navigateByUrl("/konto/logowanie");

          return false;
        }
      })
    );
  }
  
}
