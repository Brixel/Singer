import { ApiService } from '../../core/services/api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { CareRegistrationResultDTO } from './careRegistrationResult.dto';
import { CreateCareRegistrationDTO } from './createCareRegistration.dto';
@Injectable()
export class CareRegistrationProxy {
   private readonly rootUrl = 'api/careregistration';
   constructor(private apiService: ApiService) {}
   createCareRegistration(createCareRegistration: CreateCareRegistrationDTO): Observable<CareRegistrationResultDTO> {
      return this.apiService.post(this.rootUrl, createCareRegistration).pipe(map(res => res));
   }
}
