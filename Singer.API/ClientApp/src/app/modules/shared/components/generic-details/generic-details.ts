import { Output, ChangeDetectorRef, AfterViewInit, Directive } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { GenericModel } from 'src/app/modules/core/models/generics/generic-model';
import { FormElement } from '../../interfaces/form-element';

export interface GenericDetailsFormData<TModel extends GenericModel> {
   modelInstance: TModel;
   isAdding: boolean;
   showFormButtons: boolean;
}

@Directive()
export abstract class GenericDetails<TModel extends GenericModel> implements AfterViewInit {
   @Output() submitEvent: EventEmitter<TModel> = new EventEmitter();

   isAdding: boolean;
   changesMade: boolean;
   currentInstance: TModel;
   formFields: FormElement[] = [];

   public formControlGroup: UntypedFormGroup;

   constructor(
      public dialogRef: MatDialogRef<GenericDetails<TModel>>,
      public data: GenericDetailsFormData<TModel>,
      private cd: ChangeDetectorRef
   ) {}

   ngOnInit(): void {
      this.isAdding = this.data.isAdding;
      if (this.data.isAdding && this.data.modelInstance === null) {
         this.currentInstance = {} as TModel;
      } else {
         this.currentInstance = this.data.modelInstance;
      }
      let formControls = {};
      this.formFields.forEach(f => {
         formControls[`${f.name}FieldControl`] = new UntypedFormControl('', f.validators);
      });
      this.formControlGroup = new UntypedFormGroup(formControls);
      if (this.isAdding) {
         this.resetFormControls();
         this.createEmptyInstance();
      } else {
         this.loadCurrentInstanceValues();
      }
   }

   ngAfterViewInit(): void {
      this.cd.detectChanges();
   }

   private loadCurrentInstanceValues() {
      this.formFields.forEach(f => {
         this.formControlGroup.controls[`${f.name}FieldControl`].reset(this.currentInstance[f.name]);
      });
   }

   private resetFormControls() {
      this.formFields.forEach(f => {
         this.formControlGroup.controls[`${f.name}FieldControl`].reset();
      });
   }

   createEmptyInstance() {
      Object.keys(this.currentInstance).forEach(k => {
         this.currentInstance[k] = '';
      });
   }

   checkForChanges(): boolean {
      if (this.isAdding) return true;
      let result = false;
      this.formFields.forEach(f => {
         if (this.currentInstance[f.name] !== this.formControlGroup.controls[`${f.name}FieldControl`].value) {
            result = true;
         }
      });
      return result;
   }

   public updateCurrentInstance() {
      this.formFields.forEach(f => {
         this.currentInstance[f.name] = this.formControlGroup.controls[`${f.name}FieldControl`].value;
      });
   }

   getPlaceholder(fieldName: string): string {
      return this.formFields.find(x => x.name === fieldName).placeholder;
   }

   getSaveLabel(): string {
      return this.isAdding ? 'Toevoegen' : 'Opslaan';
   }

   // Submit the form
   submitForm() {
      // Check if form is valid
      if (this.formControlGroup.invalid) {
         return;
      }

      // Check for changes and determine of an API call is necesarry
      if (this.checkForChanges()) {
         this.updateCurrentInstance();
         this.submitEvent.emit(this.currentInstance);
      }
      this.closeForm();
   }

   // Close the form
   closeForm() {
      this.dialogRef.close();
   }
}
