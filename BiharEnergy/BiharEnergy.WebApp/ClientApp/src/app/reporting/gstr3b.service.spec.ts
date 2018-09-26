import { TestBed, inject } from '@angular/core/testing';

import { Gstr3bService } from './gstr3b.service';

describe('Gstr3bService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Gstr3bService]
    });
  });

  it('should be created', inject([Gstr3bService], (service: Gstr3bService) => {
    expect(service).toBeTruthy();
  }));
});
