import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PaginationDTO } from '../../models/careuser.model';

@Injectable({
   providedIn: 'root'
})
export class CareUserProxy{
   constructor(private apiService: ApiService){}

   getCareUsers(sortDirection?:string, sortColumn?:string, pageIndex?:number, pageSize?:number):Observable<PaginationDTO>{
      const searchParams = new HttpParams()
      .set('pageIndex', pageIndex.toString())
      .set('pageSize', pageSize.toString());
      return this.apiService.get('api/careuser', searchParams).pipe(map((res) => res));
   }

}
