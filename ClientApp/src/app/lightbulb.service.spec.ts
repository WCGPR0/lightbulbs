import { TestBed } from '@angular/core/testing';

import { LightbulbService } from './lightbulb.service';

describe('LightbulbService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LightbulbService = TestBed.get(LightbulbService);
    expect(service).toBeTruthy();
  });
});
