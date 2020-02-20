import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
   providedIn: 'root',
})
export class UserInfoProxy {
   constructor(private apiService: ApiService) {}

   getUserInfo(): Observable<any> {
      return this.apiService.get('api/user').pipe(map(res => res));
   }

   getLinkedUsers(): Observable<any> {
      return this.apiService.get('api/user/linkedusers').pipe(map(res => res));
   }
}
