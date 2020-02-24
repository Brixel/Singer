import { EventRegistrationTypes } from '../shared/register-care-wizard/register-care-wizard.component';
export interface CreateCareRegistrationDTO {
   careUserIds: string[];
   startDateTime: Date;
   endDateTime: Date;
   eventRegistrationType: EventRegistrationTypes;
}
