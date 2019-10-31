import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {
   SingerRouterLink,
   singerRouterLinkRequirements,
} from '../../models/singer-routerlink.model';

@Component({
   selector: 'app-nav-menu',
   templateUrl: './nav-menu.component.html',
   styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
   @Output() logoutEvent = new EventEmitter();

   isAdmin: boolean;

   //Navbar Router Links
   routerLinks:SingerRouterLink[] = [
      {
         RouterLinkName: 'Start',
         RouterLinkRequirements: [singerRouterLinkRequirements.none],
         routerLink: '/dashboard',
      },
      {
         RouterLinkName: 'Voogden',
         RouterLinkRequirements: [
            singerRouterLinkRequirements.isAdmin,
            singerRouterLinkRequirements.isAuthenticated,
         ],
         routerLink: '/admin/voogden',
      },
      {
         RouterLinkName: 'Zorg gebruikers',
         RouterLinkRequirements: [
            singerRouterLinkRequirements.isAdmin,
            singerRouterLinkRequirements.isAuthenticated,
         ],
         routerLink: '/admin/zorggebruikers',
      },
      {
         RouterLinkName: 'Beheerders',
         RouterLinkRequirements: [
            singerRouterLinkRequirements.isAdmin,
            singerRouterLinkRequirements.isAuthenticated,
         ],
         routerLink: '/admin/beheerders',
      },
      {
         RouterLinkName: 'Evenementen',
         RouterLinkRequirements: [
            singerRouterLinkRequirements.isAdmin,
            singerRouterLinkRequirements.isAuthenticated,
         ],
         routerLink: '/admin/evenementen',
      },
      {
         RouterLinkName: 'Inloggen',
         RouterLinkRequirements: [
            singerRouterLinkRequirements.isNotAuthenticated,
         ],
         routerLink: '/login',
      },
   ];

   activeRouterLink: SingerRouterLink = this.routerLinks[0];

   emptyRouterLink: SingerRouterLink = {
      RouterLinkName: '',
      RouterLinkRequirements: [singerRouterLinkRequirements.none],
      routerLink: '/',
   };

   constructor(private authService: AuthService) {
      this.authService.isAdmin$.subscribe(res => {
         this.isAdmin = res;
      });
   }

   isAuthenticated(): boolean {
      return this.authService.isAuthenticated();
   }

   onLogoutClicked() {
      this.logoutEvent.emit();
   }

   // Checks a list of RouterLinkRequirements
   checkRequirements(requirements: singerRouterLinkRequirements[]): boolean {
      // Expect all requirements to be met
      let result = true;

      // Loop through the requirements
      requirements.forEach(requirement => {
         // If a requirement is not met: result = false
         if (!this.checkRequirement(requirement)) { result = false; }
      });

      // If all requirements are checked: return result
      return result;
   }

   // Checks an individual RouterLinkRequirement
   checkRequirement(requirement:string):boolean {
      if(requirement == singerRouterLinkRequirements.none) return true;
      if(requirement == singerRouterLinkRequirements.isAdmin) return this.isAdmin;
      if(requirement == singerRouterLinkRequirements.isAuthenticated) return this.isAuthenticated();
      if(requirement == singerRouterLinkRequirements.isNotAuthenticated) return !this.isAuthenticated();
   }
}
