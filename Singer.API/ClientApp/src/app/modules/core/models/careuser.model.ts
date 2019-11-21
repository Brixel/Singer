import { AgeGroup } from './enum';
import {
   LinkedLegalGuardianDTO,
   LinkedLegalGuardian,
} from './legalguardian.model';
import { SingerEventLocation } from './singer-event-location.dto';

export interface CareUserDTO {
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
   hasResources: boolean;
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
   hasResources: boolean;
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
   hasResources: boolean;
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
   hasResources: boolean;
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
   normalDaycareLocation: SingerEventLocation;
   vacationDaycareLocation: SingerEventLocation;
   hasResources: boolean;
   legalGuardianUsers: LinkedLegalGuardian[];
   legalGuardianUsersToAdd: string[];
   legalGuardianUsersToRemove: string[];
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
   normalDaycareLocation: SingerEventLocation;
   vacationDaycareLocation: SingerEventLocation;
   hasResources: boolean;
}
