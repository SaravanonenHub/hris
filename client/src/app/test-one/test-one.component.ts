import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { first } from 'rxjs';
import { EmpContentType, EmployeeNature, Gender, IBranch, IDepartment, IDesignation, IDivision, ITeam, MartialStatus, Role } from '../domain/models/master';
import { faCircleXmark, faPlusSquare } from '@fortawesome/free-regular-svg-icons'
import { EmployeeService } from '../employees/employee.service';
@Component({
  selector: 'app-test-one',
  templateUrl: './test-one.component.html',
  styleUrls: ['./test-one.component.scss']
})
export class TestOneComponent implements OnInit {
  faCircleXmark = faCircleXmark;
  gender: string | undefined;
  contentTypeDDL: string[] = [];
  branches: IBranch[] = [];
  divisions: IDivision[] = [];
  departments: IDepartment[] = [];
  designations: IDesignation[] = [];
  teams: ITeam[] = [];
  genders = Object.values(Gender).map(key => ({ label: Gender[key], value: key }));;
  empContentTab = Object.values(EmpContentType).map(key => ({ label: EmpContentType[key], value: key }));;
  martialStatuses = Object.values(MartialStatus).map(key => ({ label: MartialStatus[key], value: key }));
  natureOfEmployees = Object.values(EmployeeNature).map(key => ({ label: EmployeeNature[key], value: key }));;
  roles = Object.values(Role).map(key => ({ label: key, value: Object.values(Role).indexOf(key) }));
  selectedDepartment: any = null;
  employee: any = { Employee: {} };
  employeeForm!: FormGroup;
  constructor(private formBuilder: FormBuilder, private empService: EmployeeService) { }
  ngOnInit(): void {
    this.employeeForm = this.formBuilder.group({
      employeeCode: [''],
      firstName: [''],
      lastName: [''],
      displayName: [''],
      imagePath: [''],
      branchId: [0],
      divisionId: [0],
      departmentId: [0],
      designationId: [0],
      qualification: [''],
      status: ['Live'],
      birthDate: ['2023-02-21T13:24:17.899Z'],
      age: [0],
      joinDate: ['2023-02-21T13:24:17.899Z'],
      emailId: [''],
      gender: [''],
      bloodGroup: [''],
      martialStatus: [''],
      employeeNature: [''],
      optionalSaturday: ['Y'],
      teamId: [0],
      teamRoleId: [0],
    })

    this.getDropdowns();
    this.employeeForm.valueChanges.subscribe(() => {
      this.employeeForm.get('displayName')?.setValue(this.referencePublicacionValues, { emitEvent: false })
    })


  }

  get f() {
    return this.employeeForm;
  }
  get referencePublicacionValues(): string {

    if ((this.employeeForm.get('firstName')?.value === '' || this.employeeForm.get('firstName')?.value === undefined)
      || (this.employeeForm.get('lastName')?.value === '' || this.employeeForm.get('lastName')?.value === undefined))
      return '';

    return `${this.employeeForm.get('firstName')?.value} , ${this.employeeForm.get('lastName')?.value}`

  }
  getDropdowns() {
    this.empService.getBranches().subscribe({
      next: orders => this.branches = orders
    })
    this.empService.getDivisions().subscribe({
      next: orders => this.divisions = orders
    })
    this.empService.getDepartments().subscribe({
      next: orders => this.departments = orders
      // next: orders => console.log(orders)
    })
    this.empService.getDesignations().subscribe({
      next: orders => this.designations = orders
    })
    this.empService.getTeams().subscribe({
      next: orders => this.teams = orders
    })
  }

  onSubmit() {
    this.empService.create(this.employeeForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
          console.log("Success")
        },
        error: error => {
          console.log("Error")
        }
      })
    // this.employee.
    // this.empService.postEmployee(this.employeeForm.value).subscribe((result) => {
    //   this.router.navigateByUrl('/profile/employee/overview');
    // console.log(this.employeeForm.value);
  }
}
