import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
   selector: 'app-account-info-base',
   templateUrl: './account-info-base.component.html',
   styleUrls: ['./account-info-base.component.css'],
})
export class AccountInfoBaseComponent implements OnInit {

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
      }
   ];

   constructor(public router: Router) {}

   ngOnInit() {}

   navigate(url: string) :void {
      this.router.navigateByUrl(url);
   }
}

interface NavbarLinks {
   displayIcon: string,
   displayName: string,
   url: string,
}
