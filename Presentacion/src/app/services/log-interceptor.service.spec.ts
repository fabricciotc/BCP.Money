import { TestBed } from '@angular/core/testing';

import { LogInterceptorService } from './log-interceptor.service';

describe('LogInterceptorService', () => {
  let service: LogInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogInterceptorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
