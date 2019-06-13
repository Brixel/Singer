import { TestBed } from '@angular/core/testing';

import { CareUsersAPIService } from './care-users-api.service';

describe('CareUsersAPIService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: CareUsersAPIService = TestBed.get(CareUsersAPIService);
      expect(service).toBeTruthy();
   });
});
