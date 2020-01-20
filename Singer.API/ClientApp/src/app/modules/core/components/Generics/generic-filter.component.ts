import { GenericFilterParameters } from '../../models/generics/generic-filter-parameters.model';
import { EventEmitter } from '@angular/core';

export abstract class GenericFilter {
   filterParameters: GenericFilterParameters;
   genericFilterEvent: EventEmitter<GenericFilterParameters> = new EventEmitter();

   emitFilterEvent(): void {
      this.genericFilterEvent.emit(this.filterParameters);
   }

   abstract resetFilter(): void;
}
