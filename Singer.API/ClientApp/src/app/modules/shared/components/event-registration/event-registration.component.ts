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
import { Registrant } from 'src/app/modules/core/models/registrant.model';

@Component({
   selector: 'app-event-registration',
   templateUrl: './event-registration.component.html',
   styleUrls: ['./event-registration.component.css'],
})
export class EventRegistrationComponent implements OnInit {
   private _event$ = new BehaviorSubject<EventRegisterDetails>(null);
   public event$ = this._event$.asObservable();
   public hasInappropriateCareUsers = false;

   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   careUsers: Registrant[] = [];

   constructor(
      public dialogRef: MatDialogRef<EventRegistrationComponent>,
      @Inject(MAT_DIALOG_DATA) private _eventId: string,
      private _eventService: SingerEventsService
   ) {
   }

   ngOnInit() {
      this._eventService
         .getEventRegisterDetails(this._eventId)
         .subscribe(res => {
            this._event$.next(res);
            this.hasInappropriateCareUsers = this.setInappropriateCareUsers(
               res
            );
            const registrants = [];
            res.relevantCareUsers
               .filter(x => x.appropriateAgeGroup)
               .forEach(x => {
                  registrants.push(new Registrant(x, 0));
               });
            this.careUsers = registrants;
            console.log(this.careUsers);
         });
   }

   setInappropriateCareUsers(event: EventRegisterDetails): boolean {
      return (
         event.relevantCareUsers.filter(x => !x.appropriateAgeGroup).length > 0
      );
   }
}
