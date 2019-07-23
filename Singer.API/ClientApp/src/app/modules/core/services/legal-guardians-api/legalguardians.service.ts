import { Injectable } from '@angular/core';
import { LegalGuardianProxy } from '../../services/legal-guardians-api/legalguardians.proxy';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {
   UpdateLegalGuardianDTO,
   CreateLegalGuardianDTO,
   LegalGuardian,
} from '../../models/legalguardian.model';
import { PaginationDTO } from '../../models/pagination.model';

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
         .getLegalGuardians(
            sortDirection,
            sortColumn,
            pageIndex,
            pageSize,
            filter
         )
         .pipe(map(res => res));
   }

   updateLegalGuardian(updateLegalGuardian: LegalGuardian) {
      const updateLegalGuardianDTO = <UpdateLegalGuardianDTO>{
         firstName: updateLegalGuardian.firstName,
         lastName: updateLegalGuardian.lastName,
         email: updateLegalGuardian.email,
         userName: updateLegalGuardian.userName,
         birthDate: updateLegalGuardian.birthDate,
         address: updateLegalGuardian.address,
         phoneNumber: updateLegalGuardian.phoneNumber,
         gsm: updateLegalGuardian.gsm,
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
         userName: createLegalGuardian.userName,
         birthDate: createLegalGuardian.birthDate,
         address: createLegalGuardian.address,
         phoneNumber: createLegalGuardian.phoneNumber,
         gsm: createLegalGuardian.gsm,
      };
      return this.legalguardianProxy
         .createLegalGuardian(createLegalGuardianDTO)
         .pipe(map(res => res));
   }
}
