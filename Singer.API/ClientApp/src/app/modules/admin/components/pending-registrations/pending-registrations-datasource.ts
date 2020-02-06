import { Registration } from 'src/app/modules/core/models/singerevent.model';
import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { RegistrationDTO } from 'src/app/modules/core/DTOs/event-registration.dto';
import { SingerEventsService } from 'src/app/modules/core/services/singerevents-api/singerevents.service';
import { PendingRegistrationsService } from 'src/app/modules/core/services/singerevents-api/pending-registrations-service';

export class PendingRegistrationsDatasource extends GenericDataSource<Registration, RegistrationDTO> {
   constructor(pendingRegistrationsService: PendingRegistrationsService) {
      super(pendingRegistrationsService);
   }
}
