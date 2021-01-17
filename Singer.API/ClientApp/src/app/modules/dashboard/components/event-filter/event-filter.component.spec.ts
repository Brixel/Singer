import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EventFilterComponent } from './event-filter.component';

describe('EventSearchComponent', () => {
   let component: EventFilterComponent;
   let fixture: ComponentFixture<EventFilterComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [EventFilterComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(EventFilterComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
