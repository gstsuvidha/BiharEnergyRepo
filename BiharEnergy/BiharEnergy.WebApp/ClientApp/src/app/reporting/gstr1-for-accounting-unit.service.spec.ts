import { TestBed, inject } from '@angular/core/testing';

import { Gstr1ForAccountingUnitService } from './gstr1-for-accounting-unit.service';

describe('Gstr1ForAccountingUnitService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Gstr1ForAccountingUnitService]
    });
  });

  it('should be created', inject([Gstr1ForAccountingUnitService], (service: Gstr1ForAccountingUnitService) => {
    expect(service).toBeTruthy();
  }));
});
