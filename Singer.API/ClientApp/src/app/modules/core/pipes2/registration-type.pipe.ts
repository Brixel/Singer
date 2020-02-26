import { RegistrationType } from '../enums/registration-type';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
   name: 'registrationType',
})
export class RegistrationTypePipe implements PipeTransform {
   transform(value: RegistrationType): string {
      switch (value) {
         case RegistrationType.DayCare:
            return 'Dagopvang';
         case RegistrationType.EventSlotDriven:
            return 'Evenement';
         case RegistrationType.NightCare:
            return 'Nachtopvang';
      }
   }
}
