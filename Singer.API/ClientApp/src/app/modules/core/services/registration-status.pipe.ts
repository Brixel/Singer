import { Pipe, PipeTransform } from '@angular/core';
import { RegistrationStatus } from '../models/enum';

@Pipe({
   name: 'registrationStatusPipe',
})
export class RegistrationStatusPipe implements PipeTransform {
   transform(value: RegistrationStatus | string): string {
      let registrationStatus: RegistrationStatus;
      if (typeof value === 'string') {
         registrationStatus = RegistrationStatus[value];
      } else {
         registrationStatus = value;
      }
      switch (registrationStatus) {
         case RegistrationStatus.Accepted:
            return 'Goedgekeurd';
         case RegistrationStatus.Pending:
            return 'In afwachting';
         case RegistrationStatus.Rejected:
            return 'Afgekeurd';
      }
   }
}
