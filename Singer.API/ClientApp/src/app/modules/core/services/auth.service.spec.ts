import { TestBed, inject } from '@angular/core/testing';

import { AuthService } from './auth.service';
import { HttpClient } from 'selenium-webdriver/http';
import { MockHttpClient } from 'testing/mock-http-client.helper';

describe('AuthService', () => {
   beforeEach(() => {
      TestBed.configureTestingModule({
         providers: [AuthService, { provide: HttpClient, useValue: MockHttpClient }],
      });
   });

   it('should be created', inject([AuthService], (service: AuthService) => {
      expect(service).toBeTruthy();
   }));
});
