import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';
import { GenericFilter } from 'src/app/modules/core/components/Generics/generic-filter.component';

@Component({
   selector: 'app-event-search',
   templateUrl: './event-search.component.html',
   styleUrls: ['./event-search.component.css'],
})
export class EventSearchComponent {

   @Input() availableLocations: SingerEventLocation[];
   @Output() filterEvent: EventEmitter<GenericFilter> = new EventEmitter();
   currentDate = new Date();

   constructor() {}

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      startDateControl: new FormControl({ value: null, disabled: true }),
      endDateControl: new FormControl({ value: null, disabled: true }),
      locationControl: new FormControl(''),
   });

   submitForm() {
      if (this.formControlGroup.invalid) {
         return;
      }
      const location = this.formControlGroup.controls.locationControl
         .value as SingerEventLocation;
      const searchEventData = <SearchEventData>{
         startDateTime: this.formControlGroup.controls.startDateControl.value,
         endDateTime: this.formControlGroup.controls.endDateControl.value,
         locationId: location.id,
      };
      this.searchEvent.emit(searchEventData);
   }
}

export class SearchEventData {
   startDateTime: Date;
   endDateTime: Date;
   locationId: string;
}
