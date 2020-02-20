import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
   selector: 'app-account-info-page',
   templateUrl: './account-info-page.component.html',
   styleUrls: ['./account-info-page.component.css'],
})
export class AccountInfoPageComponent {
   navbarLinks: NavbarLinks[] = [
      {
         displayIcon: 'assignment_ind',
         displayName: 'Mijn persoonlijke gegevens',
         url: '/dashboard/account/account-info',
      },
      {
         displayIcon: 'group',
         displayName: 'Mijn zorggebruikers',
         url: '/dashboard/account/zorggebruikers',
      },
   ];

   constructor(public router: Router) {}

   navigate(url: string): void {
      this.router.navigateByUrl(url);
   }
}

interface NavbarLinks {
   displayIcon: string;
   displayName: string;
   url: string;
}
