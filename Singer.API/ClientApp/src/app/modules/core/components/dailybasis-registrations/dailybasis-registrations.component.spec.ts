import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DailybasisRegistrationsComponent } from './dailybasis-registrations.component';

describe('DailybasisRegistrationsComponent', () => {
   let component: DailybasisRegistrationsComponent;
   let fixture: ComponentFixture<DailybasisRegistrationsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [DailybasisRegistrationsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(DailybasisRegistrationsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
