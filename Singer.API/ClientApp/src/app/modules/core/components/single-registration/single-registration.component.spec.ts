import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SingleRegistrationComponent } from './single-registration.component';

describe('SingleRegistrationsComponent', () => {
   let component: SingleRegistrationComponent;
   let fixture: ComponentFixture<SingleRegistrationComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [SingleRegistrationComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(SingleRegistrationComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
