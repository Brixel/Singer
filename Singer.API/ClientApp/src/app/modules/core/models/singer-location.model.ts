import { GenericModel } from './generics/generic-model';

export class SingerLocation extends GenericModel {
   name: string;
   address: string;
   postalCode: string;
   city: string;
   country: string;
}
