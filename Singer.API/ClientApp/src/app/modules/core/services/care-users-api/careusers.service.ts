import { Injectable } from '@angular/core';
import { CareUserProxy } from './careuser.proxy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {
   UpdateCareUserDTO,
   CareUser,
   CreateCareUserDTO,
} from '../../models/careuser.model';
import { PaginationDTO } from '../../models/pagination.model';
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
   ): Observable<PaginationDTO> {
      return this.careuserProxy
         .getCareUsers(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map(res => res));
   }
   updateUser(updateUser: CareUser) {
      const updateCareUserDTo = <UpdateCareUserDTO>{
         ageGroup: updateUser.ageGroup,
         birthday: updateUser.birthDay,
         caseNumber: updateUser.caseNumber,
         email: updateUser.email,
         firstName: updateUser.firstName,
         lastName: updateUser.lastName,
         hasNormalDayCare: updateUser.hasNormalDayCare,
         hasResources: updateUser.hasResources,
         hasTrajectory: updateUser.hasTrajectory,
         hasVacationDayCare: updateUser.hasVacationDayCare,
         isExtern: updateUser.isExtern,
         legalGuardianUsersToAdd: updateUser.legalGuardianUsersToAdd,
         legalGuardianUsersToRemove: updateUser.legalGuardianUsersToRemove,
      };
      return this.careuserProxy
         .updateCareUser(updateUser.id, updateCareUserDTo)
         .pipe(map(res => res));
   }

   createCareUser(createUser: CareUser) {
      const createCareUserDTO = <CreateCareUserDTO>{
         ageGroup: createUser.ageGroup,
         birthday: createUser.birthDay,
         caseNumber: createUser.caseNumber,
         email: createUser.email,
         firstName: createUser.firstName,
         lastName: createUser.lastName,
         hasNormalDayCare: createUser.hasNormalDayCare,
         hasResources: createUser.hasResources,
         hasTrajectory: createUser.hasTrajectory,
         hasVacationDayCare: createUser.hasVacationDayCare,
         isExtern: createUser.isExtern,
      };
      return this.careuserProxy
         .createCareuser(createCareUserDTO)
         .pipe(map(res => res));
   }
}
