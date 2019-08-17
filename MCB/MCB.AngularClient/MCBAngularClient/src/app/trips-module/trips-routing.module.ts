import { RouterModule, Routes } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

// Components
import { TripsRootComponent } from './trips-root/trips-root.component';


export const routing: ModuleWithProviders = RouterModule.forChild([
  {
    path: 'trips', /*canActivate: [AuthGuardService],*/ component: TripsRootComponent,
 
    children: [

      ]
    }
]);
