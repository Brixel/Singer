import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import {
   SingerLocationDTO,
   SingerLocationSearchDTO,
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
} from 'src/app/modules/core/DTOs/singer-event-location.dto';
import { SingerLocationService } from 'src/app/modules/core/services/singer-location-api/singer-location.service';
import { SingerLocation } from 'src/app/modules/core/models/singer-location.model';

export class SingerLocationDataSource extends GenericDataSource<
   SingerLocation,
   SingerLocationDTO,
   UpdateSingerLocationDTO,
   CreateSingerLocationDTO,
   SingerLocationService,
   SingerLocationSearchDTO
> {
   constructor(service: SingerLocationService) {
      super(service);
   }
}
