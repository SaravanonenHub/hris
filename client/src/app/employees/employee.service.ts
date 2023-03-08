import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBranch, IDepartment, IDesignation, IDivision, ITeam, Role } from '../domain/models/master';

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
  constructor(private http: HttpClient) { }

  getEmployeesBase() {
    return this.http.get(this.baseUrl + 'employees');
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
