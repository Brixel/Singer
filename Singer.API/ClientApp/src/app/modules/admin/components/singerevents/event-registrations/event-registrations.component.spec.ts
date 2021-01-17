import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SingerRegistrationsComponent } from './event-registrations.component';

describe('SingerRegistrationsComponent', () => {
   let component: SingerRegistrationsComponent;
   let fixture: ComponentFixture<SingerRegistrationsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [SingerRegistrationsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(SingerRegistrationsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
