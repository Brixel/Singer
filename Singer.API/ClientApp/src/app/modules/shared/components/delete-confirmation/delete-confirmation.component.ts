import { Component, Input, Output, EventEmitter, Inject } from '@angular/core';
import { UntypedFormControl, Validators } from '@angular/forms';

@Component({
   selector: 'app-delete-confirmation',
   templateUrl: './delete-confirmation.component.html',
   styleUrls: ['./delete-confirmation.component.css'],
})
export class DeleteConfirmationComponent {
   @Input() deleteButtonText: string;
   @Input() name: string;
   @Output() delete: EventEmitter<boolean> = new EventEmitter();

   isDeleting = false;
   deleteConfirmationOK = true;

   // Form validation values
   readonly maxNameLength = 100;
   readonly minNameLength = 2;

   confirmFieldControl: UntypedFormControl = new UntypedFormControl('', [
      Validators.minLength(this.minNameLength),
      Validators.maxLength(this.maxNameLength),
   ]);

   constructor() {
      this.makeConfirmFieldNotRequired();
   }

   makeConfirmFieldRequired() {
      // Make confirmTitleFieldControl required
      this.confirmFieldControl = new UntypedFormControl('', [
         Validators.required,
         Validators.minLength(this.minNameLength),
         Validators.maxLength(this.maxNameLength),
      ]);
   }

   makeConfirmFieldNotRequired() {
      // Make confirmTitleFieldControl not required
      this.confirmFieldControl = new UntypedFormControl('', [
         Validators.minLength(this.minNameLength),
         Validators.maxLength(this.maxNameLength),
      ]);
   }

   enableDeleteEvent() {
      this.isDeleting = true;
      this.makeConfirmFieldRequired();
   }

   disableDeleteEvent() {
      this.isDeleting = false;
      this.makeConfirmFieldNotRequired();
   }

   isConfirmFieldMatching(): boolean {
      const confirmFieldString: string = this.confirmFieldControl.value;
      return this.name.toLowerCase() === confirmFieldString.toLowerCase();
   }

   submitDeleteEvent() {
      if (!this.isDeleting) {
         this.enableDeleteEvent();
         return;
      }

      if (!this.isConfirmFieldMatching()) {
         this.deleteConfirmationOK = false;
         this.confirmFieldControl.setErrors({ invalid: true });
         return;
      }

      if (!this.confirmFieldControl.valid) {
         return;
      }

      this.delete.emit(true);
   }
}
