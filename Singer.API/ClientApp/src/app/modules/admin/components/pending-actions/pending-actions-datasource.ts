import { GenericDataSource } from 'src/app/modules/core/services/generic-data-source';
import { EventRegistrationLog } from 'src/app/modules/core/models/event-registration-log.model';
import { EventRegistrationLogDTO } from 'src/app/modules/core/DTOs/event-registration-log.dto';
import { ActionNotificationsService } from 'src/app/modules/core/services/action-notification.service';

export class PendingActionsDataSource extends GenericDataSource<EventRegistrationLog, EventRegistrationLogDTO> {
   constructor(pendingActionsDataSource: ActionNotificationsService) {
      super(pendingActionsDataSource);
   }
}
