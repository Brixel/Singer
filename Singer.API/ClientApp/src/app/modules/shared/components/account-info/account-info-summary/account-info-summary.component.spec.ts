import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('AccountInfoSummaryComponent', () => {
   let component: AccountInfoSummaryComponent;
   let fixture: ComponentFixture<AccountInfoSummaryComponent>;

   beforeEach(async(() => {
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
