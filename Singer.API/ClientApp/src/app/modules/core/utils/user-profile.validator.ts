import { ValidatorFn, AbstractControl } from '@angular/forms';

export function comparePassword(): ValidatorFn {
   return (
      control: AbstractControl
   ): {
      [key: string]: any;
   } | null => {
      const password = control.get('password');
      const verifyPassword = control.get('passwordVerify');

      if (password.value !== verifyPassword.value) {
         verifyPassword.setErrors({ passwordsDontMatch: true });
      }

      return verifyPassword.value !== password.value ? { passwordsDontMatch: true } : null;
   };
}
