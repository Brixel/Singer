import { FormGroup, FormControl } from '@angular/forms';
import { OnInit, Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { LoadingService } from 'src/app/modules/core/services/loading.service';

@Component({
   selector: 'app-auth-component',
   templateUrl: './auth.component.html',
   styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
   form: FormGroup;
   private returnUrl: string;
   constructor(
      private _authService: AuthService,
      private _router: Router,
      private _activated: ActivatedRoute,
      private _loadingService: LoadingService
   ) {}

   ngOnInit() {
      this._activated.queryParams.subscribe(params => {
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
      this._loadingService.show();
      this._authService.authenticate(username, password).subscribe(
         () => {
            this._loadingService.hide();
            const url = this.returnUrl || '/';
            this._router.navigate([url]);
         },
         error => {
            this._loadingService.hide();
            this.form.setErrors({
               notAuthorized: true,
            });
         }
      );
   }
}
