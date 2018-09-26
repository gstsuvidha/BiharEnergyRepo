import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CndrComponent } from './cndr.component';

describe('CndrComponent', () => {
  let component: CndrComponent;
  let fixture: ComponentFixture<CndrComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CndrComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CndrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
