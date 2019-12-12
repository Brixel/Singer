import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ConfirmRequest } from './confirmrequest.model';
import { ConfirmResponse } from './confirmresponse.model';

@Component({
   selector: 'app-confirm',
   templateUrl: './confirm.component.html',
   styleUrls: ['./confirm.component.css'],
})
export class ConfirmComponent implements OnInit {
   private confirmResponse = <ConfirmResponse>{ isConfirmed: false };

   constructor(
      private dialogRef: MatDialogRef<ConfirmComponent>,
      @Inject(MAT_DIALOG_DATA) public data: ConfirmRequest
   ) {}

   ngOnInit() {}

   confirm() {
      this.confirmResponse.isConfirmed = true;
      this.dialogRef.close(this.confirmResponse);
   }

   cancel() {
      this.dialogRef.close(this.confirmResponse);
   }
}
