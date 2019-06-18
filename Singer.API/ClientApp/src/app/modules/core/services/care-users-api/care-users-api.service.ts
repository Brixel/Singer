import { Injectable } from '@angular/core';
import { CareUserProxy } from './careuser.proxy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginationDTO, UpdateCareUserDTO } from '../../models/careuser.model';

@Injectable({
   providedIn: 'root',
})
export class CareUsersService {

   constructor(private careuserProxy: CareUserProxy) {}

   fetchCareUsersData(
      sortDirection? :string,
      sortColumn?: string,
      pageIndex?:number,
      pageSize?:number,
      filter?:string):Observable<PaginationDTO> {
      return this.careuserProxy.getCareUsers(sortDirection, sortColumn, pageIndex, pageSize, filter).pipe(map((res) => res));
   }

   updateUser(result: CareUser) {
      const updateCareUserDTo = <UpdateCareUserDTO>{
         ageGroup: result.ageGroup,
         birthday: result.birthDay,
         caseNumber: result.caseNumber,
         email: result.email,
         firstName:result.firstName,
         lastName:result.lastName,
         hasNormalDayCare: result.hasNormalDayCare,
         hasResources: result.hasResources,
         hasTrajectory: result.hasTrajectory,
         hasVacationDayCare: result.hasVacationDayCare,
         isExtern: result.isExtern
      }
      return this.careuserProxy.updateCareUser(result.id, updateCareUserDTo).pipe(map((res) => res));
   }
}

//http://api/carusers?filter="filterValue";sort="sortProperty";page=0

// Care User class
export class CareUser {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDay: Date;
   caseNumber: string;
   ageGroup: string; //Maybe replace by own class?
   isExtern: boolean;
   hasTrajectory: boolean;
   hasNormalDayCare: boolean;
   hasVacationDayCare: boolean;
   hasResources: boolean;
}

// Dummy Data
const EXAMPLE_DATA: CareUser[] = [
   {
      id: '1',
      firstName: 'Joske',
      lastName: 'Vermeulen',
      email: 'joske.vermeulen@gmail.com',
      userName: 'joske',
      birthDay: new Date('2008-07-06'),
      caseNumber: '0123456789',
      ageGroup: 'Kinderen',
      isExtern: false,
      hasTrajectory: true,
      hasNormalDayCare: true,
      hasVacationDayCare: true,
      hasResources: true,
   },
   {
      id: '2',
      firstName: 'Kim',
      lastName: 'Janssens',
      email: 'kim.janssens@gmail.com',
      userName: 'kim',
      birthDay: new Date('2006-07-08'),
      caseNumber: '9876543210',
      ageGroup: 'Kinderen',
      isExtern: true,
      hasTrajectory: true,
      hasNormalDayCare: true,
      hasVacationDayCare: true,
      hasResources: true,
   },
   {
      id: '3',
      firstName: 'Benjamin',
      lastName: 'Vermeulen',
      email: 'benjamin.vermeulen@gmail.com',
      userName: 'benjamin',
      birthDay: new Date('2010-08-06'),
      caseNumber: '091837465',
      ageGroup: 'Jongeren',
      isExtern: false,
      hasTrajectory: true,
      hasNormalDayCare: true,
      hasVacationDayCare: true,
      hasResources: false,
   },
];
