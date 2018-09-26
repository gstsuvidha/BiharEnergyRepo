import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Gstr3bForAccountingComponent } from './gstr3b-for-accounting.component';

describe('Gstr3bForAccountingComponent', () => {
  let component: Gstr3bForAccountingComponent;
  let fixture: ComponentFixture<Gstr3bForAccountingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Gstr3bForAccountingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Gstr3bForAccountingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
