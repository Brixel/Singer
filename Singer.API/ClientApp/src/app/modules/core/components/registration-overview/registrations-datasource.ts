import { Registration } from 'src/app/modules/core/models/singerevent.model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { RegistrationDTO } from 'src/app/modules/core/DTOs/event-registration.dto';
import { RegistrationService } from '../../services/registration-api/registration-service';
import { RegistrationSearchDTO } from '../../DTOs/registration.dto';

export class RegistrationsDatasource extends GenericDataSource<
   Registration,
   RegistrationDTO,
   null,
   null,
   RegistrationService,
   RegistrationSearchDTO
> {
   constructor(registrationsService: RegistrationService) {
      super(registrationsService);
   }
}
