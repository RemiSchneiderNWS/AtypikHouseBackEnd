import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsAnnouncementComponent } from './details-announcement.component';

describe('DetailsAnnouncementComponent', () => {
  let component: DetailsAnnouncementComponent;
  let fixture: ComponentFixture<DetailsAnnouncementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsAnnouncementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsAnnouncementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
