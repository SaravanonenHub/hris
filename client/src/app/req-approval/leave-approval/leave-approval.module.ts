import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LeaveApprovalRoutingModule } from './leave-approval-routing.module';
import { LeaveApprovalComponent } from './leave-approval.component';

import { DropdownModule } from 'primeng/dropdown'
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { CalendarModule } from 'primeng/calendar';
import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [
    LeaveApprovalComponent
  ],
  imports: [
    CommonModule,
    LeaveApprovalRoutingModule,
    DropdownModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    CalendarModule,
    FileUploadModule,
    TableModule
  ]
})
export class LeaveApprovalModule { }
