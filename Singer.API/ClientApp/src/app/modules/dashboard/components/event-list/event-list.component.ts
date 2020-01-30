import { Component, OnInit } from '@angular/core';
import {
   EventDescription,
   EventFilterParameters,
} from 'src/app/modules/core/models/singerevent.model';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { LoadingService } from 'src/app/modules/core/services/loading.service';

@Component({
   selector: 'app-event-list',
   templateUrl: './event-list.component.html',
   styleUrls: ['./event-list.component.css'],
})
export class EventListComponent implements OnInit {
   events: EventDescription[] = [];

   // Number of colums for the event cards
   columns: number;

   constructor(private _eventService: SingerEventsService, private _loadingService: LoadingService) {}

   ngOnInit(): void {
      this.columns = this.calculateColumns(window.innerWidth);
   }

   onResize(event) {
      this.columns = this.calculateColumns(event.target.innerWidth);
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

   onFilterEvent(filterParameters: EventFilterParameters) {
      this._loadingService.show();
      this._eventService.getPublicEvents(filterParameters).subscribe(res => {
         this.events = res.sort((a, b)=>{
            return a.startDateTime.getTime() - b.startDateTime.getTime();
         });
         this._loadingService.hide();
      });
   }
}
