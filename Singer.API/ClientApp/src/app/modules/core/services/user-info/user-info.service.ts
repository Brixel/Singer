import { Injectable } from '@angular/core';
import { UserInfoProxy } from './user-info.proxy';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
   providedIn: 'root',
})
export class UserInfoService {
   constructor(private userInfoProxy: UserInfoProxy) {}

   getUserInfo(): Observable<any> {
      return this.userInfoProxy.getUserInfo().pipe(map(res => res));
   }

   getLinkedUsers(): Observable<any> {
      return this.userInfoProxy.getLinkedUsers().pipe(map(res => res));
   }
}
