import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'keys'})
export class KeysPipe implements PipeTransform {
  transform(value, args: string[]): any {
    const keys = [];
    for (const enumMember in value) {
      const key = parseInt(enumMember, 10);
      if (!isNaN(key)) {
         keys.push({key: key, value: value[enumMember]});
      }
    }
    return keys;
  }
}
