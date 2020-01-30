import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {
   UpdateLegalGuardianDTO,
   CreateLegalGuardianDTO,
   LegalGuardianDTO,
} from '../../models/legalguardian.model';
import { PaginationDTO } from '../../DTOs/pagination.dto';

@Injectable({
   providedIn: 'root',
})
export class LegalGuardianProxy {
   constructor(private apiService: ApiService) {}

   getLegalGuardians(
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
         .get('api/legalguardianuser', searchParams)
         .pipe(map(res => res));
   }

   updateLegalGuardian(
      id: string,
      updateLegalGuardianDTO: UpdateLegalGuardianDTO
   ) {
      return this.apiService
         .put(`api/legalguardianuser/${id}`, updateLegalGuardianDTO)
         .pipe(map(res => res));
   }

   createLegalGuardian(
      createLegalGuardianDTO: CreateLegalGuardianDTO
   ): Observable<LegalGuardianDTO> {
      return this.apiService
         .post('api/legalguardianuser', createLegalGuardianDTO)
         .pipe(map(res => res));
   }

   deleteLegalGuardian(id: string): Observable<any> {
      return this.apiService.delete(`api/legalguardianuser/${id}`).pipe(map(res => res));
   }
}
