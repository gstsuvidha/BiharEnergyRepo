import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TdsListComponent } from './tds-list.component';

describe('TdsListComponent', () => {
  let component: TdsListComponent;
  let fixture: ComponentFixture<TdsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TdsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TdsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
