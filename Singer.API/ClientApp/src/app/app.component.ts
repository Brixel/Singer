import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
   selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css'],
})
export class AppComponent {
   title = 'Singer';

   constructor(private router: Router) {}

   onLogout() {
      this.router.navigateByUrl('/dashboard').then(() => {
         // this.authService.logout();
      });
   }
}
