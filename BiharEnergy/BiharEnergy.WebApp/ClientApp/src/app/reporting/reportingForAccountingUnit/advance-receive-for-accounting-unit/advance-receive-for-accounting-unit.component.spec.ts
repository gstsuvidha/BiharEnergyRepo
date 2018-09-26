import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvanceReceiveForAccountingUnitComponent } from './advance-receive-for-accounting-unit.component';

describe('AdvanceReceiveForAccountingUnitComponent', () => {
  let component: AdvanceReceiveForAccountingUnitComponent;
  let fixture: ComponentFixture<AdvanceReceiveForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvanceReceiveForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvanceReceiveForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
