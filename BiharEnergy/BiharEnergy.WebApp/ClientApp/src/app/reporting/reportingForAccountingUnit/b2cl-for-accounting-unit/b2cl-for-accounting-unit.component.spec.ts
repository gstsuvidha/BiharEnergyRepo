import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { B2clForAccountingUnitComponent } from './b2cl-for-accounting-unit.component';

describe('B2clForAccountingUnitComponent', () => {
  let component: B2clForAccountingUnitComponent;
  let fixture: ComponentFixture<B2clForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ B2clForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(B2clForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
