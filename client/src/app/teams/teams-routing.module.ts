import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TeamListComponent } from './team-list.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { EditTeamComponent } from './edit-team/edit-team.component';

const routes: Routes = [
  { path: '', component: TeamListComponent },
  { path: 'create-team', component: CreateTeamComponent },
  { path: 'edit-team/:id', component: EditTeamComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TeamsRoutingModule {
  constructor() { }

}
