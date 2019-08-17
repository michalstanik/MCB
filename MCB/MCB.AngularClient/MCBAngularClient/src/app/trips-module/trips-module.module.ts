import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// Components
import { TripsRootComponent } from './trips-root/trips-root.component';
import { TripsListComponent } from './trips-list/trips-list.component';
import { TripDetailsComponent } from './trip-details/trip-details.component';

// Modules
import { CoreModule } from '../core/core.module';
import { routing } from './trips-routing.module';
import { TripThumbnailComponent } from './trip-thumbnail/trip-thumbnail.component';
import { MDBBootstrapModulesPro } from 'ng-uikit-pro-standard';


@NgModule({
  declarations: [TripsRootComponent, TripsListComponent, TripThumbnailComponent, TripDetailsComponent],
  imports: [
    MDBBootstrapModulesPro.forRoot(),
    CommonModule,
    CoreModule,
    routing
  ]
})
export class TripsModuleModule { }
