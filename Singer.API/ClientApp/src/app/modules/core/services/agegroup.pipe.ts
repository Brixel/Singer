import { Pipe, PipeTransform } from '@angular/core';
import { AgeGroup } from '../models/enum';

@Pipe({
  name: 'agegroupPipe'
})
export class AgegroupPipe implements PipeTransform {

  transform(value: AgeGroup | string): string {
   let ageGroup: AgeGroup;
   if (typeof value === 'string') {
      ageGroup = AgeGroup[value];
   } else {
      ageGroup = value;
   }

   let result = '';
   switch (ageGroup) {
      case AgeGroup.Toddler:
         result = 'Kleuters';
         break;
      case AgeGroup.Child:
         result = 'Kinderen';
         break;
      case AgeGroup.Youngster:
         result = 'Jongeren';
         break;
      case AgeGroup.Adult:
         result = 'Volwassenen';
         break;
   }
   return result;
  }

}
