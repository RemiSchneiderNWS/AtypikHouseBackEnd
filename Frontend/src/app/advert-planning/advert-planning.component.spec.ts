import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvertPlanningComponent } from './advert-planning.component';

describe('AdvertPlanningComponent', () => {
  let component: AdvertPlanningComponent;
  let fixture: ComponentFixture<AdvertPlanningComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvertPlanningComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvertPlanningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
