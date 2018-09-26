/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { Gstr3bForAccountingService } from './gstr3bForAccounting.service';

describe('Service: Gstr3bForAccounting', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Gstr3bForAccountingService]
    });
  });

  it('should ...', inject([Gstr3bForAccountingService], (service: Gstr3bForAccountingService) => {
    expect(service).toBeTruthy();
  }));
});
