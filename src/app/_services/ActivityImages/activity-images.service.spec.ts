import { TestBed } from '@angular/core/testing';

import { ActivityImagesService } from './activity-images.service';

describe('ActivityImagesService', () => {
  let service: ActivityImagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActivityImagesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
