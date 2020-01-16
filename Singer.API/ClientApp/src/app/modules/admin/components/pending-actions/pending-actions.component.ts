import { Component, ViewChild, ChangeDetectorRef, OnInit } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { PendingActionsDataSource } from './pending-actions-datasource';
import { EventRegistrationLogCareUser } from 'src/app/modules/core/models/event-registration-log.model';
import { EventRegistrationLogCareUserDTO } from 'src/app/modules/core/DTOs/event-registration-log.dto';
import { ActionNotificationsService } from 'src/app/modules/core/services/action-notification.service';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

@Component({
  selector: 'app-pending-actions',
  templateUrl: './pending-actions.component.html',
  styleUrls: ['./pending-actions.component.css']
})
export class PendingActionsComponent implements OnInit {
   public dialog: MatDialog;
   public dataSource: PendingActionsDataSource;
   RegistrationStatus = RegistrationStatus;

   constructor(
      dataService: ActionNotificationsService,
      dialog: MatDialog
   ){

      this.dataSource = new PendingActionsDataSource(dataService);
      this.dialog = dialog;
   }
   ngOnInit(){
      this.dataSource.load();
   }
}



