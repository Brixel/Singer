import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule, MatSortModule, MatTableModule } from '@angular/material';

import { SingerEventOverviewComponent } from './singerevent-overview.component';

describe('EventsOverviewComponent', () => {
   let component: SingerEventOverviewComponent;
   let fixture: ComponentFixture<SingerEventOverviewComponent>;

   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [SingerEventOverviewComponent],
         imports: [NoopAnimationsModule, MatPaginatorModule, MatSortModule, MatTableModule],
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(SingerEventOverviewComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
   });

   it('should compile', () => {
      expect(component).toBeTruthy();
   });
});
