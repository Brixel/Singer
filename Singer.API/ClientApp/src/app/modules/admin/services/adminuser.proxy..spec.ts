import { TestBed } from '@angular/core/testing';

import { AdminUserProxy } from './adminuser.proxy';

describe('AdminUserProxy', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: AdminUserProxy = TestBed.get(AdminUserProxy);
      expect(service).toBeTruthy();
   });
});
