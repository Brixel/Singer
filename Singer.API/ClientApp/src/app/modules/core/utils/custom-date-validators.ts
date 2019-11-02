import { ValidatorFn, AbstractControl } from '@angular/forms';

export function dateNotBefore(date: Date): ValidatorFn {
   return (control: AbstractControl): { [key: string]: any } | null => {
      const formDate = new Date(control.value);
      return formDate < date ? { dateNotBefore: { value: formDate } } : null;
   };
}

export function dateNotAfter(date: Date): ValidatorFn {
   return (control: AbstractControl): { [key: string]: any } | null => {
      const formDate = new Date(control.value);
      return formDate > date ? { dateNotBefore: { value: formDate } } : null;
   };
}

export function dateBetween(first: Date, last: Date) {
   return (control: AbstractControl): { [key: string]: any } | null => {
      const formDate = new Date(control.value);
      return formDate < first || formDate > last
         ? { dateNotBefore: { value: formDate } }
         : null;
   };
}
