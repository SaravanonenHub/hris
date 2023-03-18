import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { TableModule } from 'primeng/table'
import { InputTextModule } from 'primeng/inputtext'
import { ImageModule } from 'primeng/image'
import { FileUploadModule } from 'primeng/fileupload'
import { CalendarModule } from 'primeng/calendar'
import { RadioButtonModule } from 'primeng/radiobutton'
import { DropdownModule } from 'primeng/dropdown'
import { EmployeesRoutingModule } from './employees-routing.module';
import { EmployeesComponent } from './employees.component';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    EmployeesComponent,
    CreateEmployeeComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TableModule,
    InputTextModule,
    CalendarModule,
    RadioButtonModule,
    DropdownModule,
    ImageModule,
    FileUploadModule,
    EmployeesRoutingModule,
    SharedModule,
    FontAwesomeModule
  ]
})
export class EmployeesModule { }
