import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';

/**
 * Data source for the AddFamilyWizard.
 */
export class AddFamilyWizardDataSource {
   legalGuardians: LegalGuardian[];
   careUsers: CareUser[];

   constructor(
      private careUserService: CareUserService,
      private legalguardiansService: LegalguardiansService
   ) {}
}
