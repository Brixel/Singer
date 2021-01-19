import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { EventRegisterDetails } from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { BehaviorSubject } from 'rxjs';
import { Registrant } from 'src/app/modules/core/models/registrant.model';

@Component({
   selector: 'app-event-registration',
   templateUrl: './event-registration.component.html',
   styleUrls: ['./event-registration.component.css'],
})
export class RegistrationComponent implements OnInit {
   public event$ = new BehaviorSubject<EventRegisterDetails>(new EventRegisterDetails());
   public hasInappropriateCareUsers = false;

   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;
   careUsers: Registrant[] = [];

   constructor(
      public dialogRef: MatDialogRef<RegistrationComponent>,
      @Inject(MAT_DIALOG_DATA) private _eventId: string,
      private _eventService: SingerEventsService,
      private _snackBar: MatSnackBar
   ) {}

   ngOnInit() {
      this._eventService.getEventRegisterDetails(this._eventId).subscribe(
         res => {
            this.event$.next(res);
            this.hasInappropriateCareUsers = this.setInappropriateCareUsers(res);
            const registrants = [];
            res.relevantCareUsers
               .filter(x => x.appropriateAgeGroup)
               .forEach(x => {
                  registrants.push(new Registrant(null, x.id, x.firstName, x.lastName, 0, null));
               });
            this.careUsers = registrants;
         },
         err => {
            console.error(err);
            this._snackBar.open(`⚠ ${err}`, 'OK');
         }
      );
   }

   setInappropriateCareUsers(event: EventRegisterDetails): boolean {
      return event.relevantCareUsers.filter(x => !x.appropriateAgeGroup).length > 0;
   }
}
