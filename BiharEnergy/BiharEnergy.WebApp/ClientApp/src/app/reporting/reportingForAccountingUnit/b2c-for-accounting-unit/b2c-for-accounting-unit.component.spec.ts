import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { B2cForAccountingUnitComponent } from './b2c-for-accounting-unit.component';

describe('B2cForAccountingUnitComponent', () => {
  let component: B2cForAccountingUnitComponent;
  let fixture: ComponentFixture<B2cForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ B2cForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(B2cForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
