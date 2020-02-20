import { AgeGroup } from './enum';

export interface UserDescriptionDTO {
   email: string;
   firstName: string;
   lastName: string;
   isAdmin: boolean;
   address: string;
   city: string;
   country: string;
   postalCode: string;
   careUsers: RelatedCareUserDTO[];
}

export interface RelatedCareUserDTO {
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
   birthDay: Date;
}
