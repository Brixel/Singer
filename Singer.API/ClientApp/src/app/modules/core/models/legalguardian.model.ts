export interface LegalGuardianDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDate: Date;
   address: string;
   phoneNumber: string;
   gsm: string;
}

export interface UpdateLegalGuardianDTO {
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDate: Date;
   address: string;
   phoneNumber: string;
   gsm: string;
}

export interface CreateLegalGuardianDTO {
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDate: Date;
   address: string;
   phoneNumber: string;
   gsm: string;
}

export class LegalGuardian {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   birthDate: Date;
   address: string;
   phoneNumber: string;
   gsm: string;
}
