import { Component, ViewChild, ChangeDetectorRef } from '@angular/core';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { PendingActionsDataSource } from './pending-actions-datasource';
import { EventRegistrationLog } from 'src/app/modules/core/models/event-registration-log.model';
import { EventRegistrationLogDTO } from 'src/app/modules/core/DTOs/event-registration-log.dto';
import { ActionNotificationsService } from 'src/app/modules/core/services/action-notification.service';

@Component({
  selector: 'app-pending-actions',
  templateUrl: './pending-actions.component.html',
  styleUrls: ['./pending-actions.component.css']
})
export class PendingActionsComponent extends GenericOverviewComponent<
EventRegistrationLog,
EventRegistrationLogDTO,
PendingActionsDataSource
> {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   public dialog: MatDialog;

   constructor(
      dataService: ActionNotificationsService,
      cd: ChangeDetectorRef,
      dialog: MatDialog
   ){

      const ds = new PendingActionsDataSource(dataService);
      super(cd, ds, 'id');
      this.dialog = dialog;
   }

   myOnInit() {}
}



