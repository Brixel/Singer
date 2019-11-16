import { Injectable } from '@angular/core';
import { LegalGuardian } from 'src/app/modules/core/models/legalguardian.model';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { LegalguardiansService } from 'src/app/modules/core/services/legal-guardians-api/legalguardians.service';
import { map } from 'rxjs/operators';

/**
 * Data source for the AddFamilyWizard.
 */
@Injectable({
   providedIn: 'root',
})
export class AddFamilyWizardDataSource {
   legalGuardians: LegalGuardian[];
   careUsers: CareUser[];

   constructor(
      private careUserService: CareUserService,
      private legalguardiansService: LegalguardiansService,
   ) {}

   addLegalGuardian(legalguardian: LegalGuardian) {
      return this.legalguardiansService.createLegalGuardian(legalguardian).pipe(map(res => res));
   }

   addCareUser(careUser: CareUser) {
      return this.careUserService.createCareUser(careUser).pipe(map(res => res));
   }


}
