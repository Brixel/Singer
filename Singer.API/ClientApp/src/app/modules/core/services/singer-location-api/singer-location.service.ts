import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import { UpdateSingerEventLocationDTO, CreateSingerEventLocationDTO } from '../../DTOs/singer-event-location.dto';
import { SingerLocation } from '../../models/singer-location.model';
import { SingerLocationProxy } from './singer-location.proxy';

@Injectable({
   providedIn: 'root',
})
export class SingerLocationService {
   constructor(private singerEventLocationProxy: SingerLocationProxy) {}

   fetchSingerEventLocationsData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      return this.singerEventLocationProxy
         .getSingerEventLocations(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map(res => res));
   }

   updateSingerEventLocation(updateSingerEventLocation: SingerLocation) {
      const updateSingerEventLocationDTO = <UpdateSingerEventLocationDTO>{
         name: updateSingerEventLocation.name,
         address: updateSingerEventLocation.address,
         postalCode: updateSingerEventLocation.postalCode,
         city: updateSingerEventLocation.city,
         country: updateSingerEventLocation.country,
      };
      return this.singerEventLocationProxy
         .updateSingerEventLocation(updateSingerEventLocation.id, updateSingerEventLocationDTO)
         .pipe(map(res => res));
   }

   createSingerEventLocation(createSingerEventLocation: SingerLocation) {
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
