import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

describe('AccountInfoSummaryComponent', () => {
   let component: AccountInfoSummaryComponent;
   let fixture: ComponentFixture<AccountInfoSummaryComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [AccountInfoSummaryComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(AccountInfoSummaryComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
