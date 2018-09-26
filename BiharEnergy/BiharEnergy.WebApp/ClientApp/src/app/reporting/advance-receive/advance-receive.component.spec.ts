import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvanceReceiveComponent } from './advance-receive.component';

describe('AdvanceReceiveComponent', () => {
  let component: AdvanceReceiveComponent;
  let fixture: ComponentFixture<AdvanceReceiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvanceReceiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvanceReceiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
