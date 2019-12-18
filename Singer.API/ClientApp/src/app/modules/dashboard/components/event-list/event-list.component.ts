import { Component, OnInit } from '@angular/core';
import {
   EventDescription,
   SingerEventLocation,
} from 'src/app/modules/core/models/singerevent.model';
import { SearchEventData } from '../event-search/event-search.component';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';
import { LoadingService } from 'src/app/modules/core/services/loading.service';

@Component({
   selector: 'app-event-list',
   templateUrl: './event-list.component.html',
   styleUrls: ['./event-list.component.css'],
})
export class EventListComponent implements OnInit {
   breakpoint: number;

   events: EventDescription[] = [];
   availableLocations: SingerEventLocation[];

   constructor(
      private _eventService: SingerEventsService,
      private _eventLocationService: SingerEventLocationService,
      private _loadingService: LoadingService
   ) {}

   ngOnInit(): void {
      this._eventLocationService
         .fetchSingerEventLocationsData('asc', 'name', 0, 1000, '')
         .subscribe(res => {
            this.availableLocations = res.items as SingerEventLocation[];
         });

      this.breakpoint = window.innerWidth <= 500 ? 1 : 3;

      // Make first searchevent to load all events
      var emptySearchEventData: SearchEventData = {
         startDateTime: null,
         endDateTime: null,
         locationId: '',
      };

      this.onSearchEvent(emptySearchEventData);
   }

   onResize(event) {
      this.breakpoint = this.calculateColumns(event.target.innerWidth);
   }

   calculateColumns(width: number): number {
      switch (true) {
         case width >= 1200:
            return 3;
         case width >= 800:
            return 2;
         case width >= 400:
            return 1;
         default:
            return 1;
      }
   }

   onSearchEvent(searchEventData: SearchEventData) {
      this._loadingService.show();
      this._eventService.getPublicEvents(searchEventData).subscribe(res => {
         this.events = res;
         this._loadingService.hide();
      });
   }
}
