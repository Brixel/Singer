import { Component, OnInit, Input } from '@angular/core';
import { AgeGroup } from '../../models/enum';

@Component({
  selector: 'app-agegroup-chips',
  templateUrl: './agegroup-chips.component.html',
  styleUrls: ['./agegroup-chips.component.css']
})
export class AgegroupChipsComponent implements OnInit {

   @Input() agegroups: AgeGroup[];
   @Input() agegroup: AgeGroup;

  constructor() { }

  ngOnInit() {
  }

  getClass(ageGroup: AgeGroup) {
   let result = '';
   switch (ageGroup) {
      case AgeGroup.Toddler:
         result = 'agegroup-toddler';
         break;
      case AgeGroup.Kindergartener:
         result = 'agegroup-kindergartener';
         break;
      case AgeGroup.Child:
         result = 'agegroup-child';
         break;
      case AgeGroup.Youngster:
         result = 'agegroup-youngster';
         break;
      case AgeGroup.Adult:
         result = 'agegroup-adult';
         break;
   }
   return result;
  }

}
