import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CdnurForAccountingUnitComponent } from './cdnur-for-accounting-unit.component';

describe('CdnurForAccountingUnitComponent', () => {
  let component: CdnurForAccountingUnitComponent;
  let fixture: ComponentFixture<CdnurForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CdnurForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CdnurForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
