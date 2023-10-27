import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject, Observable, BehaviorSubject, of } from 'rxjs';
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
         // shouldShow: new BehaviorSubject<boolean>(true),
         shouldShow: new BehaviorSubject<boolean>(true),
      },
      {
         displayIcon: 'group',
         displayName: 'Mijn zorggebruikers',
         url: '/dashboard/account/zorggebruikers',
         // shouldShow: this._authService.isLegalGuardian$,
         shouldShow: new BehaviorSubject<boolean>(true),
      },
      {
         displayIcon: 'calendar_today',
         displayName: 'Registratieoverzicht',
         url: '/dashboard/registratie-overzicht',
         // shouldShow: this._authService.isLegalGuardian$,
         shouldShow: new BehaviorSubject<boolean>(true),
      },
   ];

   constructor(public router: Router) {}

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
