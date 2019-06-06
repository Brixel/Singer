import { Injectable } from '@angular/core';

@Injectable({
   providedIn: 'root',
})
export class CareUsersAPIService {
   constructor() {}

   fetchCareUsersData() {
      return EXAMPLE_DATA;
   }
}

// Care User class
export interface CareUser {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthday: Date;
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
      birthday: new Date('2008-07-06'),
      caseNumber: '0123456789',
      ageGroup: 'Child',
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
      birthday: new Date('2006-07-08'),
      caseNumber: '9876543210',
      ageGroup: 'Child',
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
      birthday: new Date('2010-08-06'),
      caseNumber: '091837465',
      ageGroup: 'Youngster',
      isExtern: false,
      hasTrajectory: true,
      hasNormalDayCare: true,
      hasVacationDayCare: true,
      hasResources: false,
   },
];
