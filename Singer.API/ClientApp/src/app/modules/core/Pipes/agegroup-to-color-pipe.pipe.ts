import { Pipe, PipeTransform } from '@angular/core';
import { AgeGroup } from '../models/enum';

@Pipe({
   name: 'agegroupToColorPipe',
})
export class AgegroupToColorPipePipe implements PipeTransform {
   transform(value: any, args?: any): any {
      let ageGroup: AgeGroup;
      if (typeof value === 'string') {
         ageGroup = AgeGroup[value];
      } else {
         ageGroup = value;
      }

      let result = '';
      switch (ageGroup) {
         case AgeGroup.Toddler:
            result = '#991';
            break;
         case AgeGroup.Kindergartener:
            result = '#e03';
            break;
         case AgeGroup.Child:
            result = '#4ab';
            break;
         case AgeGroup.Youngster:
            result = '#294';
            break;
         case AgeGroup.Adult:
            result = '#4ab';
            break;
      }
      return result;
   }
}
