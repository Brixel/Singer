import { TestBed } from '@angular/core/testing';

import { CareUsersService } from './care-users-api.service';

describe('CareUsersAPIService', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: CareUsersService = TestBed.get(CareUsersService);
      expect(service).toBeTruthy();
   });
});
