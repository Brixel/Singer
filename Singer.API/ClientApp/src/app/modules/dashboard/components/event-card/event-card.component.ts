import { Component, Input } from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { MatDialog } from '@angular/material';
import { EventRegistrationComponent } from 'src/app/modules/shared/components/event-registration/event-registration.component';

@Component({
   selector: 'app-event-card',
   templateUrl: './event-card.component.html',
   styleUrls: ['./event-card.component.css'],
})
export class EventCardComponent {
   @Input() event: EventDescription;

   constructor(private _dialog: MatDialog) {}

   getDurationMessage(event: EventDescription): string {
      let isSameDay =
         event.startDateTime.getDate() === event.endDateTime.getDate() &&
         event.startDateTime.getMonth() === event.endDateTime.getMonth() &&
         event.startDateTime.getFullYear() === event.endDateTime.getFullYear();
      if (isSameDay) {
         return 'Duurt 1 dag';
      } else {
         return 'Duurt meerdere dagen';
      }
   }

   openEventRegistration(event: EventDescription) {
      this._dialog.open(EventRegistrationComponent, {
         data: event.id,
         maxHeight: '100vh',
         width: '40vw',
      });
   }
}
