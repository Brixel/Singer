import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminUserProxy } from './adminuser.proxy';
import { map } from 'rxjs/operators';
import { PaginationDTO } from '../../core/models/pagination.model';
import {
   AdminUser,
   UpdateAdminUserDTO,
   CreateAdminUserDTO,
   AdminUserDTO,
} from '../../core/models/adminuser.model';

@Injectable({
   providedIn: 'root',
})
export class AdminUserService {
   constructor(private adminUserProxy: AdminUserProxy) {}
   get(
      sortDirection: string,
      sortColumn: string,
      pageIndex: number,
      pageSize: number,
      filter: string
   ): Observable<PaginationDTO> {
      return this.adminUserProxy
         .get(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map(res => res));
   }

   update(updateAdmin: AdminUser) {
      const updateAdminUserDTO = <AdminUserDTO>{
         firstName: updateAdmin.firstName,
         lastName: updateAdmin.lastName,
         email: updateAdmin.email,
         id: updateAdmin.id,
      };
      return this.adminUserProxy
         .updateAdmin(updateAdmin.id, updateAdminUserDTO)
         .pipe(map(res => res));
   }

   create(adminUser: AdminUser) {
      const createAdminUserDTO = <CreateAdminUserDTO>{
         firstName: adminUser.firstName,
         lastName: adminUser.lastName,
         email: adminUser.email,
      };
      return this.adminUserProxy
         .createAdmin(createAdminUserDTO)
         .pipe(map(res => res));
   }
}
