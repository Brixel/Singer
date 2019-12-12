import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
   REQUIRED_VALIDATOR,
   RequiredValidator,
} from '@angular/forms/src/directives/validators';
import { comparePassword } from 'src/app/modules/core/utils/user-profile.validator';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { MatSnackBar } from '@angular/material';
import { config } from 'rxjs';

@Component({
   selector: 'app-reset-password',
   templateUrl: './reset-password.component.html',
   styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
   formGroup: FormGroup;
   userId: string;
   token: string;
   errorMessage: string;

   constructor(
      private _activatedRoute: ActivatedRoute,
      private _router: Router,
      private _authService: AuthService,
      private _snackbar: MatSnackBar
   ) {}

   ngOnInit() {
      this._activatedRoute.queryParams.subscribe(res => {
         this.userId = res['userId'];
         this.token = res['token'];
      });

      this.formGroup = new FormGroup({
         password: new FormControl('', Validators.required),
         passwordVerify: new FormControl('', Validators.required),
      });

      this.formGroup.setValidators([
         comparePassword()
      ]);

      this._authService.passwordResetError$.subscribe((res) => this.processError(res));
   }

   private processError(error: string){

      switch(error){
         case 'InvalidToken':
            this.errorMessage = 'De gebruikte URL is vervallen'
            break;
         default:
            this.errorMessage = 'Wijzigen van wachtwoord is mislukt';
            break;

      }
   }

   submit() {
      console.log(this.formGroup.controls);
      if (!this.formGroup.valid) {
         return;
      }
      const password = this.formGroup.controls.passwordVerify.value;
      this._authService.updatePassword(this.userId, this.token, password)
      .subscribe(
         () => {

            this._snackbar.open('Wachtwoord is gewijzigd. U wordt nu naar de login pagina doorgestuurd.',
            'OK',
              { duration: 2000}
            );
            this._router.navigateByUrl('login');
         },
         error => {
            this.processError(error.error);
         }
      );
   }
}
