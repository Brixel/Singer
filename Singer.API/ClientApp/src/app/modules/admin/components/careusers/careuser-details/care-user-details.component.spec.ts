import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CareUserDetailsComponent } from './care-user-details.component';

describe('CareUserDetailsComponent', () => {
   let component: CareUserDetailsComponent;
   let fixture: ComponentFixture<CareUserDetailsComponent>;

   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [CareUserDetailsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(CareUserDetailsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
