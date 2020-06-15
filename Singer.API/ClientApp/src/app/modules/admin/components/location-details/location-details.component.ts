import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import {
   GenericDetails,
   GenericDetailsFormData,
} from 'src/app/modules/shared/components/generic-details/generic-details';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Validators } from '@angular/forms';
import { FormElement } from 'src/app/modules/shared/interfaces/form-element';

@Component({
   selector: 'app-location-details',
   templateUrl: './location-details.component.html',
   styleUrls: ['./location-details.component.css'],
})
export class LocationDetailsComponent extends GenericDetails<SingerLocation> {
   constructor(
      dialogRef: MatDialogRef<LocationDetailsComponent>,
      @Inject(MAT_DIALOG_DATA) public data: GenericDetailsFormData<SingerLocation>,
      cd: ChangeDetectorRef
   ) {
      super(dialogRef, data, cd);
      this.formFields.push(
         <FormElement>{
            name: 'name',
            placeholder: 'Naam',
            validators: [Validators.required],
         },
         <FormElement>{
            name: 'address',
            placeholder: 'Adres',
            validators: [Validators.required],
         },
         <FormElement>{
            name: 'city',
            placeholder: 'Gemeente',
            validators: [Validators.required],
         },
         <FormElement>{
            name: 'postalCode',
            placeholder: 'Postcode',
            validators: [Validators.required],
         },
         <FormElement>{
            name: 'country',
            placeholder: 'Land',
            validators: [Validators.required],
         }
      );
   }

   createEmptyInstance() {
      this.currentInstance = <SingerLocation>{};
   }
}
