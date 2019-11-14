import { TestBed } from '@angular/core/testing';

import { CareUserService } from './careusers.service';

describe('CareUsersAPIService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: CareUserService = TestBed.get(CareUserService);
      expect(service).toBeTruthy();
   });
});
