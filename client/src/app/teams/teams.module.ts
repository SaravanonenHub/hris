import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table'
import { DropdownModule } from 'primeng/dropdown'
import { AvatarModule } from 'primeng/avatar'
import { ButtonModule } from 'primeng/button'
import { OverlayPanelModule } from 'primeng/overlaypanel'
import { AvatarGroupModule } from 'primeng/avatargroup'
import { DataViewModule } from 'primeng/dataview'
import { TeamsRoutingModule } from './teams-routing.module';
import { TeamListComponent } from './team-list.component';
import { CreateTeamComponent } from './create-team/create-team.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    TeamListComponent,
    CreateTeamComponent
  ],
  imports: [
    CommonModule,
    TeamsRoutingModule,
    TableModule,
    DataViewModule,
    AvatarModule,
    AvatarGroupModule,
    SharedModule,
    ButtonModule,
    OverlayPanelModule,
    DropdownModule
  ]
})
export class TeamsModule { }
