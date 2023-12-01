import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/guard/auth.guard';
import { MyApprovalComponent } from './my-approval/my-approval.component';
import { MyApprovalDetailComponent } from './my-approval-detail/my-approval-detail.component';

const routes: Routes = [
  {path:'',component:MyApprovalComponent},
  {path:'approve',component:MyApprovalDetailComponent},
  {
    path: 'Details'
    , canActivate: [AuthGuard]
    , loadChildren: () => import('./leave-approval/leave-approval.module').then(m => m.LeaveApprovalModule)
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReqApprovalRoutingModule { }
