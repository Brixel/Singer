import { TestBed } from '@angular/core/testing';
import { SingerAdminEventService } from './singer-admin-event.service';

describe('SingerAdminEventService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: SingerAdminEventService = TestBed.get(SingerAdminEventService);
      expect(service).toBeTruthy();
   });
});
