import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';

@Component({
   selector: 'app-event-search',
   templateUrl: './event-search.component.html',
   styleUrls: ['./event-search.component.css'],
})
export class EventSearchComponent implements OnInit {
   @Input() availableLocations: SingerEventLocation[];
   @Input() isOpen: boolean;
   @Output() searchEvent: EventEmitter<SearchEventData> = new EventEmitter();
   @Output() toggleDrawerEvent: EventEmitter<boolean> = new EventEmitter(
      this.isOpen
   );
   currentDate = new Date();

   constructor() {}

   formControlGroup: FormGroup = new FormGroup({
      // Form controls
      startDateControl: new FormControl({ value: '', disabled: true }),
      endDateControl: new FormControl({ value: '', disabled: true }),
      locationControl: new FormControl(''),
   });

   ngOnInit() {}

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

   toggleDrawer() {
      this.isOpen = !this.isOpen;
      this.toggleDrawerEvent.emit(this.isOpen);
   }

   getRequiredFieldErrorMessage(formControl: FormControl) {
      return formControl.hasError('required') ? 'Dit veld is verplicht' : '';
   }
}

export class SearchEventData {
   startDateTime: Date;
   endDateTime: Date;
   locationId: string;
}
