import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown'
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { LeavesRoutingModule } from './leaves-routing.module';
import { LeaveRequestsComponent } from './leave-requests.component';
import { CreateRequestsComponent } from './create-requests/create-requests.component';
import { CalendarModule } from 'primeng/calendar';
import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';
import { FieldsetModule } from 'primeng/fieldset';
@NgModule({
  declarations: [
    LeaveRequestsComponent,
    CreateRequestsComponent,

  ],
  imports: [
    CommonModule,
    LeavesRoutingModule,
    DropdownModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    CalendarModule,
    FileUploadModule,
    TableModule,
    FieldsetModule  
  ]
})
export class LeavesModule { }
