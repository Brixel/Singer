import { Component, OnInit } from '@angular/core';
import { EventDescription } from 'src/app/modules/core/models/singerevent.model';
import { SearchEventData } from '../event-search/event-search.component';
import { SingerEventLocation } from 'src/app/modules/core/models/singer-event-location.dto';
import { AuthService } from 'src/app/modules/core/services/auth.service';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { SingerEventLocationService } from 'src/app/modules/core/services/singerevents-api/singerevent-location.service';

@Component({
   selector: 'app-home',
   templateUrl: './home.component.html',
   styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
   isAuthenticated = false;
   events: EventDescription[] = [];
   availableLocations: SingerEventLocation[];
   constructor(
      private _authService: AuthService,
      private _eventService: SingerEventsService,
      private _eventLocationService: SingerEventLocationService
   ) {}

   ngOnInit(): void {
      this._eventLocationService
         .fetchSingerEventLocationsData('asc', 'name', 0, 1000, '')
         .subscribe(res => {
            this.availableLocations = res.items as SingerEventLocation[];
         });
      this._authService.isAuthenticated$.subscribe(res => {
         this.isAuthenticated = res;
      });

      this._authService.isAuthenticated();
   }

   onSearchEvent(searchEventData: SearchEventData) {
      this._eventService.getPublicEvents(searchEventData).subscribe(res => {
         this.events = res;
      });
   }
}
