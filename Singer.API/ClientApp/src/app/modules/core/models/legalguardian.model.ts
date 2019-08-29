import { CareUserDTO } from './careuser.model';

export interface LegalGuardianDTO {
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
}
