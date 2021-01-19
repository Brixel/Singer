import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PendingActionsDataSource } from './pending-actions-datasource';
import { ActionNotificationsService } from 'src/app/modules/core/services/action-notification.service';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

@Component({
   selector: 'app-pending-actions',
   templateUrl: './pending-actions.component.html',
   styleUrls: ['./pending-actions.component.css'],
})
export class PendingActionsComponent implements OnInit {
   public dialog: MatDialog;
   public dataSource: PendingActionsDataSource;
   RegistrationStatus = RegistrationStatus;

   constructor(private actionNotificationService: ActionNotificationsService, dialog: MatDialog) {
      this.dataSource = new PendingActionsDataSource(actionNotificationService);
      this.dialog = dialog;
   }
   ngOnInit() {
      this.dataSource.load();
   }

   sendEmails() {
      this.actionNotificationService.sendEmails().subscribe(() => this.dataSource.load());
   }
}
