import { RegistrationOverview } from 'src/app/modules/core/models/registration.model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { RegistrationOverviewDTO } from 'src/app/modules/core/DTOs/registration.dto';
import { RegistrationOverviewService } from '../../services/registration-api/registration-overview-service';
import { RegistrationSearchDTO } from '../../DTOs/registration.dto';

export class RegistrationOverviewDatasource extends GenericDataSource<
   RegistrationOverview,
   RegistrationOverviewDTO,
   null,
   null,
   RegistrationOverviewService,
   RegistrationSearchDTO
> {
   constructor(registrationsService: RegistrationOverviewService) {
      super(registrationsService);
   }
}
