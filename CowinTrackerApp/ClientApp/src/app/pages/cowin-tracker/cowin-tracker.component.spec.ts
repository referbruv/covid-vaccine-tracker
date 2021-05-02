import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CowinTrackerComponent } from './cowin-tracker.component';

describe('CowinTrackerComponent', () => {
  let component: CowinTrackerComponent;
  let fixture: ComponentFixture<CowinTrackerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CowinTrackerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CowinTrackerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
