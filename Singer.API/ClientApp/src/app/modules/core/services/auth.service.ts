import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
   public isAdmin$ = new BehaviorSubject<boolean>(false);
   isAdmin = false;

   public isLegalGuardian$ = new BehaviorSubject<boolean>(false);
}
