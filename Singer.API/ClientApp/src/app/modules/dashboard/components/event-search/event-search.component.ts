import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';
import { GenericFilter } from 'src/app/modules/core/components/Generics/generic-filter.component';
import { GenericFilterParameters } from 'src/app/modules/core/models/generics/generic-filter-parameters.model';

@Component({
   selector: 'app-event-search',
   templateUrl: './event-search.component.html',
   styleUrls: ['./event-search.component.css'],
})
export class EventSearchComponent extends GenericFilter {
   @Output()
   get filterEvent(): EventEmitter<GenericFilterParameters> {
      return this.genericFilterEvent;
   }

   currentDate = new Date();
   availableLocations: SingerEventLocation[];

   constructor() {
      super();
   }

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
      const location = this.formControlGroup.controls.locationControl.value as SingerEventLocation;
      const searchEventData = <SearchEventData>{
         startDateTime: this.formControlGroup.controls.startDateControl.value,
         endDateTime: this.formControlGroup.controls.endDateControl.value,
         locationId: location.id,
      };

   }

   resetFilter() {
      throw new Error('Method not implemented.');
   }
}

export class SearchEventData {
   startDateTime: Date;
   endDateTime: Date;
   locationId: string;
}
