import { Injectable } from '@angular/core';
import { LegalGuardian } from '../../models/legalguardian.model';

@Injectable({
   providedIn: 'root',
})
export class LegalguardiansService {
   constructor() {}

   fetchLegalGuardiansData() {
      return LEGALGUARDIANS_DATA;
   }

   updateLegalGuardian(updateLegalGuardian: LegalGuardian) {
      for (let index = 0; index < LEGALGUARDIANS_DATA.length; index++) {
         if(LEGALGUARDIANS_DATA[index].id === updateLegalGuardian.id)
         {
            LEGALGUARDIANS_DATA[index] = updateLegalGuardian;
            return;
         }
      }
   }

   createLegalGuardian(createLegalGuardian: LegalGuardian) {
      LEGALGUARDIANS_DATA.push(createLegalGuardian);
   }
}

const LEGALGUARDIANS_DATA: LegalGuardian[] = [
   {
      id: '1',
      firstName: 'Jaak',
      lastName: 'Lambrechts',
      email: 'jaak.lambrechts@gmail.com',
      userName: 'jaak_lambrechts',
      birthday: new Date('02/02/1984'),
      address: 'spalbeekstraat 31',
      phoneNumber: '0895555',
      gsm: '04945555',
   },
   {
      id: '2',
      firstName: 'Miranda',
      lastName: 'Voorpijl',
      email: 'miranda.voorpijl@hotmail.com',
      userName: 'miranda_voorpijl',
      birthday: new Date('03/03/1984'),
      address: 'ketelstraat 25',
      phoneNumber: '0896666',
      gsm: '04946666',
   },
];
