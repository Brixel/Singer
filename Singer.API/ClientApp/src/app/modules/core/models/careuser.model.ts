import { AgeGroup } from './enum';
import { LinkedLegalGuardianDTO, LinkedLegalGuardian } from './legalguardian.model';
import { SingerLocation } from './singer-location.model';

export interface CareUserDTO {
   id: string;
   userId: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDay: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
   normalDaycareLocation: SingerLocation;
   vacationDaycareLocation: SingerLocation;
   legalGuardianUsers: LinkedLegalGuardianDTO[];
}

export interface LinkedCareUserDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthday: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
   normalDaycareLocation: SingerLocation;
   vacationDaycareLocation: SingerLocation;
}

export interface UpdateCareUserDTO {
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthday: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
   normalDaycareLocationId: string;
   vacationDaycareLocationId: string;
   legalGuardianUsersToAdd: string[];
   legalGuardianUsersToRemove: string[];
}

export interface CreateCareUserDTO {
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthday: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
   normalDaycareLocationId: string;
   vacationDaycareLocationId: string;
}

export class CareUser {
   id: string;
   userId: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDay: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
   legalGuardianUsers: LinkedLegalGuardian[];
   legalGuardianUsersToAdd?: string[];
   legalGuardianUsersToRemove?: string[];
}

export class LinkedCareUser {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDay: Date;
   caseNumber: string;
   ageGroup: AgeGroup;
   isExtern: boolean;
   hasTrajectory: boolean;
}

export interface RelatedCareUserDTO {
   id: string;
   userId: string;
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
}
