import { Component, Input, OnInit } from '@angular/core';
import { IDepartment } from 'src/app/domain/models/master';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-department-ddl',
  templateUrl: './department-ddl.component.html',
  styleUrls: ['./department-ddl.component.scss']
})
export class DepartmentDDLComponent implements OnInit {
  @Input() controlName?: string;
  departments: IDepartment[] = [];
  constructor(private service:SharedService){}
  ngOnInit(): void {
    this.service.getDepartments().subscribe((data) => {
      this.departments = data;
    });
  }
  
}
