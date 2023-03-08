import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppMainComponent } from './app.main.component';
import { TestOneComponent } from './test-one/test-one.component';
import { TestComponent } from './test/test.component';

const routes: Routes = [{
  path: '', component: AppMainComponent
  , children: [{ path: 'employee', loadChildren: () => import('./employees/employees-routing.module').then(m => m.EmployeesRoutingModule) }]
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
