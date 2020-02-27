import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { ReplaySubject, Observable, BehaviorSubject } from 'rxjs';
import { startWith } from 'rxjs/operators';

@Component({
   selector: 'app-account-info-page',
   templateUrl: './account-info-page.component.html',
   styleUrls: ['./account-info-page.component.css'],
})
export class AccountInfoPageComponent {
   isAdmin: ReplaySubject<boolean> = new ReplaySubject<boolean>();
   navbarLinks: NavbarLink[] = [
      {
         displayIcon: 'assignment_ind',
         displayName: 'Mijn persoonlijke gegevens',
         url: '/dashboard/account/account-info',
         shouldShow: new BehaviorSubject<boolean>(true),
      },
      {
         displayIcon: 'group',
         displayName: 'Mijn zorggebruikers',
         url: '/dashboard/account/zorggebruikers',
         shouldShow: this._authService.isLegalGuardian$,
      },
      {
         displayIcon: 'calendar_today',
         displayName: 'Registratieoverzicht',
         url: '/dashboard/registratie-overzicht',
         shouldShow: this._authService.isLegalGuardian$,
      },
   ];

   constructor(public router: Router, private _authService: AuthService) {}

   navigate(url: string): void {
      this.router.navigateByUrl(url);
   }
}

interface NavbarLink {
   displayIcon: string;
   displayName: string;
   url: string;
   shouldShow: BehaviorSubject<boolean>;
}
