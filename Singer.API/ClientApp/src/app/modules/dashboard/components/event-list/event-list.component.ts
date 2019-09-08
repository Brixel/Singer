import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { EventDescription, SingerEventLocation } from 'src/app/modules/core/models/singerevent.model';
import { MatDrawer } from '@angular/material';
import { SearchEventData } from '../event-search/event-search.component';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit {

   @Input() events: EventDescription[];
   @Input() availableLocations: SingerEventLocation[];

   @Output() searchEvent: EventEmitter<SearchEventData> = new EventEmitter();

   @ViewChild('drawer') drawer: MatDrawer;
   breakpoint: number;

  constructor() { }

   ngOnInit() {
      this.breakpoint = (window.innerWidth <= 500) ? 1 : 3;
      const searchEvent = <SearchEventData>{}
      this.searchEvent.emit(searchEvent);
   }

   onResize(event) {
      this.breakpoint = (event.target.innerWidth <= 500) ? 1 : 3;
   }


   onSearchEvent(searchEvent: SearchEventData){
      this.searchEvent.emit(searchEvent);
   }
}
