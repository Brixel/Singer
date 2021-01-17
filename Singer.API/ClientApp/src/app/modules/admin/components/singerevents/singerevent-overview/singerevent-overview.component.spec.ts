import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { SingerEventOverviewComponent } from './singerevent-overview.component';

describe('EventsOverviewComponent', () => {
   let component: SingerEventOverviewComponent;
   let fixture: ComponentFixture<SingerEventOverviewComponent>;

   beforeEach(waitForAsync(() => {
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
