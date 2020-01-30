import { TestBed } from '@angular/core/testing';

import { SingerEventLocationService } from './singerevent-location.service';

describe('SingerEventLocationService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: SingerEventLocationService = TestBed.get(SingerEventLocationService);
      expect(service).toBeTruthy();
   });
});
