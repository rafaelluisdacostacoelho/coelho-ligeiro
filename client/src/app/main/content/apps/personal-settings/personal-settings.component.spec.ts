import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FusePersonalSettingsComponent } from './personal-settings.component';

describe('PersonalSettingsComponent', () => {
  let component: FusePersonalSettingsComponent;
  let fixture: ComponentFixture<FusePersonalSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FusePersonalSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FusePersonalSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
