import { Component, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { RegistrationDTO } from 'src/app/modules/core/DTOs/registration.dto';
import { PendingRegistrationsDatasource } from './pending-registrations-datasource';
import { Registration } from 'src/app/modules/core/models/registration.model';
import { PendingRegistrationsService } from 'src/app/modules/core/services/singerevents-api/pending-registrations-service';
import {
   SingerRegistrationsComponent,
   SingerRegistrationData,
} from '../singerevents/event-registrations/event-registrations.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { RegistrationSearchDTO } from 'src/app/modules/core/DTOs/registration.dto';

@Component({
   selector: 'app-pending-registrations',
   templateUrl: './pending-registrations.component.html',
   styleUrls: ['./pending-registrations.component.css'],
})
export class PendingRegistrationsComponent extends GenericOverviewComponent<
   Registration,
   RegistrationDTO,
   null,
   null,
   PendingRegistrationsService,
   PendingRegistrationsDatasource,
   RegistrationSearchDTO
> {
   @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort: MatSort;
   public dialog: MatDialog;
   private _eventService: SingerEventsService;

   constructor(
      dataService: PendingRegistrationsService,
      cd: ChangeDetectorRef,
      dialog: MatDialog,
      eventService: SingerEventsService
   ) {
      const ds = new PendingRegistrationsDatasource(dataService);
      super(cd, ds, 'id');
      this.displayedColumns.push('eventDescription.title', 'fromTo', 'careUser', 'actions');
      this.dialog = dialog;
      this._eventService = eventService;
   }

   manageRegistrations(row: Registration) {
      this._eventService.getSingleEvent(row.eventDescription.id).subscribe(res => {
         this.dialog
            .open(SingerRegistrationsComponent, {
               data: <SingerRegistrationData>{
                  event: this._eventService.toModel(res),
                  defaultEventSlot: row.eventSlot,
               },
               width: '60vw',
               maxHeight: '70vh',
            })
            .afterClosed()
            .subscribe(_ => {
               this.dataSource.load(this.searchDTO);
            });
      });
   }
}
