import { Component, OnInit, Input } from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { MatDialog } from '@angular/material';
import { EventRegistrationComponent } from 'src/app/modules/shared/components/event-registration/event-registration.component';

@Component({
   selector: 'app-event-card',
   templateUrl: './event-card.component.html',
   styleUrls: ['./event-card.component.css'],
})
export class EventCardComponent implements OnInit {
   @Input() event: EventDescription;

   constructor(private _dialog: MatDialog) {}

   ngOnInit() {}

   getDurationMessage(event: EventDescription): string {
      if (event.startDateTime === event.endDateTime) {
         return 'Duurt 1 dag';
      } else {
         return 'Duurt meerdere dagen';
      }
   }

   openEventRegistration(event: EventDescription) {
      this._dialog.open(EventRegistrationComponent, {
         data: event,
         width: '40vw',
      });
   }
}
