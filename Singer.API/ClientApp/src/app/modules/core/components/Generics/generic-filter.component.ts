import { GenericFilterParameters } from '../../models/generics/generic-filter-parameters.model';
import { EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

export abstract class GenericFilter {
   filterParameters: GenericFilterParameters;
   genericFilterEvent: EventEmitter<GenericFilterParameters> = new EventEmitter();

   // Form validation values
   readonly maxNameLength = 100;

   formGroup: FormGroup = new FormGroup({
      nameFieldControl: new FormControl([Validators.maxLength(this.maxNameLength)]),
   });

   constructor() {
      this.initializeFilterForm();
      this.resetFilter();
   }

   emitFilterEvent(): void {
      this.genericFilterEvent.emit(this.filterParameters);
   }
   abstract initializeFilterForm(): void;
   abstract resetFilter(): void;
}
