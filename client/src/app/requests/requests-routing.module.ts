import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/guard/auth.guard';
import { MyRequestsComponent } from './my-requests.component';
import { RequestServicesComponent } from './request-services.component';
import { RequestDetailsComponent } from './request-details/request-details.component';

const routes: Routes = [
  {
    path:'',component:MyRequestsComponent
  },
  {
    path:'details/:id',component:RequestDetailsComponent
  },
  {path:'services',component:RequestServicesComponent},
  {
    path: 'services/leave'
    , canActivate: [AuthGuard]
    , loadChildren: () => import('./leaves/leaves.module').then(m => m.LeavesModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequestsRoutingModule { }
