import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppMainComponent } from './app.main.component';
import { AuthGuard } from './core/guard/auth.guard';


const routes: Routes = [{
  path: '', component: AppMainComponent, data: { breadcrumb: 'Home' }
  , children: [
    {
      path: 'employee'
      , canActivate: [AuthGuard]
      , loadChildren: () => import('./employees/employees.module').then(m => m.EmployeesModule)
    },
    {
      path: 'request'
      , canActivate: [AuthGuard]
      , loadChildren: () => import('./requests/requests.module').then(m => m.RequestsModule)
    },
    {
      path: 'approval'
      , canActivate: [AuthGuard]
      , loadChildren: () => import('./req-approval/req-approval.module').then(m => m.ReqApprovalModule)
    },
    {
      path:'teams',loadChildren: () => import('./teams/teams.module').then(m => m.TeamsModule)
    },
    { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) }
  ]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
