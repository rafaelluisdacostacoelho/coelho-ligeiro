import { NgModule } from '@angular/core';
import { SharedModule } from '../../../../core/modules/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { FusePersonalSettingsComponent } from './personal-settings.component';
import { PersonalSettingsService } from './personal-settings.service';

const routes: Routes = [
  {
    path: 'personal-settings',
    component: FusePersonalSettingsComponent,
    resolve: {
      personalSettings: PersonalSettingsService
    }
  }
];

@NgModule({
  declarations: [
    FusePersonalSettingsComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    PersonalSettingsService
  ]
})
export class FusePersonalSettingsModule
{
}
