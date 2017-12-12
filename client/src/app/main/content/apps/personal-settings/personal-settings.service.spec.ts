import { TestBed, inject } from '@angular/core/testing';

import { PersonalSettingsService } from './personal-settings.service';

describe('PersonalSettingsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PersonalSettingsService]
    });
  });

  it('should be created', inject([PersonalSettingsService], (service: PersonalSettingsService) => {
    expect(service).toBeTruthy();
  }));
});
