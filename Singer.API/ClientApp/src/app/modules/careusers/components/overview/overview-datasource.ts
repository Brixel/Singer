import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { CareUsersAPIService } from 'src/app/modules/core/services/care-users-api/care-users-api.service';

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

// TODO: replace this with real data from your application
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

/**
 * Data source for the Overview view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class OverviewDataSource extends MatTableDataSource<CareUser> {
   data: CareUser[];

   constructor(
      public paginator: MatPaginator,
      public sort: MatSort,
      private careUsersAPI: CareUsersAPIService
   ) {
      super();
      this.data = careUsersAPI.fetchCareUsersData();
   }

   /**
    *  Called when the table is being destroyed. Use this function, to clean up
    * any open connections or free any held resources that were set up during connect.
    */
   disconnect() {}
}
