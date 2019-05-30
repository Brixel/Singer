import { FormGroup, FormControl } from '@angular/forms';
import { OnInit, Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({

   selector: 'app-auth-component',
   templateUrl: './auth.component.html',
   styleUrls: ['./auth.component.css'],
})

export class AuthComponent implements OnInit {
   form: FormGroup;
   private returnUrl: string;
   constructor(
      private authService: AuthService,
      private router: Router,
      private activated: ActivatedRoute
   ) {}

   ngOnInit() {
      this.activated.queryParams.subscribe(params => {
         this.returnUrl = params['returnUrl'];
      });

      this.form = new FormGroup({
         username: new FormControl(null),
         password: new FormControl(null),
      });
   }

   submit() {
      const username = this.form.get('username').value;
      const password = this.form.get('password').value;
      console.log(' hit');
      this.authService.authenticate(username, password).subscribe(
         () => {
            const url = this.returnUrl || '/';
            this.router.navigate([url]);
         },
         error => {
            this.form.setErrors({
               notAuthorized: true,
            });
         }
      );
   }
}
