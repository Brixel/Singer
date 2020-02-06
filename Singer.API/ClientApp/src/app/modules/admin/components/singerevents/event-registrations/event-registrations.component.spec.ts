import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingerRegistrationsComponent } from './event-registrations.component';

describe('SingerRegistrationsComponent', () => {
   let component: SingerRegistrationsComponent;
   let fixture: ComponentFixture<SingerRegistrationsComponent>;

   beforeEach(async(() => {
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
