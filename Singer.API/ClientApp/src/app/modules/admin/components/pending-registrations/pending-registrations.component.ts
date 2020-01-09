import { Component, ViewChild, ChangeDetectorRef, OnInit } from '@angular/core';
import { MatPaginator, MatSort, MatDialog } from '@angular/material';
import { GenericOverviewComponent } from 'src/app/modules/shared/components/generic-overview/generic-overview.component';
import { EventRegistrationDTO } from 'src/app/modules/core/DTOs/event-registration.dto';
import { PendingRegistrationsDatasource } from './pending-registrations-datasource';
import { EventRegistration } from 'src/app/modules/core/models/singerevent.model';
import { PendingRegistrationsService } from 'src/app/modules/core/services/singerevents-api/pending-registrations-service';
import {
   SingerEventRegistrationsComponent,
   SingerEventRegistrationData,
} from '../singerevents/singer-event-registrations/singer-event-registrations.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';

@Component({
   selector: 'app-pending-registrations',
   templateUrl: './pending-registrations.component.html',
   styleUrls: ['./pending-registrations.component.css'],
})
export class PendingRegistrationsComponent extends GenericOverviewComponent<
   EventRegistration,
   EventRegistrationDTO,
   PendingRegistrationsDatasource
> {
   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   public dialog: MatDialog;
   private _eventService: SingerEventsService;

   myOnInit() {}
   constructor(
      dataService: PendingRegistrationsService,
      cd: ChangeDetectorRef,
      dialog: MatDialog,
      eventService: SingerEventsService
   ) {
      const ds = new PendingRegistrationsDatasource(dataService);
      super(cd, ds, 'id');
      this.displayedColumns.push(
         'eventDescription.title',
         'fromTo',
         'careUser'
      );
      this.dialog = dialog;
      this._eventService = eventService;
   }

   manageRegistrations(row: EventRegistration) {
      this._eventService
         .getSingleEvent(row.eventDescription.id)
         .subscribe(res => {
            this.dialog
               .open(SingerEventRegistrationsComponent, {
                  data: <SingerEventRegistrationData>{
                     event: this._eventService.toModel(res),
                     defaultEventSlot: row.eventSlot,
                  },
                  width: '60vw',
                  maxHeight: '70vh',
               })
               .afterClosed()
               .subscribe(_ => {
                  this.dataSource.load();
               });
         });
   }
}
