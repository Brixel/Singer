import { Pipe, PipeTransform } from '@angular/core';
import { CostFilterParameter } from '../models/enum';

@Pipe({
   name: 'singerEventCostFilterParameterPipe',
})
export class SingerEventCostFilterParametersPipe implements PipeTransform {
   transform(value: CostFilterParameter | string): string {
      let filterParameter: CostFilterParameter;
      if (typeof value === 'string') {
         filterParameter = CostFilterParameter[value];
      } else {
         filterParameter = value;
      }

      let result = '';
      switch (filterParameter) {
         case CostFilterParameter.Free:
            result = 'Gratis';
            break;
         case CostFilterParameter.UpToFive:
            result = 'Max €5';
            break;
         case CostFilterParameter.UpToTen:
            result = 'Max €10';
            break;
         case CostFilterParameter.UpToFifteen:
            result = 'Max €15';
            break;
         case CostFilterParameter.UpToTwentyFive:
            result = 'Max €25';
            break;
         case CostFilterParameter.UpToFifty:
            result = 'Max €50';
            break;
      }
      return result;
   }
}
