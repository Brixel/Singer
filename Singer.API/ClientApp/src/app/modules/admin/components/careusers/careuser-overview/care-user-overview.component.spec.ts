import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {
   MatPaginatorModule,
   MatSortModule,
   MatTableModule,
   MatFormFieldModule,
   MatSpinner,
   MatInputModule,
   MatCardModule,
   MatProgressSpinnerModule,
} from '@angular/material';

import { CareUserOverviewComponent } from './care-user-overview.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AgegroupPipe } from 'src/app/modules/core/services/agegroup.pipe';
import { CoreModule } from 'src/app/modules/core/core.module';
import { CommonModule } from '@angular/common';
import { CareUserProxy } from 'src/app/modules/core/services/care-users-api/careuser.proxy';
import { CareUserService } from 'src/app/modules/core/services/care-users-api/careusers.service';
import { ApiService } from 'src/app/modules/core/services/api.service';
import { HttpClient } from '@angular/common/http';
import { MockHttpClient } from 'testing/mock-http-client.helper';
import { apiServiceMock } from 'testing/api.service.mock';
import { of } from 'rxjs';

describe('OverviewComponent', () => {
   let component: CareUserOverviewComponent;
   let fixture: ComponentFixture<CareUserOverviewComponent>;
   let careUserService: CareUserService;
   beforeEach(async(() => {
      TestBed.configureTestingModule({
         declarations: [CareUserOverviewComponent],
         imports: [
            NoopAnimationsModule,
            CoreModule,
            CommonModule,
            MatTableModule,
            MatPaginatorModule,
            MatSortModule,
            MatFormFieldModule,
            MatInputModule,
            MatCardModule,
            MatProgressSpinnerModule

         ],
         providers: [AgegroupPipe, { provide: ApiService, useValue: apiServiceMock }, {provide: HttpClient, useValue: MockHttpClient}, {provide: 'BASE_URL', useValue: 'http://'}, CareUserProxy, CareUserService]
      }).compileComponents();
   }));

   beforeEach(() => {
      fixture = TestBed.createComponent(CareUserOverviewComponent);
      component = fixture.componentInstance;
      careUserService = TestBed.get(CareUserService);

      spyOn(careUserService, 'fetchCareUsersData').and.returnValue(of({}));
      fixture.detectChanges();
   });

   it('should compile', () => {
      expect(component).toBeTruthy();
   });
});
