import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { CareUser, CareUserDTO } from 'src/app/modules/core/models/careuser.model';
import { Observable, of } from 'rxjs';
import { UntypedFormControl } from '@angular/forms';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { startWith, debounceTime, switchMap, map } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

@Component({
   selector: 'app-care-user-multi-autocomplete',
   templateUrl: './care-user-multi-autocomplete.component.html',
   styleUrls: ['./care-user-multi-autocomplete.component.css'],
})
export class CareUserMultiAutocompleteComponent implements OnInit {
   selectedCareUsers: CareUser[];
   careUsersAutoComplete$: Observable<CareUserDTO[]>;
   careUserCtrl: UntypedFormControl;
   separatorKeysCodes: number[] = [ENTER, COMMA];
   @Output() onChange: EventEmitter<CareUser[]> = new EventEmitter();
   @ViewChild('careUserInput', { static: true }) careUserInput: ElementRef<HTMLInputElement>;

   constructor(private careUserService: CareUserService) {
      this.selectedCareUsers = [];
   }

   ngOnInit() {
      this.careUserCtrl = new UntypedFormControl('');
      this.careUsersAutoComplete$ = this.careUserCtrl.valueChanges.pipe(
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

   removeCareUser(user: CareUser) {
      const index = this.selectedCareUsers.indexOf(user);

      if (index >= 0) {
         this.selectedCareUsers.splice(index, 1);
      }
      this.onChange.emit(this.selectedCareUsers);
   }

   careUserSelected(event: MatAutocompleteSelectedEvent): void {
      this.selectedCareUsers.push(event.option.value);
      this.onChange.emit(this.selectedCareUsers);
      this.careUserCtrl.setValue(null);
      this.careUserInput.nativeElement.value = '';
   }

   careUserLookup(value: string): Observable<CareUserDTO[]> {
      return this.careUserService.fetchCareUsersData('asc', 'firstName', 0, 15, value).pipe(
         map(res =>
            res.items.filter(user => {
               return this.selectedCareUsers.find(x => x.userId === user.userId) === undefined;
            })
         )
      );
   }
}
