import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
   selector: 'app-confirm',
   templateUrl: './confirm.component.html',
   styleUrls: ['./confirm.component.css'],
})
export class ConfirmComponent implements OnInit {
   private isConfirmed = false;

   constructor(
      private dialogRef: MatDialogRef<ConfirmComponent>,
      @Inject(MAT_DIALOG_DATA) public data: ConfirmRequest
   ) {}

   ngOnInit() {}

   confirm() {
      this.isConfirmed = true;
      this.dialogRef.close(this.isConfirmed);
   }

   cancel() {
      this.dialogRef.close(this.isConfirmed);
   }
}

export interface ConfirmRequest {
   confirmMessage: string;
}
