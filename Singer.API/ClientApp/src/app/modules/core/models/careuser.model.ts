import { AgeGroup } from './enum';
import {
   LinkedLegalGuardianDTO,
   LinkedLegalGuardian,
} from './legalguardian.model';
import { SingerEventLocation } from './singerevent.model';

export interface CareUserDTO {
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
   normalDaycareLocation: SingerEventLocation;
   vacationDaycareLocation: SingerEventLocation;
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
   normalDaycareLocation: SingerEventLocation;
   vacationDaycareLocation: SingerEventLocation;
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
