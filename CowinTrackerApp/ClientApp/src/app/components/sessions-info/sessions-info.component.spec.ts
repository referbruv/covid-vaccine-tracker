import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SessionsInfoComponent } from './sessions-info.component';

describe('SessionsInfoComponent', () => {
  let component: SessionsInfoComponent;
  let fixture: ComponentFixture<SessionsInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SessionsInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SessionsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
