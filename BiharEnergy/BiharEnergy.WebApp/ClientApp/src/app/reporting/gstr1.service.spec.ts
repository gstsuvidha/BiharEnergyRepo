import { TestBed, inject } from '@angular/core/testing';

import { Gstr1Service } from './gstr1.service';

describe('Gstr1Service', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Gstr1Service]
    });
  });

  it('should be created', inject([Gstr1Service], (service: Gstr1Service) => {
    expect(service).toBeTruthy();
  }));
});
