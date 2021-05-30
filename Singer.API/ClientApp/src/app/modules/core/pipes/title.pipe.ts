import { Pipe, PipeTransform } from '@angular/core';
import { RegistrationType } from '../enums/registration-type';
import { RegistrationTypePipe } from './registration-type.pipe';

@Pipe({
  name: 'title'
})
export class TitlePipe implements PipeTransform {

  constructor(private registrationTypePipe: RegistrationTypePipe) {


  }
  transform(value: string | undefined | null, ...args: unknown[]): unknown {
    if (value) {
      return value;
    }
    if (args[0]) {
      const eventType = args[0] as RegistrationType;
      return this.registrationTypePipe.transform(eventType);
    }
  }

}
