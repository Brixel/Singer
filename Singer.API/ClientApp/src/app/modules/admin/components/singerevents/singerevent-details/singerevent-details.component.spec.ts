import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SingerEventDetailsComponent } from './singerevent-details.component';

describe('SingereventDetailsComponent', () => {
   let component: SingerEventDetailsComponent;
   let fixture: ComponentFixture<SingerEventDetailsComponent>;

   beforeEach(waitForAsync(() => {
      TestBed.configureTestingModule({
         declarations: [SingerEventDetailsComponent],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(SingerEventDetailsComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should create', () => {
      expect(component).toBeTruthy();
   });
});
