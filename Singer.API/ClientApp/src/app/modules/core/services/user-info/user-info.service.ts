import { Injectable } from '@angular/core';
import { UserInfoProxy } from './user-info.proxy';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { UserDescriptionDTO } from '../../models/userdescription.model';

@Injectable({
   providedIn: 'root',
})
export class UserInfoService {
   constructor(private userInfoProxy: UserInfoProxy) {}

   getUserInfo(): Observable<UserDescriptionDTO> {
      return this.userInfoProxy.getUserInfo().pipe(map(res => res));
   }
}
