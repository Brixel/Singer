export interface PaginationDTO{
   previousPageUrl:string;
   nextPageUrl: string;
   currentPageUrl: string;
   size: number;
   totalSize: number;
   items: any[];
}

export interface CareUserDTO {
   id: string;
   name:string;
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
