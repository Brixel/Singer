import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-dashboard',
   templateUrl: './dashboard.component.html',
   styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
   isAuthenticated:boolean = false;
   isAdmin:boolean = false;

   constructor(private authService: AuthService) {}

   ngOnInit() {
      this.authService.isAuthenticated$.subscribe(res => {
         this.isAuthenticated = res;
      });

      this.authService.isAuthenticated();

      this.authService.isAdmin$.subscribe(res => {
         this.isAdmin = res;
      });
   }
}
