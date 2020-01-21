import { Component, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { SingerEventLocation, EventFilterParameters } from 'src/app/modules/core/models/singerevent.model';
import { GenericFilter } from 'src/app/modules/core/components/Generics/generic-filter.component';
import { GenericFilterParameters } from 'src/app/modules/core/models/generics/generic-filter-parameters.model';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { AgeGroup, CostFilterParameter } from 'src/app/modules/core/models/enum';

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
   // Available agegroups
   ageGroups = Object.keys(AgeGroup).filter(k => typeof AgeGroup[k as any] === 'number');
   // Available cost filter parameters
   costFilterParameters = Object.keys(CostFilterParameter).filter(k => typeof CostFilterParameter[k as any] === 'number');

   // Form placeholders
   startDateFieldPlaceholder: string = 'Start Datum';
   endDateFieldPlaceholder: string = 'Eind Datum';
   locationFieldPlaceholder: string = 'Locatie';
   ageGroupsFieldPlaceholder: string = 'Leeftijdsgroep';
   nameFieldPlaceholder: string = 'Naam';
   priceFieldPlaceholder: string = 'Prijs';

   constructor(private eventLocationService: SingerEventLocationService) {
      super();

      this.eventLocationService.fetchSingerEventLocationsData('asc', 'name', 0, 1000, '').subscribe(res => {
         this.availableLocations = res.items as SingerEventLocation[];
      });
   }

   initializeFilterForm(): void {
      this.formGroup.addControl('startDateFieldControl', new FormControl());
      this.formGroup.addControl('endDateFieldControl', new FormControl());
      this.formGroup.addControl('locationFieldControl', new FormControl());
      this.formGroup.addControl('ageGroupsFieldControl', new FormControl());
      this.formGroup.addControl('priceFieldControl', new FormControl());
   }

   resetFilter() {
      this.formGroup.reset();
      this.filterParameters = new EventFilterParameters();
   }

   compareAgeGroups(ageGroupX: number, ageGroupY: string) {
      return AgeGroup[ageGroupX] === ageGroupY;
   }

   compareCostFilterParameters(costFilterParameterX: number, costFilterParameterY: string) {
      return CostFilterParameter[costFilterParameterX] === costFilterParameterY;
   }
}
