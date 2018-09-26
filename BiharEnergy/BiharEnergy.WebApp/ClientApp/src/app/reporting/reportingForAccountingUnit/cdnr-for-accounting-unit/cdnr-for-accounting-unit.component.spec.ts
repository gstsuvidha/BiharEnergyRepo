import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CdnrForAccountingUnitComponent } from './cdnr-for-accounting-unit.component';

describe('CdnrForAccountingUnitComponent', () => {
  let component: CdnrForAccountingUnitComponent;
  let fixture: ComponentFixture<CdnrForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CdnrForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CdnrForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
