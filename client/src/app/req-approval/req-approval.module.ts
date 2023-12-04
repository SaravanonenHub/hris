import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReqApprovalRoutingModule } from './req-approval-routing.module';
import { MyOpenApprovalComponent } from './my-open-approval.component';
import { MyClosedApprovalComponent } from './my-closed-approval.component';
import { MyApprovalComponent } from './my-approval/my-approval.component';
import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { MyApprovalDetailComponent } from './my-approval-detail/my-approval-detail.component';
import { ActionsComponent } from './my-approval-detail/actions.component'



@NgModule({
  declarations: [
    MyOpenApprovalComponent,
    MyClosedApprovalComponent,
    MyApprovalComponent,
    MyApprovalDetailComponent,
    ActionsComponent
  ],
  imports: [
    CommonModule,
    ReqApprovalRoutingModule,
    AccordionModule,
    TableModule,
    CardModule,
    ButtonModule,
  ]
})
export class ReqApprovalModule { }
