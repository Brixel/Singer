import { Injectable } from '@angular/core';
import { CareUserProxy } from './careuser.proxy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {
   UpdateCareUserDTO,
   CareUser,
   CreateCareUserDTO,
   CareUserDTO,
   RelatedCareUserDTO,
} from '../../models/careuser.model';
import { PaginationDTO } from '../../DTOs/pagination.dto';
import { EventRelevantCareUserDTO } from '../../DTOs/event-registration.dto';
@Injectable({
   providedIn: 'root',
})
export class CareUserService {
   constructor(private careuserProxy: CareUserProxy) {}
   fetchCareUsersData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO<CareUserDTO>> {
      return this.careuserProxy
         .getCareUsers(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map(res => res));
   }
   updateUser(updateUser: CareUser) {
      const updateCareUserDTO = <UpdateCareUserDTO>{
         ageGroup: updateUser.ageGroup,
         birthday: updateUser.birthDay,
         email: updateUser.email,
         firstName: updateUser.firstName,
         lastName: updateUser.lastName,
         hasTrajectory: updateUser.hasTrajectory,
         isExtern: updateUser.isExtern,
         legalGuardianUsersToAdd: updateUser.legalGuardianUsersToAdd,
         legalGuardianUsersToRemove: updateUser.legalGuardianUsersToRemove,
      };
      return this.careuserProxy.updateCareUser(updateUser.id, updateCareUserDTO).pipe(map(res => res));
   }

   createCareUser(createUser: CareUser) {
      const createCareUserDTO = <CreateCareUserDTO>{
         ageGroup: createUser.ageGroup,
         birthday: createUser.birthDay,
         email: createUser.email,
         firstName: createUser.firstName,
         lastName: createUser.lastName,
         hasTrajectory: createUser.hasTrajectory,
         isExtern: createUser.isExtern,
      };
      return this.careuserProxy.createCareuser(createCareUserDTO).pipe(map(res => res));
   }

   getOwnCareUsers(value: string): Observable<RelatedCareUserDTO[]> {
      return this.careuserProxy.getOwnCareUsers(value).pipe(map(res => res));
   }
}
