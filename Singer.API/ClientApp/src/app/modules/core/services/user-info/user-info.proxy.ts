import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { UserDescriptionDTO } from '../../models/userdescription.model';

@Injectable({
   providedIn: 'root',
})
export class UserInfoProxy {
   constructor(private apiService: ApiService) {}

   getUserInfo(): Observable<UserDescriptionDTO> {
      return this.apiService.get('api/user/me').pipe(map(res => res));
   }
}
