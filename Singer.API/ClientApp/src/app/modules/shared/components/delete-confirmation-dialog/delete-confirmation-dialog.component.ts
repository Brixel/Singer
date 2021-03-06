import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';

export interface ConfirmationData {
   name: string;
   deleteButtonText: string;
}

@Component({
   selector: 'app-delete-confirmation-dialog',
   templateUrl: './delete-confirmation-dialog.component.html',
   styleUrls: ['./delete-confirmation-dialog.component.css'],
})
export class DeleteConfirmationDialogComponent implements OnInit {
   constructor(
      public dialogRef: MatDialogRef<DeleteConfirmationComponent>,
      @Inject(MAT_DIALOG_DATA) public data: ConfirmationData
   ) {}

   ngOnInit() {}

   confirm() {
      this.dialogRef.close(true);
   }
}
