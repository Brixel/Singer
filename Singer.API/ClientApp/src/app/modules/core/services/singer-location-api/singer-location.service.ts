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
import { endpoint } from '../auth-config';

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
   constructor(private singerLocationProxy: SingerLocationProxy, protected httpClient: HttpClient) {
      super(`${endpoint}/api/location`);
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

   fetchSingerLocationsData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      return this.singerLocationProxy
         .getSingerLocations(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map((res) => res));
   }

   updateSingerLocation(updateSingerLocation: SingerLocation) {
      const updateSingerLocationDTO = <UpdateSingerLocationDTO>{
         name: updateSingerLocation.name,
         address: updateSingerLocation.address,
         postalCode: updateSingerLocation.postalCode,
         city: updateSingerLocation.city,
         country: updateSingerLocation.country,
      };
      return this.singerLocationProxy
         .updateSingerLocation(updateSingerLocation.id, updateSingerLocationDTO)
         .pipe(map((res) => res));
   }

   createSingerLocation(createSingerLocation: SingerLocation) {
      const createSingerLocationDTO = <CreateSingerLocationDTO>{
         name: createSingerLocation.name,
         address: createSingerLocation.address,
         postalCode: createSingerLocation.postalCode,
         city: createSingerLocation.city,
         country: createSingerLocation.country,
      };
      return this.singerLocationProxy.createSingerLocation(createSingerLocationDTO).pipe(map((res) => res));
   }
}
