import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Pipe({
   name: 'singereventCost',
})
export class SingereventCostPipe implements PipeTransform {
   decimalPipe: DecimalPipe = new DecimalPipe('be');

   transform(value: number): string {
      if (value === 0) {
         return 'Gratis';
      }
      return '€' + this.decimalPipe.transform(value, '1.2-2');
   }
}
