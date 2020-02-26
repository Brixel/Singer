import { EventRegistrationTypes } from '../../legalguardians/components/register-care-wizard/register-care-wizard.component';

export interface CreateCareRegistrationDTO {
   careUserIds: string[];
   startDateTime: Date;
   endDateTime: Date;
   eventRegistrationType: EventRegistrationTypes;
}
