import { Component, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
   selector: 'app-nav-menu',
   templateUrl: './nav-menu.component.html',
   styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent {
   isExpanded = false;

   @Output() logoutEvent = new EventEmitter();

   constructor(private authService: AuthService) {

   }

   isAuthenticated(): boolean{
      return this.authService.isAuthenticated();
   }

   onLogoutClicked(){
      this.logoutEvent.emit();
   }

   collapse() {
      this.isExpanded = false;
   }

   toggle() {
      this.isExpanded = !this.isExpanded;
   }
}
