import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Gstr1FinalReportComponent } from './gstr1-final-report.component';

describe('Gstr1FinalReportComponent', () => {
  let component: Gstr1FinalReportComponent;
  let fixture: ComponentFixture<Gstr1FinalReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Gstr1FinalReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Gstr1FinalReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
