import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import {
   MatDialogRef,
   MAT_DIALOG_DATA,
   MatSnackBar,
   MatPaginator,
   MatSort,
} from '@angular/material';
import {
   EventRelevantCareUserDTO,
   EventRegisterDetails,
   EventSlotRegistrations,
   EventCareUserRegistration,
} from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject } from 'rxjs';
import { RegistrationStatus } from 'src/app/modules/core/models/enum';

@Component({
   selector: 'app-event-registration',
   templateUrl: './event-registration.component.html',
   styleUrls: ['./event-registration.component.css'],
})
export class EventRegistrationComponent implements OnInit {
   private _event$ = new BehaviorSubject<EventRegisterDetails>(null);
   public event$ = this._event$.asObservable();
   public eventSlotDataSource: MatTableDataSource<EventSlotRegistrations>;
   public columnsToDisplay: string[] = ['eventSlot'];
   public hasInappropriateCareUsers = false;

   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;

   constructor(
      public dialogRef: MatDialogRef<EventRegistrationComponent>,
      @Inject(MAT_DIALOG_DATA) private _eventId: string,
      private _eventService: SingerEventsService,
      private _snackBar: MatSnackBar
   ) {
      this.eventSlotDataSource = new MatTableDataSource([]);
   }

   ngOnInit() {
      this.eventSlotDataSource.paginator = this.paginator;
      this.eventSlotDataSource.sort = this.sort;
      this._eventService
         .getEventRegisterDetails(this._eventId)
         .subscribe(res => {
            this._event$.next(res);
            this.hasInappropriateCareUsers = this.setInappropriateCareUsers(
               res
            );
            res.relevantCareUsers.forEach(x => {
               if (x.appropriateAgeGroup) {
                  this.columnsToDisplay.push(x.id);
               }
            });
            this.eventSlotDataSource = new MatTableDataSource(res.eventSlots);
            this.eventSlotDataSource.paginator = this.paginator;
            this.eventSlotDataSource.sort = this.sort;
         });
   }

   registerCareUserOnEvent(careUser: EventRelevantCareUserDTO) {
      this._eventService
         .registerCareUserOnEvent(this._eventId, careUser.id)
         .subscribe(
            res => {
               this._snackBar.open(
                  `${careUser.firstName} ${careUser.lastName} werd ingeschreven voor het evenement: ${this._event$.value.title}`,
                  'OK',
                  { duration: 2000 }
               );
               let tmp = this._event$.value;
               res.forEach(r => {
                  tmp.eventSlots.forEach(e =>
                     e.registrations.push(<EventCareUserRegistration>{
                        careUserId: r.careUser.id,
                        status: r.status,
                     })
                  );
               });
            },
            err => {
               console.log(err);
               this._snackBar.open(`⚠ ${err}`, 'OK');
            }
         );
   }

   registerCareUserOnEventSlot(
      eventSlotId: string,
      careUser: EventRelevantCareUserDTO
   ) {
      this._eventService
         .registerCareUserOnEventSlot(
            this._event$.value.id,
            eventSlotId,
            careUser.id
         )
         .subscribe(
            res => {
               this._snackBar.open(
                  `${careUser.firstName} ${careUser.lastName} werd ingeschreven voor het evenement: ${this._event$.value.title}`,
                  'OK',
                  { duration: 2000 }
               );
               let ev = this._event$.value;
               ev.eventSlots
                  .find(x => x.id == eventSlotId)
                  .registrations.push(<EventCareUserRegistration>{
                     careUserId: careUser.id,
                     status: res.status,
                  });
               this._event$.next(ev);
            },
            err => {
               console.log(err);
               this._snackBar.open(`⚠ ${err}`, 'OK');
            }
         );
   }

   setInappropriateCareUsers(event: EventRegisterDetails): boolean {
      return (
         event.relevantCareUsers.filter(x => !x.appropriateAgeGroup).length > 0
      );
   }

   getSlotRegistrationStatus(
      registrations: EventCareUserRegistration[],
      careUserId: string
   ): RegistrationStatus {
      const registration = registrations.find(x => x.careUserId == careUserId);
      if (registration === undefined) {
         return 0;
      } else {
         return registration.status;
      }
   }

   getEventRegistrationStatus(careUserId: string): RegistrationStatus {
      const slots = this._event$.value.eventSlots.filter(x =>
         x.registrations.filter(y => y.careUserId == careUserId)
      );
      if (slots.length != this._event$.value.eventSlots.length) {
         return 0;
      }
      const regs = slots.map(s => s.registrations[0]);

      if (regs.length == 0) {
         return 0;
      }
      const states = regs.map(r => (r === undefined ? 0 : r.status));
      const uniqueStates = Array.from(new Set(states));
      if (uniqueStates.length != 1) {
         return 0;
      }

      return +uniqueStates[0];
   }
}
