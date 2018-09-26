import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountingUnitComponent } from './accounting-unit.component';

describe('AccountingUnitComponent', () => {
  let component: AccountingUnitComponent;
  let fixture: ComponentFixture<AccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
