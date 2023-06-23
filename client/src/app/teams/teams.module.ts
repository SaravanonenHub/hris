import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {TableModule} from 'primeng/table'
import {DataViewModule} from 'primeng/dataview'
import { TeamsRoutingModule } from './teams-routing.module';
import { TeamListComponent } from './team-list.component';
import { CreateTeamComponent } from './create-team/create-team.component';


@NgModule({
  declarations: [
    TeamListComponent,
    CreateTeamComponent
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    TableModule,
    DataViewModule
  ]
})
export class TeamsModule { }
