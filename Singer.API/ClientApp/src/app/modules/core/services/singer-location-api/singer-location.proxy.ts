import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import {
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
   SingerLocationDTO,
} from '../../DTOs/singer-event-location.dto';

@Injectable({
   providedIn: 'root',
})
export class SingerLocationProxy {
   constructor(private apiService: ApiService) {}

   getSingerLocations(
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
      return this.apiService.get('api/location', searchParams).pipe(map(res => res));
   }

   updateSingerLocation(id: string, updateSingerLocationDTO: UpdateSingerLocationDTO) {
      return this.apiService.put(`api/location/${id}`, updateSingerLocationDTO).pipe(map(res => res));
   }

   createSingerLocation(createSingerLocationDTO: CreateSingerLocationDTO): Observable<SingerLocationDTO> {
      return this.apiService.post('api/location', createSingerLocationDTO).pipe(map(res => res));
   }
}
