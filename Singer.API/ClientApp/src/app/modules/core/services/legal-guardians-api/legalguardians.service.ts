import { Injectable } from '@angular/core';
import { LegalGuardianProxy } from '../../services/legal-guardians-api/legalguardians.proxy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UpdateLegalGuardianDTO, CreateLegalGuardianDTO, LegalGuardian } from '../../models/legalguardian.model';
import { PaginationDTO } from '../../DTOs/pagination.dto';

@Injectable({
   providedIn: 'root',
})
export class LegalguardiansService {
   constructor(private legalguardianProxy: LegalGuardianProxy) {}

   fetchLegalGuardiansData(
      sortDirection?: string,
      sortColumn?: string,
      pageIndex?: number,
      pageSize?: number,
      filter?: string
   ): Observable<PaginationDTO> {
      return this.legalguardianProxy
         .getLegalGuardians(sortDirection, sortColumn, pageIndex, pageSize, filter)
         .pipe(map(res => res));
   }

   updateLegalGuardian(updateLegalGuardian: LegalGuardian) {
      const updateLegalGuardianDTO = <UpdateLegalGuardianDTO>{
         firstName: updateLegalGuardian.firstName,
         lastName: updateLegalGuardian.lastName,
         email: updateLegalGuardian.email,
         address: updateLegalGuardian.address,
         postalCode: updateLegalGuardian.postalCode,
         city: updateLegalGuardian.city,
         country: updateLegalGuardian.country,
         careUsersToAdd: updateLegalGuardian.careUsersToAdd,
         careUsersToRemove: updateLegalGuardian.careUsersToRemove,
      };
      return this.legalguardianProxy
         .updateLegalGuardian(updateLegalGuardian.id, updateLegalGuardianDTO)
         .pipe(map(res => res));
   }

   createLegalGuardian(createLegalGuardian: LegalGuardian) {
      const createLegalGuardianDTO = <CreateLegalGuardianDTO>{
         firstName: createLegalGuardian.firstName,
         lastName: createLegalGuardian.lastName,
         email: createLegalGuardian.email,
         address: createLegalGuardian.address,
         postalCode: createLegalGuardian.postalCode,
         city: createLegalGuardian.city,
         country: createLegalGuardian.country,
      };
      return this.legalguardianProxy.createLegalGuardian(createLegalGuardianDTO).pipe(map(res => res));
   }

   deleteLegalGuardian(id: string) {
      return this.legalguardianProxy.deleteLegalGuardian(id).pipe(map(res => res));
   }
}
