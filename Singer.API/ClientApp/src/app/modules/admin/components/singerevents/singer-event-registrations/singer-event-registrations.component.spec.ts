import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SingerEventRegistrationsComponent } from './singer-event-registrations.component';

describe('SingerEventRegistrationsComponent', () => {
   let component: SingerEventRegistrationsComponent;
   let fixture: ComponentFixture<SingerEventRegistrationsComponent>;

   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [SingerEventRegistrationsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(SingerEventRegistrationsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
