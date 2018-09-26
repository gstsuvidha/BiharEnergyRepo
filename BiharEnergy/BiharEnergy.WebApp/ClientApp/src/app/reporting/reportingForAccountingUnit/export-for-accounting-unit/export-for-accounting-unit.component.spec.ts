import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportForAccountingUnitComponent } from './export-for-accounting-unit.component';

describe('ExportForAccountingUnitComponent', () => {
  let component: ExportForAccountingUnitComponent;
  let fixture: ComponentFixture<ExportForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExportForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExportForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
