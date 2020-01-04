import { Component, Input, Output } from '@angular/core';
import { EventEmitter } from 'protractor';
import { FormControl, Validators } from '@angular/forms';

@Component({
   selector: 'app-delete-confirmation',
   templateUrl: './delete-confirmation.component.html',
   styleUrls: ['./delete-confirmation.component.css'],
})
export class DeleteConfirmationComponent {
   @Input() name: string;
   @Output() delete: EventEmitter = new EventEmitter();

   isDeleting: boolean = false;

   // Form validation values
   readonly maxNameLength = 100;
   readonly minNameLength = 2;

   confirmFieldControl: FormControl = new FormControl('', [
      Validators.minLength(this.minNameLength),
      Validators.maxLength(this.maxNameLength),
   ]);

   constructor() {}

   makeConfirmFieldRequired() {
      // Make confirmTitleFieldControl required
      this.confirmFieldControl = new FormControl(
         this.confirmFieldControl.value,
         [
            Validators.required,
            Validators.minLength(this.minNameLength),
            Validators.maxLength(this.maxNameLength),
         ]
      );
   }

   makeConfirmFieldNotRequired() {
      // Make confirmTitleFieldControl not required
      this.confirmFieldControl = new FormControl(
         this.confirmFieldControl.value,
         [
            Validators.minLength(this.minNameLength),
            Validators.maxLength(this.maxNameLength),
         ]
      );
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
      let confirmFieldString: string = this.confirmFieldControl.value;
      return this.name.toLowerCase() === confirmFieldString.toLowerCase();
   }

   submitDeleteEvent() {
      if (this.isDeleting) {
         if (this.isConfirmFieldMatching()) {
            this.delete.emit('');
         }
      } else {
         this.enableDeleteEvent();
      }
   }
}
