import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {
   SingerRouterLink,
   singerRouterLinkRequirements,
} from '../../models/singer-routerlink.model';
import { Router } from '@angular/router';

@Component({
   selector: 'app-nav-menu',
   templateUrl: './nav-menu.component.html',
   styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit {
   @Output() logoutEvent = new EventEmitter();

   isAdmin: boolean;

   // Navbar Router Links
   routerLinks: SingerRouterLink[] = [
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

   isAuthenticated: boolean;

   routerLinkRequirements: { [name: string]: boolean } = {};

   constructor(private authService: AuthService, public router: Router) {}

   ngOnInit() {
      this.authService.isAdmin$.subscribe(res => {
         this.isAdmin = res;
         this.updateRequirements();
      });
      this.authService.isAuthenticated$.subscribe(res => {
         this.isAuthenticated = res;
         this.updateRequirements();
      });
   }

   onLogoutClicked() {
      this.updateRequirements();
      this.logoutEvent.emit();
   }

   private updateRequirements() {
      const routerLinkRequirements: { [name: string]: boolean } = {};
      this.routerLinks.forEach(routerLink => {
         const routerLinkRequirement = this.routerLinkRequirements[
            routerLink.RouterLinkName
         ];
         if (routerLinkRequirement === undefined) {
            routerLinkRequirements[routerLink.RouterLinkName] = false;
         }
         const isValid = this.checkRequirements(
            routerLink.RouterLinkRequirements
         );
         routerLinkRequirements[routerLink.RouterLinkName] = isValid;
      });
      this.routerLinkRequirements = routerLinkRequirements;
   }

   // Checks a list of RouterLinkRequirements
   checkRequirements(requirements: singerRouterLinkRequirements[]): boolean {
      // Expect all requirements to be met
      let result = true;

      // Loop through the requirements
      requirements.forEach(requirement => {
         // If a requirement is not met: result = false
         if (!this.checkRequirement(requirement)) {
            result = false;
         }
      });

      // If all requirements are checked: return result
      return result;
   }

   // Checks an individual RouterLinkRequirement
   checkRequirement(requirement: string): boolean {
      if (requirement === singerRouterLinkRequirements.none) {
         return true;
      }
      if (requirement === singerRouterLinkRequirements.isAdmin) {
         return this.isAdmin;
      }
      if (requirement === singerRouterLinkRequirements.isAuthenticated) {
         return this.isAuthenticated;
      }
      if (requirement === singerRouterLinkRequirements.isNotAuthenticated) {
         return !this.isAuthenticated;
      }
   }
}
