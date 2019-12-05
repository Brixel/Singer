import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
   REQUIRED_VALIDATOR,
   RequiredValidator,
} from '@angular/forms/src/directives/validators';
import { comparePassword } from 'src/app/modules/core/utils/user-profile.validator';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-reset-password',
   templateUrl: './reset-password.component.html',
   styleUrls: ['./reset-password.component.css'],
})
export class ResetPasswordComponent implements OnInit {
   formGroup: FormGroup;
   userId: string = '60B781DD-40D4-4F1D-9C5E-08D73ECA4147';
   token: string = 'b';

   constructor(
      private _activatedRoute: ActivatedRoute,
      private authService: AuthService
   ) {}

   ngOnInit() {
      // this._activatedRoute.queryParams.subscribe(res => {
      //    this.userId = res['id'];
      //    this.token = res['token'];
      // });
      this.formGroup = new FormGroup({
         password: new FormControl('', Validators.required),
         passwordVerify: new FormControl('', Validators.required),
      });

      this.formGroup.setValidators([
         comparePassword()
      ]);
   }

   submit() {
      console.log(this.formGroup.controls);
      if (!this.formGroup.valid) {
         return;
      }
      const password = this.formGroup.controls.passwordVerify.value;
      this.authService.updatePassword(this.userId, this.token, password);
   }
}
