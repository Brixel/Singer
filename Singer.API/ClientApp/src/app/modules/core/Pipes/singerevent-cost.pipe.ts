import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
   name: 'singereventCost',
})
export class SingereventCostPipe implements PipeTransform {
   transform(value: number): string {
      if (value === 0) {
         return 'Gratis';
      }
      return '€' + value;
   }
}
