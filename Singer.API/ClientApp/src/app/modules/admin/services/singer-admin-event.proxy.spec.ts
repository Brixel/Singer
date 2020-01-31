import { TestBed } from '@angular/core/testing';

import { SingerAdminEventProxy } from './singer-admin-event.proxy';

describe('SingerAdminEventProxy', () => {
   beforeEach(() => TestBed.configureTestingModule({}));

   it('should be created', () => {
      const service: SingerAdminEventProxy = TestBed.get(SingerAdminEventProxy);
      expect(service).toBeTruthy();
   });
});
