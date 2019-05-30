import { Component } from '@angular/core';
import { AuthService } from './modules/core/services/auth.service';

@Component({
   selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css'],
})
export class AppComponent {
   title = 'Singer';
   constructor(private authService:AuthService) {

   }
   onLogout(){
      this.authService.logout();
   }
}

