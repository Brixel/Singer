import { CareUserDTO, CareUser } from './careuser.model';

export interface LegalGuardianDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
   careUsers: CareUserDTO[];
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
   firstName: string;
   lastName: string;
   email: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
   careUsers: CareUser[];
   careUsersToAdd: string[];
   careUsersToRemove: string[];
}
