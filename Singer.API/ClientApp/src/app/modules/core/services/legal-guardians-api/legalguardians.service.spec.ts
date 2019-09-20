import { TestBed } from '@angular/core/testing';

import { LegalguardiansService } from './legalguardians.service';

describe('LegalguardiansService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LegalguardiansService = TestBed.get(LegalguardiansService);
    expect(service).toBeTruthy();
  });
});
