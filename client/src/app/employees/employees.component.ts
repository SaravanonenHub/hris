import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faCircleXmark, faPlusSquare } from '@fortawesome/free-regular-svg-icons';
import { IEmployee } from '../domain/models/employee';
import { EmployeeNature, IDivision, Status } from '../domain/models/master';
import { EmployeeService } from './employee.service';
import { AlertService } from '../shared/services/alertService';
import { Message } from 'primeng/api'
import { PgmessageService } from '../shared/services/pgmessage.service';
import { EmployeeParams } from '../shared/models/employeeParams';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  empParam: EmployeeParams = {code:"", search: "", status: "", nature: "", departmentIDs: [], role: "" };
  faCircleXmark = faCircleXmark; faPlusSquare = faPlusSquare;
  employees: IEmployee[] = [];
  natureOfEmployees = Object.values(EmployeeNature).map(key => ({ label: EmployeeNature[key], value: key }));;
  statuses = Object.keys(Status).map(key => ({ label: Status[key as keyof typeof Status], value: key }));;
  divisions: IDivision[] = [];
  selectedEmployee?: IEmployee | null = null;
  constructor(private employeeService: EmployeeService, private router: Router
    , private alertService: AlertService, private messageService: PgmessageService) { }
  ngOnInit(): void {
    // const statusEnum = Object.values(Status);
    // console.log(Status['NotWorking']);
    this.employeeService.getDivisions().subscribe({
      next: orders => this.divisions = orders
    });
    // console.log(this.empParam);
    this.getEmployee(this.empParam);

  }

  getEmployee(param: EmployeeParams) {
    console.log(param);
    this.employeeService.getEmployeesBase(param).subscribe({
      next: employees => {
        this.employees = employees;

        // this.alertService.successAlert("Records Received");

        // console.log(this.employees);
      }

    })
  }
  getSeverity(status: string): any {
    switch (status) {
      case 'Live':
        return 'success';
      case 'Not Working':
        return 'danger';
    }
  }
  onEdit() {
    console.log(this.selectedEmployee);
    this.router.navigateByUrl(`/employee/personal-edit/${this.selectedEmployee?.id}`);
  }
  onStatusChange(event: any) {
    this.empParam.status = "";
    // console.log(`${event.value}: ${Object.values(Status)[Object.keys(Status).indexOf(event.value as unknown as Status)]}`)
    if (event.value != null) {
      this.empParam.status = Object.values(Status)[Object.keys(Status).indexOf(event.value as unknown as Status)];

    }
    this.getEmployee(this.empParam);
    // console.log(event);
  }
  onNatureChange(event: any) {
    this.empParam.nature = "";
    if (event.value != null) {
      this.empParam.nature = Object.values(EmployeeNature)[Object.keys(EmployeeNature).indexOf(event.value as unknown as Status)];

    }
    this.getEmployee(this.empParam);
  }
  onDepartmentChange(event: any) {
    const values: IDivision[] = event.value
    let departmentIds: string = "";
    const queryParams = values.map(val => val.id);
    // console.log(`Query Params:${queryParams}`)
    // if(values.length > 0)
    // {
    //   values.forEach(val => {
    //     if(departmentIds != "") {
    //       departmentIds = `${departmentIds},${val.id}`
    //     }
    //     else{departmentIds = `${val.id}`}

    //   })
    // }
    // console.log(queryParams);
    this.empParam.departmentIDs = queryParams;
    this.getEmployee(this.empParam);
  }
}
