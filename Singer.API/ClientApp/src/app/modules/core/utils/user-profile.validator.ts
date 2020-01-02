import { ValidatorFn, AbstractControl } from '@angular/forms';

export function comparePassword(): ValidatorFn {
   return (
      control: AbstractControl
   ): {
      [key: string]: any;
   } | null => {
      const password = control.get('password').value;
      const verifyPassword = control.get('passwordVerify').value;
      return verifyPassword !== password
         ? { passwordDontMatch: { value: true } }
         : null;
   };
}