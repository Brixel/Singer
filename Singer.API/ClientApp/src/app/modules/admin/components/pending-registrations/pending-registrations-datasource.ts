import { Registration } from 'src/app/modules/core/models/registration.model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { RegistrationDTO } from 'src/app/modules/core/DTOs/registration.dto';
import { PendingRegistrationsService } from 'src/app/modules/core/services/singerevents-api/pending-registrations-service';
import { RegistrationSearchDTO } from 'src/app/modules/core/DTOs/registration.dto';

export class PendingRegistrationsDatasource extends GenericDataSource<
   Registration,
   RegistrationDTO,
   null,
   null,
   PendingRegistrationsService,
   RegistrationSearchDTO
> {
   constructor(pendingRegistrationsService: PendingRegistrationsService) {
      super(pendingRegistrationsService);
   }
}
