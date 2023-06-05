import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faCircleXmark, faPlusSquare } from '@fortawesome/free-regular-svg-icons';
import { IEmployee } from '../domain/models/employee';
import { EmployeeNature, IDivision, Status } from '../domain/models/master';
import { EmployeeService } from './employee.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  faCircleXmark = faCircleXmark; faPlusSquare = faPlusSquare;
  employees: IEmployee[] = [];
  natureOfEmployees = Object.values(EmployeeNature).map(key => ({ label: EmployeeNature[key], value: key }));;
  statuses = Object.keys(Status).map(key => ({ label: Status[key as keyof typeof Status], value: key }));;
  divisions: IDivision[] = [];
  selectedEmployee?: IEmployee | null = null;
  constructor(private employeeService: EmployeeService, private router: Router) { }
  ngOnInit(): void {
    // const statusEnum = Object.values(Status);
    // console.log(Status['NotWorking']);
    this.employeeService.getDivisions().subscribe({
      next: orders => this.divisions = orders
    })
    this.employeeService.getEmployeesBase().subscribe({
      next: employees => this.employees = employees
    })
  }

  onEdit() {
    console.log(this.selectedEmployee);
    this.router.navigateByUrl(`/employee/personal-edit/${this.selectedEmployee?.id}`);
  }
}
