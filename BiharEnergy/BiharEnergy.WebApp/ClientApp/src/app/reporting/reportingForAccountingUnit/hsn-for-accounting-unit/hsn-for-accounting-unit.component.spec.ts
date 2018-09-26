import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsnForAccountingUnitComponent } from './hsn-for-accounting-unit.component';

describe('HsnForAccountingUnitComponent', () => {
  let component: HsnForAccountingUnitComponent;
  let fixture: ComponentFixture<HsnForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsnForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsnForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
