import { CareUserDTO, CareUser, LinkedCareUserDTO, LinkedCareUser } from './careuser.model';

export interface LegalGuardianDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
   careUsers: LinkedCareUserDTO[];
}
export interface LinkedLegalGuardianDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface UpdateLegalGuardianDTO {
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
   careUsersToAdd: string[];
   careUsersToRemove: string[];
}

export interface CreateLegalGuardianDTO {
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export class LegalGuardian {
   id: string;
   userId: string;
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
   careUsers: LinkedCareUser[];
   careUsersToAdd: string[];
   careUsersToRemove: string[];
}

export class LinkedLegalGuardian {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}
