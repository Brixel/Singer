import { ApiService } from '../../core/services/api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { CreateCareRegistrationDTO } from '../../core/DTOs/create-care-registration.dto';
import { CareRegistrationResultDTO } from '../../core/DTOs/care-registration-result.dto';
@Injectable()
export class CareRegistrationProxy {
   private readonly rootUrl = 'api/careregistration';
   constructor(private apiService: ApiService) {}
   createCareRegistration(createCareRegistration: CreateCareRegistrationDTO): Observable<CareRegistrationResultDTO> {
      return this.apiService.post(this.rootUrl, createCareRegistration).pipe(map(res => res));
   }
}
