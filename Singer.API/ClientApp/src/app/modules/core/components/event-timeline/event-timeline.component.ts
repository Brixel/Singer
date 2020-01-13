import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-event-timeline',
  templateUrl: './event-timeline.component.html',
  styleUrls: ['./event-timeline.component.css']
})
export class EventTimelineComponent {

   @Input() displayEventOnly: boolean;
   @Input() animated: boolean;
   @Input() startRegistrationDate: Date;
   @Input() endRegistrationDate: Date;
   @Input() finalCancelationDate: Date;
   @Input() startEventDate: Date;
   @Input() endEventDate: Date;

}
