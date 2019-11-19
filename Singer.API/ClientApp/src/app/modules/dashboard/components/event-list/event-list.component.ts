import {
   Component,
   OnInit,
   Input,
   ViewChild,
   Output,
   EventEmitter,
} from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { MatDrawer } from '@angular/material';
import { SearchEventData } from '../event-search/event-search.component';
import { SingerEventLocation } from 'src/app/modules/core/models/singer-event-location';

@Component({
   selector: 'app-event-list',
   templateUrl: './event-list.component.html',
   styleUrls: ['./event-list.component.css'],
})
export class EventListComponent implements OnInit {
   @Input() events: EventDescription[];
   @Input() availableLocations: SingerEventLocation[];

   @Output() searchEvent: EventEmitter<SearchEventData> = new EventEmitter();

   @ViewChild('drawer') drawer: MatDrawer;
   breakpoint: number;

   constructor() {}

   ngOnInit() {
      this.breakpoint = window.innerWidth <= 500 ? 1 : 3;
      const searchEvent = <SearchEventData>{};
      this.searchEvent.emit(searchEvent);
   }

   onResize(event) {
      this.breakpoint = this.calculateColumns(event.target.innerWidth);
   }

   calculateColumns(width: number):number {
      switch(true) {
         case (width >= 1200):
            return 3;
         case (width >= 800):
            return 2;
         case (width >= 400):
            return 1;
         default:
            return 1;
      }
   }

   onSearchEvent(searchEvent: SearchEventData) {
      this.searchEvent.emit(searchEvent);
   }
}
