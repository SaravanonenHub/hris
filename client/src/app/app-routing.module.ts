import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppMainComponent } from './app.main.component';


const routes: Routes = [{
  path: '', component: AppMainComponent, data: { breadcrumb: 'Home' }
  , children: [
    { path: 'employee', loadChildren: () => import('./employees/employees.module').then(m => m.EmployeesModule) },
    { path: 'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) }
  ]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
