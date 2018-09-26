import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CndurComponent } from './cndur.component';

describe('CndurComponent', () => {
  let component: CndurComponent;
  let fixture: ComponentFixture<CndurComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CndurComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CndurComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
