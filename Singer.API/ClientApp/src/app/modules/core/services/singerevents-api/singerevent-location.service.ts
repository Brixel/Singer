import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SingerEventLocationProxy } from './singerevent-location.proxy';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../models/pagination.model';
import {
   SingerEventLocation,
   UpdateSingerEventLocationDTO,
   CreateSingerEventLocationDTO,
} from '../../models/singer-event-location.dto';

@Injectable({
   providedIn: 'root',
})
export class SingerEventLocationService {
   constructor(private singerEventLocationProxy: SingerEventLocationProxy) {}

   fetchSingerEventLocationsData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      return this.singerEventLocationProxy
         .getSingerEventLocations(
            sortDirection,
            sortColumn,
            pageIndex,
            pageSize,
            filter
         )
         .pipe(map(res => res));
   }

   updateSingerEventLocation(updateSingerEventLocation: SingerEventLocation) {
      const updateSingerEventLocationDTO = <UpdateSingerEventLocationDTO>{
         name: updateSingerEventLocation.name,
         address: updateSingerEventLocation.address,
         postalCode: updateSingerEventLocation.postalCode,
         city: updateSingerEventLocation.city,
         country: updateSingerEventLocation.country,
      };
      return this.singerEventLocationProxy
         .updateSingerEventLocation(
            updateSingerEventLocation.id,
            updateSingerEventLocationDTO
         )
         .pipe(map(res => res));
   }

   createSingerEventLocation(createSingerEventLocation: SingerEventLocation) {
      const createSingerEventLocationDTO = <CreateSingerEventLocationDTO>{
         name: createSingerEventLocation.name,
         address: createSingerEventLocation.address,
         postalCode: createSingerEventLocation.postalCode,
         city: createSingerEventLocation.city,
         country: createSingerEventLocation.country,
      };
      return this.singerEventLocationProxy
         .createSingerEventLocation(createSingerEventLocationDTO)
         .pipe(map(res => res));
   }
}
