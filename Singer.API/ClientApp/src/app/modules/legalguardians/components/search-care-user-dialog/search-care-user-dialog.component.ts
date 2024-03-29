import { Component, OnInit, Inject } from '@angular/core';
import { RelatedCareUserDTO } from 'src/app/modules/core/models/careuser.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
   SingerRegistrationsComponent,
   SingerRegistrationData,
} from 'src/app/modules/admin/components/singerevents/event-registrations/event-registrations.component';
import { UntypedFormControl } from '@angular/forms';
import { startWith, debounceTime, switchMap, map } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { AgeGroup } from 'src/app/modules/core/models/enum';
import { AuthService } from 'src/app/modules/core/services/auth.service';

@Component({
   selector: 'app-search-care-user-dialog',
   templateUrl: './search-care-user-dialog.component.html',
   styleUrls: ['./search-care-user-dialog.component.css'],
})
export class SearchCareUserDialogComponent implements OnInit {
   control: UntypedFormControl;
   careUsersAutoComplete$: Observable<RelatedCareUser[]>;

   constructor(
      private _careUserService: CareUserService,
      private dialogRef: MatDialogRef<SingerRegistrationsComponent>,
      @Inject(MAT_DIALOG_DATA) data: SingerRegistrationData,
      private _authService: AuthService
   ) {}

   ngOnInit() {
      this.control = new UntypedFormControl('');
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
   careUserLookup(value: string): Observable<RelatedCareUser[]> {
      if (this._authService.isAdmin$.value) {
         return this._careUserService
            .fetchCareUsersData('asc', 'id', 0, 15, value)
            .pipe(map(res => res.items.map(careUser => new RelatedCareUser(careUser))));
      } else {
         return this._careUserService
            .getOwnCareUsers(value)
            .pipe(map(res => res.map(careUser => new RelatedCareUser(careUser))));
      }
   }
   close() {
      this.dialogRef.close();
   }

   showUserNameFn(careUser?: RelatedCareUser): string | undefined {
      return careUser ? `${careUser.firstName} ${careUser.lastName}` : undefined;
   }

   onUserSelect(careUser: RelatedCareUser) {
      this.dialogRef.close(careUser);
   }
}

export class RelatedCareUser {
   id: string;
   firstName: string;
   lastName: string;
   ageGroup: AgeGroup;
   constructor(relatedCareUser: RelatedCareUserDTO) {
      this.id = relatedCareUser.id;
      this.firstName = relatedCareUser.firstName;
      this.lastName = relatedCareUser.lastName;
      this.ageGroup = relatedCareUser.ageGroup;
   }
}
