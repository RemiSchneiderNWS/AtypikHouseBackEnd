import { TestBed } from '@angular/core/testing';

import { AdvertImagesService } from './advert-images.service';

describe('AdvertImagesService', () => {
  let service: AdvertImagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AdvertImagesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
