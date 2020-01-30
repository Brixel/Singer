import { TestBed } from '@angular/core/testing';

import { SingerEventsService } from './singerevents.service';

describe('SingerEventsService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: SingerEventsService = TestBed.get(SingerEventsService);
      expect(service).toBeTruthy();
   });
});
