import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule,ReactiveFormsModule} from '@angular/forms'
import { AlertComponent } from './alert/alert.component';
import {DropdownModule} from 'primeng/dropdown'
import { DepartmentDDLComponent } from './dropdowns/department-ddl/department-ddl.component';
import { DesignationDDLComponent } from './dropdowns/designation-ddl/designation-ddl.component';
import { DivisionDDLComponent } from './dropdowns/division-ddl/division-ddl.component';



@NgModule({
  declarations: [
    AlertComponent,
    DepartmentDDLComponent,
    DesignationDDLComponent,
    DivisionDDLComponent
  ],
  imports: [
    CommonModule,
    DropdownModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    AlertComponent,
    DepartmentDDLComponent
  ]
})
export class SharedModule { }
