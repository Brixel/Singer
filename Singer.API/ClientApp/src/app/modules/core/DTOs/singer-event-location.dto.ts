import { SearchDTOBase } from './base.dto';

export interface SingerLocationDTO {
   id: string;
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface CreateSingerLocationDTO {
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface UpdateSingerLocationDTO {
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}

export interface SingerLocationSearchDTO extends SearchDTOBase {}
