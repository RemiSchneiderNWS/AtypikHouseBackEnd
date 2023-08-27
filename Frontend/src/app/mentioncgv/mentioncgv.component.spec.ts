import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MentioncgvComponent } from './mentioncgv.component';

describe('MentioncgvComponent', () => {
  let component: MentioncgvComponent;
  let fixture: ComponentFixture<MentioncgvComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MentioncgvComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MentioncgvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
