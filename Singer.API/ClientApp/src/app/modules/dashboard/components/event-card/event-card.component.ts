import { Component, OnInit, Input } from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';

@Component({
  selector: 'app-event-card',
  templateUrl: './event-card.component.html',
  styleUrls: ['./event-card.component.css']
})
export class EventCardComponent implements OnInit {

   @Input() event: EventDescription;

  constructor() { }

  ngOnInit() {
  }

  getDurationMessage(event: EventDescription): string {
     if (event.startDate === event.endDate) {
        return 'Duurt 1 dag';
     } else {
        return 'Duurt meerdere dagen';
     }
  }

}
