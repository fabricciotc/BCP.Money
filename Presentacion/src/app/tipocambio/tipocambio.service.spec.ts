import { TestBed } from '@angular/core/testing';

import { TipocambioService } from './tipocambio.service';

describe('TipocambioService', () => {
  let service: TipocambioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TipocambioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
