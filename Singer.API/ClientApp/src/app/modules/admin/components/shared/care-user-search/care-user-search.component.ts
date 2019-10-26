import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import {
   startWith,
   debounceTime,
   switchMap,
   map,
   catchError,
} from 'rxjs/operators';
import { of, Observable, Subject } from 'rxjs';
import { CareUser } from 'src/app/modules/core/models/careuser.model';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { MatOptionSelectionChange } from '@angular/material';

@Component({
   selector: 'app-care-user-search',
   templateUrl: './care-user-search.component.html',
   styleUrls: ['./care-user-search.component.css'],
})
export class CareUserSearchComponent implements OnInit {
   @Output() userSelected = new EventEmitter<CareUser>();

   control: FormControl;
   careUsersAutoComplete$: Observable<CareUser[]>;

   constructor(private _careUserService: CareUserService) {}

   ngOnInit() {
      this.control = new FormControl('');
      this.careUsersAutoComplete$ = this.control.valueChanges.pipe(
         startWith(''),
         debounceTime(300),
         switchMap(value => {
            if (typeof value === 'string' && value !== '') {
               return this.careUserLookup(value);
            } else {
               return of(null);
            }
         })
      );
   }

   careUserLookup(value: string): Observable<CareUser[]> {
      return this._careUserService
         .fetchCareUsersData('asc', 'firstName', 0, 15, value)
         .pipe(map(res => res.items));
   }

   selectCareUser(careUser: CareUser, event: MatOptionSelectionChange) {
      this.userSelected.emit(careUser);
      this.control.reset();
   }

   showUserNameFn(careUser?: CareUser): string | undefined {
      return careUser
         ? `${careUser.firstName} ${careUser.lastName}`
         : undefined;
   }
}
