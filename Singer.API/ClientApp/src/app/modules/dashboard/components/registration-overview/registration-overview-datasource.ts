import { RegistrationOverview } from 'src/app/modules/core/models/registration.model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { RegistrationOverviewDTO } from 'src/app/modules/core/DTOs/registration.dto';
import { RegistrationService } from '../../../core/services/registration-api/registration-service';
import { RegistrationSearchDTO } from '../../../core/DTOs/registration.dto';

export class RegistrationOverviewDatasource extends GenericDataSource<
   RegistrationOverview,
   RegistrationOverviewDTO,
   null,
   null,
   RegistrationService,
   RegistrationSearchDTO
> {
   constructor(registrationsService: RegistrationService) {
      super(registrationsService);
   }
}
