import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import {
   MatDialogRef,
   MAT_DIALOG_DATA,
   MatSnackBar,
   MatPaginator,
   MatSort,
} from '@angular/material';
import {
   EventDescription,
   EventRelevantCareUserDTO,
} from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { MatTableDataSource } from '@angular/material/table';
import { EventSlot } from 'src/app/modules/core/models/event-slot';

@Component({
   selector: 'app-event-registration',
   templateUrl: './event-registration.component.html',
   styleUrls: ['./event-registration.component.css'],
})
export class EventRegistrationComponent implements OnInit {
   public careUsers: EventRelevantCareUserDTO[];
   public eventSlotDataSource: MatTableDataSource<EventSlot>;
   public columnsToDisplay: string[] = ['eventSlot'];

   @ViewChild(MatPaginator) paginator: MatPaginator;
   @ViewChild(MatSort) sort: MatSort;

   constructor(
      public dialogRef: MatDialogRef<EventRegistrationComponent>,
      @Inject(MAT_DIALOG_DATA) public event: EventDescription,
      private _eventService: SingerEventsService,
      private _snackBar: MatSnackBar
   ) {
      this.eventSlotDataSource = new MatTableDataSource(event.eventSlots);
   }

   ngOnInit() {
      this._eventService.getRelevantCareUsers(this.event.id).subscribe(res => {
         this.careUsers = res;
         res.forEach(x => {
            if (x.appropriateAgeGroup) {
               this.columnsToDisplay.push(x.id);
            }
         });
      });
      this.eventSlotDataSource.paginator = this.paginator;
      this.eventSlotDataSource.sort = this.sort;
   }

   registerCareUser(
      event: EventDescription,
      careUser: EventRelevantCareUserDTO
   ) {
      this._eventService.registerCareUser(event.id, careUser.id).subscribe(
         res => {
            this._snackBar.open(
               `${careUser.firstName} ${careUser.lastName} werd ingeschreven voor het evenement: ${event.title}`,
               'OK',
               { duration: 2000 }
            );
         },
         err => {
            console.log(err);
            this._snackBar.open(`⚠ ${err}`, 'OK');
         }
      );
   }

   hasInappropriateCareUsers() {
      return this.careUsers.filter(x => !x.appropriateAgeGroup).length > 0;
   }
}
