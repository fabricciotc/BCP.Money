import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate{

  constructor(private accountService: AccountService,
    private router: Router) { }


canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.accountService.estaLogueado()) {
        return true;
    } else {
        this.router.navigate(['/login']);
        return false;
    }
}
}
