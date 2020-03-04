import { AbstractControl, ValidationErrors } from '@angular/forms';

export interface FormElement {
   name: string;
   placeholder: string;
   validators: [(control: AbstractControl) => ValidationErrors];
}
