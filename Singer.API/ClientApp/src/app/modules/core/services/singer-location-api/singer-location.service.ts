import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import {
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
   SingerLocationDTO,
   SingerLocationSearchDTO,
} from '../../DTOs/singer-event-location.dto';
import { SingerLocation } from '../../models/singer-location.model';
import { SingerLocationProxy } from './singer-location.proxy';
import { GenericService } from '../generic-service';
import { HttpClient } from '@angular/common/http';

@Injectable({
   providedIn: 'root',
})
export class SingerLocationService extends GenericService<
   SingerLocation,
   SingerLocationDTO,
   CreateSingerLocationDTO,
   UpdateSingerLocationDTO,
   SingerLocationSearchDTO
> {
   constructor(private singerEventLocationProxy: SingerLocationProxy, protected httpClient: HttpClient) {
      super('api/singerlocation');
   }

   toEditDTO(model: SingerLocation): UpdateSingerLocationDTO {
      return <UpdateSingerLocationDTO>{
         address: model.address,
         city: model.city,
         country: model.country,
         name: model.name,
         postalCode: model.postalCode,
      };
   }
   toCreateDTO(model: SingerLocation): CreateSingerLocationDTO {
      return <CreateSingerLocationDTO>{
         address: model.address,
         city: model.city,
         country: model.country,
         name: model.name,
         postalCode: model.postalCode,
      };
   }
   toModel(dto: SingerLocationDTO): SingerLocation {
      return <SingerLocation>{
         address: dto.address,
         city: dto.city,
         country: dto.country,
         id: dto.id,
         name: dto.name,
         postalCode: dto.postalCode,
      };
   }

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
      const updateSingerEventLocationDTO = <UpdateSingerLocationDTO>{
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
      const createSingerEventLocationDTO = <CreateSingerLocationDTO>{
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
