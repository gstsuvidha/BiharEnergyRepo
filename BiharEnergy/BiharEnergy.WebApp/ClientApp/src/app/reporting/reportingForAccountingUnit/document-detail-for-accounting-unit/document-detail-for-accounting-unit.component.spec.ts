import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentDetailForAccountingUnitComponent } from './document-detail-for-accounting-unit.component';

describe('DocumentDetailForAccountingUnitComponent', () => {
  let component: DocumentDetailForAccountingUnitComponent;
  let fixture: ComponentFixture<DocumentDetailForAccountingUnitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DocumentDetailForAccountingUnitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentDetailForAccountingUnitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
