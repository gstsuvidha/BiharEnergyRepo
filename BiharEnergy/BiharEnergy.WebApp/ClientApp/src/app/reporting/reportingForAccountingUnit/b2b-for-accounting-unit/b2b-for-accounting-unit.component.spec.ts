import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { B2bForAccountingUnitComponent } from './b2b-for-accounting-unit.component';

describe('B2bForAccountingUnitComponent', () => {
  let component: B2bForAccountingUnitComponent;
  let fixture: ComponentFixture<B2bForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ B2bForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(B2bForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
