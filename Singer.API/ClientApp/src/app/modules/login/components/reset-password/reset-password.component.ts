import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { comparePassword } from 'src/app/modules/core/utils/user-profile.validator';

@Component({
   selector: 'app-reset-password',
   templateUrl: './reset-password.component.html',
   styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
   formGroup: UntypedFormGroup;
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

      this.formGroup = new UntypedFormGroup({
         password: new UntypedFormControl('', Validators.required),
         passwordVerify: new UntypedFormControl('', Validators.required),
      });

      this.formGroup.setValidators([comparePassword()]);

      this._authService.passwordResetError$.subscribe(res => this.processError(res));
   }

   private processError(error: string) {
      switch (error) {
         case 'InvalidToken':
            this.errorMessage = 'De gebruikte URL is vervallen';
            break;
         default:
            this.errorMessage = 'Wijzigen van wachtwoord is mislukt';
            break;
      }
   }

   submit() {
      if (!this.formGroup.valid) {
         return;
      }
      const password = this.formGroup.controls.passwordVerify.value;
      this._authService.updatePassword(this.userId, this.token, password).subscribe(
         () => {
            this._snackbar.open('Wachtwoord is gewijzigd. U wordt nu naar de login pagina doorgestuurd.', 'OK', {
               duration: 2000,
            });
            this._router.navigateByUrl('login');
         },
         error => {
            this.processError(error.error);
         }
      );
   }
}
