import { Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { SingerEventLocation, EventFilterParameters } from 'src/app/modules/core/models/singerevent.model';
import { GenericFilter } from 'src/app/modules/core/models/generics/generic-filter.model';
import { GenericFilterParameters } from 'src/app/modules/core/models/generics/generic-filter-parameters.model';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { AgeGroup, CostFilterParameter } from 'src/app/modules/core/models/enum';

@Component({
   selector: 'app-event-filter',
   templateUrl: './event-filter.component.html',
   styleUrls: ['./event-filter.component.css'],
})
export class EventFilterComponent extends GenericFilter implements OnInit {
   @Output()
   get filterEvent(): EventEmitter<GenericFilterParameters> {
      return this.genericFilterEvent;
   }

   currentDate = new Date();
   availableLocations: SingerEventLocation[];
   // Available agegroups
   ageGroups = Object.keys(AgeGroup).filter(k => typeof AgeGroup[k as any] === 'number');
   // Available cost filter parameters
   costFilterParameters = Object.keys(CostFilterParameter).filter(
      k => typeof CostFilterParameter[k as any] === 'number'
   );

   // Form placeholders
   startDateFieldPlaceholder: string = 'Start Datum';
   endDateFieldPlaceholder: string = 'Eind Datum';
   locationFieldPlaceholder: string = 'Locatie';
   ageGroupsFieldPlaceholder: string = 'Leeftijdsgroep';
   nameFieldPlaceholder: string = 'Naam';
   costFieldPlaceholder: string = 'Prijs';

   constructor(private eventLocationService: SingerEventLocationService) {
      super();

      this.filterParameters = new EventFilterParameters();

      this.eventLocationService.fetchSingerEventLocationsData('asc', 'name', 0, 1000, '').subscribe(res => {
         this.availableLocations = res.items as SingerEventLocation[];
      });
   }

   ngOnInit() {
      this.resetFilter();
   }

   initializeFilterForm(): void {
      this.formGroup.addControl('startDateFieldControl', new FormControl());
      this.formGroup.addControl('endDateFieldControl', new FormControl());
      this.formGroup.addControl('locationFieldControl', new FormControl());
      this.formGroup.addControl('ageGroupsFieldControl', new FormControl());
      this.formGroup.addControl('costFieldControl', new FormControl());
   }

   loadFilterParameters(): void {
      let filterParameters: EventFilterParameters = {
         startDate: this.formGroup.controls.startDateFieldControl.value,
         endDate: this.formGroup.controls.endDateFieldControl.value,
         locationId: this.formGroup.controls.locationFieldControl.value,
         allowedAgeGroups: this.formGroup.controls.ageGroupsFieldControl.value,
         name: this.formGroup.controls.nameFieldControl.value,
         maxCost:
            this.formGroup.controls.costFieldControl.value == null
               ? null
               : CostFilterParameter[this.formGroup.controls.costFieldControl.value as string],
      };
      this.filterParameters = filterParameters;
   }

   resetFilter() {
      this.formGroup.reset();
      this.filterParameters = new EventFilterParameters();
      this.emitFilterEvent();
   }

   compareAgeGroups(ageGroupX: number, ageGroupY: string) {
      return AgeGroup[ageGroupX] === ageGroupY;
   }

   compareCostFilterParameters(costFilterParameterX: number, costFilterParameterY: string) {
      return CostFilterParameter[costFilterParameterX] === costFilterParameterY;
   }
}
