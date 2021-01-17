import { TestBed, inject, waitForAsync } from '@angular/core/testing';

import { AuthGuard } from './auth.guard';
import { HttpClient } from 'selenium-webdriver/http';
import { MockHttpClient } from 'testing/mock-http-client.helper';
import { AuthService } from './auth.service';

describe('AuthGuard', () => {
   beforeEach(() => {
      TestBed.configureTestingModule({
         providers: [AuthGuard, { provide: HttpClient, useValue: MockHttpClient }, AuthService],
      });
   });

   it('should ...', inject([AuthGuard], (guard: AuthGuard) => {
      expect(guard).toBeTruthy();
   }));
});
