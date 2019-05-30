import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
})

export class HomeComponent implements OnInit {

constructor(private authService: AuthService) {

}

ngOnInit(): void {
   if (this.authService.isAuthenticated()) {
      this.authService.getUserInfo().subscribe((res) => {
         console.log(res);
      });
   }
}
}
