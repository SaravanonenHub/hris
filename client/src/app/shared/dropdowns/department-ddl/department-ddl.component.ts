import { Component, Input, OnInit } from '@angular/core';
import { IDepartment } from 'src/app/domain/models/master';
import { SharedService } from '../../services/shared.service';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TRISTATECHECKBOX_VALUE_ACCESSOR } from 'primeng/tristatecheckbox';

@Component({
  selector: 'app-department-ddl',
  templateUrl: './department-ddl.component.html',
  styleUrls: ['./department-ddl.component.scss'],
  providers:[
    {
      provide:NG_VALUE_ACCESSOR,
      multi:true,
      useExisting:DepartmentDDLComponent
    }
  ]
})
export class DepartmentDDLComponent implements OnInit,ControlValueAccessor {
  
  departmentId =new FormControl();
  departments: IDepartment[] = [];
  constructor(private service:SharedService){}
  ngOnInit(): void {
    this.service.getDepartments().subscribe((data) => {
      this.departments = data;
    });
  }
  onChange:any = () => {};
  onTouched = ()=>{};
  // value = new FormControl();
  set value(v : number) {
    this.departmentId.setValue(v);
    this.onChange(v);
  }
  
  writeValue(Id: number): void {
    this.value = Id;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
