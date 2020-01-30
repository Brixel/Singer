import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';

@Injectable({
   providedIn: 'root',
})
export class AdminAuthGuard implements CanActivate {
   constructor(private authService: AuthService, private router: Router) {}
   canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot
   ): Observable<boolean> | Promise<boolean> | boolean {
      return this.authService.isAdmin$.pipe(
         map(res => {
            if (res) {
               return true;
            } else {
               this.router.navigate(['/']);
               return false;
            }
         })
      );
   }
}
