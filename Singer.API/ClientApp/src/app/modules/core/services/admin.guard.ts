import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AdminAuthGuard implements CanActivate {
   constructor(private authService: AuthService, private router: Router) {}
   canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot
   ): Observable<boolean> | Promise<boolean> | boolean {
      return this.authService.isAdmin$.pipe(
         tap((isAdmin) => {
            if (isAdmin) {
               return true;
            }
            this.router.navigate(['/login'], {
               queryParams: { returnUrl: state.url },
            });
         })
      );
   }
}
