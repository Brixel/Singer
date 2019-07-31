import { Pipe, PipeTransform } from '@angular/core';
import { AgeGroup } from '../models/enum';

@Pipe({
   name: 'ageGroupArray',
})
export class AgeGroupArrayPipe implements PipeTransform {
   transform(value: AgeGroup[], args?: any): string {}
}
