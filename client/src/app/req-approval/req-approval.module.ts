import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReqApprovalRoutingModule } from './req-approval-routing.module';
import { MyOpenApprovalComponent } from './my-open-approval.component';
import { MyClosedApprovalComponent } from './my-closed-approval.component';
import { MyApprovalComponent } from './my-approval/my-approval.component';


@NgModule({
  declarations: [
    MyOpenApprovalComponent,
    MyClosedApprovalComponent,
    MyApprovalComponent
  ],
  imports: [
    CommonModule,
    ReqApprovalRoutingModule
  ]
})
export class ReqApprovalModule { }
