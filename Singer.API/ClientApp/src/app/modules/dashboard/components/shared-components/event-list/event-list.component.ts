import {
   Component,
   OnInit,
} from '@angular/core';
import { EventDescription, SearchEventDTO } from 'src/app/modules/core/models/singerevent.model';
import { SearchEventData } from '../event-search/event-search.component';
import { SingerEventLocation } from 'src/app/modules/core/models/singer-event-location';
import { ApiService } from 'src/app/modules/core/services/api.service';
import { Observable } from 'rxjs';
import { PaginationDTO } from 'src/app/modules/core/models/pagination.model';
import { HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
   selector: 'app-event-list',
   templateUrl: './event-list.component.html',
   styleUrls: ['./event-list.component.css'],
})
export class EventListComponent implements OnInit {

   breakpoint: number;

   events: EventDescription[] = [];
   availableLocations: SingerEventLocation[];

   constructor(private apiService: ApiService) {}

   ngOnInit() {
      this.breakpoint = window.innerWidth <= 500 ? 1 : 3;

      this.getSingerEventLocations('asc', 'name', 0, 1000, '').subscribe(
         res => {
            this.availableLocations = res.items as SingerEventLocation[];
         }
      );

      // Make first searchevent to load all events
      var emptySearchEventData:SearchEventData = {
         startDateTime: null,
         endDateTime: null,
         locationId: '',

      };

      this.onSearchEvent(emptySearchEventData);
   }

   getSingerEventLocations(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      const searchParams = new HttpParams()
         .set('sortDirection', sortDirection)
         .set('sortColumn', sortColumn)
         .set('pageIndex', pageIndex.toString())
         .set('pageSize', pageSize.toString())
         .set('filter', filter);
      return this.apiService
         .get('api/eventlocation', searchParams)
         .pipe(map(res => res));
   }

   onResize(event) {
      this.breakpoint = event.target.innerWidth <= 500 ? 1 : 3;
   }

   getEvents(searchEventData: SearchEventData): Observable<EventDescription[]> {
      const searchParams = <SearchEventDTO>{
         startDate: searchEventData.startDateTime,
         endDate: searchEventData.endDateTime,
         locationId: searchEventData.locationId,
      };
      return this.apiService
         .post('api/event/search', searchParams)
         .pipe(map(res => res));
   }

   onSearchEvent(searchEventData: SearchEventData) {
      this.getEvents(searchEventData).subscribe(
         res =>
            (this.events = res.map(
               r =>
                  new EventDescription(
                     r.id,
                     r.title,
                     r.description,
                     r.ageGroups,
                     r.startDateTime,
                     r.endDateTime
                  )
            ))
      );
   }
}
