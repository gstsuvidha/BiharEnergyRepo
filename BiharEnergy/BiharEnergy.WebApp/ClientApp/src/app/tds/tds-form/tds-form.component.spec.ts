import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TdsFormComponent } from './tds-form.component';

describe('TdsFormComponent', () => {
  let component: TdsFormComponent;
  let fixture: ComponentFixture<TdsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TdsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TdsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
