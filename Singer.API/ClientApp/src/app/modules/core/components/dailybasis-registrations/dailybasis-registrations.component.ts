import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { EventSlotRegistrations, EventCareUserRegistration } from '../../models/singerevent.model';
import { Registrant } from '../../models/registrant.model';
import { SingerEventsService } from '../../services/singerevents-api/singerevents.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { RegistrationStatus } from '../../models/enum';

@Component({
   selector: 'app-dailybasis-registrations',
   templateUrl: './dailybasis-registrations.component.html',
   styleUrls: ['./dailybasis-registrations.component.css'],
})
export class DailybasisRegistrationsComponent implements OnInit {
   @Input() eventId: string;
   _eventSlots: EventSlotRegistrations[];
   @Input() set eventSlots(value: EventSlotRegistrations[]) {
      this._eventSlots = value;
      this.eventSlotDataSource = new MatTableDataSource(value);
      this.eventSlotDataSource.paginator = this.paginator;
      this.eventSlotDataSource.sort = this.sort;
   }
   RegistrationStatus = RegistrationStatus;

   get eventSlots() {
      return this._eventSlots;
   }

   private _careUsers: Registrant[] = [];
   @Input() set careUsers(value: Registrant[]) {
      this.columnsToDisplay = ['eventSlot'];
      this._careUsers = [];
      this._careUsers = value;
      const careUserIds = this._careUsers.map(c => c.careUserId);
      this.columnsToDisplay.push(...careUserIds);
   }
   get careUsers() {
      return this._careUsers;
   }

   public eventSlotDataSource = new MatTableDataSource([]);
   public columnsToDisplay: string[] = ['eventSlot'];

   @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort: MatSort;

   constructor(private _eventService: SingerEventsService, private _snackBar: MatSnackBar) {
      this.eventSlotDataSource = new MatTableDataSource([]);
   }

   ngOnInit() {}

   getSlotRegistrationStatus(registrations: EventCareUserRegistration[], careUserId: string): RegistrationStatus {
      const registration = registrations.find(x => x.careUserId === careUserId);
      if (registration === undefined) {
         return 0;
      } else {
         return registration.status;
      }
   }

   registerCareUserOnEventSlot(eventSlotId: string, careUser: Registrant) {
      this._eventService.registerCareUserOnEventSlot(this.eventId, eventSlotId, careUser.careUserId).subscribe(
         res => {
            this._snackBar.open(`${careUser.name} werd ingeschreven voor het evenement`, 'OK', { duration: 2000 });
            this.eventSlots
               .find(x => x.id === eventSlotId)
               .registrations.push(<EventCareUserRegistration>{
                  careUserId: careUser.careUserId,
                  status: res.status,
               });
         },
         err => {
            this._snackBar.open(`⚠ ${err.message}`, 'OK');
         }
      );
   }
}
