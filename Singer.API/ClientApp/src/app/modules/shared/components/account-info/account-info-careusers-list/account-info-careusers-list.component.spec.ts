import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountInfoCareusersListComponent } from './account-info-careusers-list.component';

describe('AccountInfoCareusersListComponent', () => {
   let component: AccountInfoCareusersListComponent;
   let fixture: ComponentFixture<AccountInfoCareusersListComponent>;

   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [AccountInfoCareusersListComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(AccountInfoCareusersListComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
