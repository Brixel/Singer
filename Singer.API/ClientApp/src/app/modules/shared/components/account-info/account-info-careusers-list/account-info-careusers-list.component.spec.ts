import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AccountInfoCareusersListComponent } from './account-info-careusers-list.component';

describe('AccountInfoCareusersListComponent', () => {
   let component: AccountInfoCareusersListComponent;
   let fixture: ComponentFixture<AccountInfoCareusersListComponent>;

   beforeEach(waitForAsync(() => {
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
