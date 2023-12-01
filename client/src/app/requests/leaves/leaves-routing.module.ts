import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeaveRequestsComponent } from './leave-requests.component';
import { CreateRequestsComponent } from './create-requests/create-requests.component';
import { LeaveDetailsComponent } from './leave-details/leave-details.component';

const routes: Routes = [{
  path: '', component: LeaveRequestsComponent
},
{path:'details/:id',component:LeaveDetailsComponent},
{ path: 'leave-add', component: CreateRequestsComponent },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeavesRoutingModule { }
