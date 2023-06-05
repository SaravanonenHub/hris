import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../core/guard/auth.guard';

const routes: Routes = [
  {
    path: ''
    , canActivate: [AuthGuard]
    , loadChildren: () => import('./leave-approval/leave-approval.module').then(m => m.LeaveApprovalModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReqApprovalRoutingModule { }
