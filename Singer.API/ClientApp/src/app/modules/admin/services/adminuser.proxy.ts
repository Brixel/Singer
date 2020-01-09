import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../core/DTOs/pagination.dto';
import {
   CreateAdminUserDTO,
   AdminUserDTO,
} from '../../core/DTOs/adminuser.dto';

@Injectable({
   providedIn: 'root',
})
export class AdminUserProxy {
   constructor(private apiService: ApiService) {}

   get(
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
         .get('api/admin', searchParams)
         .pipe(map(res => res));
   }
   updateAdmin(id: string, admin: AdminUserDTO) {
      return this.apiService
         .put(`api/admin/${id}`, admin)
         .pipe(map(res => res));
   }

   createAdmin(admin: CreateAdminUserDTO) {
      return this.apiService.post('api/admin', admin).pipe(map(res => res));
   }

   deleteAdmin(adminId: string) {
      return this.apiService
         .delete(`api/admin/${adminId}`)
         .pipe(map(res => res));
   }
}
