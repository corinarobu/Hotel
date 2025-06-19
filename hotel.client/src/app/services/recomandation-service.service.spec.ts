import { TestBed } from '@angular/core/testing';

import { RecomandationServiceService } from './recomandation-service.service';

describe('RecomandationServiceService', () => {
  let service: RecomandationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecomandationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
