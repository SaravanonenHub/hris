import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee, IEmployee } from '../domain/models/employee';
import { IBranch, IDepartment, IDesignation, IDivision, ITeam, Role } from '../domain/models/master';
import { EmployeeParams } from '../shared/models/employeeParams';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl;
  braches: IBranch[] = [];
  divisions: IDivision[] = [];
  departments: IDepartment[] = [];
  designations: IDesignation[] = [];
  roles: Role[] = [];
  teams: ITeam[] = [];
  employees: IEmployee[] = [];
  filterParam!:EmployeeParams;
  constructor(private http: HttpClient) { }

  getEmployeesBase(param:EmployeeParams) {
    let params = new HttpParams();

    if (param.status !== "") {
      params = params.append('Status', param.status!);
    }

    if (param.nature !== "") {
      params = params.append('EmployeeNature', param.nature!.toString());
    }
    if(param.departmentIDs.length > 0){
      params = params.append('DepartmentId',param.departmentIDs.toString())
      // param.departmentIDs.forEach((val) => {
      //   params = params.append('DepartmentId',val)
      // })
    }
    return this.http.get<IEmployee[]>(this.baseUrl + 'Employee/employees',{params}).pipe(
      map(data => this.employees = data)
    );;
  }
  getEmployeesBaseById(id: number) {
    return this.http.get<Employee>(`${this.baseUrl}Employee/employee/${id}`);
  }
  getEmployeesBaseByCode(code: string) {
    return this.http.get<IEmployee>(`${this.baseUrl}Employee/employeebyCode/${code}`);
  }
  getBranches() {
    if (this.braches.length > 0) return of(this.braches);

    return this.http.get<IBranch[]>(this.baseUrl + 'Master/branches').pipe(
      map(branches => this.braches = branches)
    );
  }
  getDivisions() {
    if (this.divisions.length > 0) return of(this.divisions);

    return this.http.get<IDivision[]>(this.baseUrl + 'Master/divisions').pipe(
      map(data => this.divisions = data)
    );
  }
  getDepartments() {
    if (this.departments.length > 0) return of(this.departments);

    return this.http.get<IDepartment[]>(this.baseUrl + 'Master/departments').pipe(
      map(data => this.departments = data)
    );
  }
  getDesignations() {
    if (this.designations.length > 0) return of(this.designations);

    return this.http.get<IDesignation[]>(this.baseUrl + 'Master/designations').pipe(
      map(data => this.designations = data)
    );
  }
  getTeams() {
    if (this.teams.length > 0) return of(this.teams);

    return this.http.get<ITeam[]>(this.baseUrl + 'Team/teams').pipe(
      map(data => this.teams = data)
    );
  }
  getRoles() {
    // if (this.roles.length > 0) return of(this.roles);

    return this.http.get<Role[]>(this.baseUrl + 'products/types').pipe(
      map(data => this.roles = data)
    );
  }

  create(params: any) {
    console.log(params);
    return this.http.post(this.baseUrl + 'Employee/create', params);

  }
}
