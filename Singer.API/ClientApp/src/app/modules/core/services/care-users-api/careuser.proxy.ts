import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PaginationDTO, UpdateCareUserDTO, CreateCareUserDTO, CareUserDTO } from '../../models/careuser.model';

@Injectable({
   providedIn: 'root'
})
export class CareUserProxy{

   constructor(private apiService: ApiService){}

   getCareUsers(sortDirection?:string, sortColumn?:string, pageIndex?:number, pageSize?:number, filter?:string):Observable<PaginationDTO>{
      const searchParams = new HttpParams()
      .set('sortDirection', sortDirection)
      .set('sortColumn', sortColumn)
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString())
      .set('filter', filter);
      return this.apiService.get('api/careuser', searchParams).pipe(map((res) => res));
   }

   updateCareUser(id: string, updateCareUserDTO: UpdateCareUserDTO) {
   return this.apiService.put(`api/careuser/${id}`, updateCareUserDTO).pipe(map((res) => res));
   }

   createCareuser(createCareUserDTO: CreateCareUserDTO):Observable<CareUserDTO>{
      return this.apiService.post('api/careuser', createCareUserDTO).pipe(map((res) => res));
   }

}
