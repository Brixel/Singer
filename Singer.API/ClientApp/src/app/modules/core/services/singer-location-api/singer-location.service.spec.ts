import { TestBed } from '@angular/core/testing';

import { SingerLocationService } from './singer-location.service';

describe('SingerLocationService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: SingerLocationService = TestBed.get(SingerLocationService);
      expect(service).toBeTruthy();
   });
});
