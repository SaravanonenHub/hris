import { Injectable } from '@angular/core';
import { ITeam, ITeamwithDetails } from '../domain/models/master';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { IEmployee } from '../domain/models/employee';
import { EmployeeParams } from '../shared/models/employeeParams';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  baseUrl = environment.apiUrl;
  teams: ITeam[] = [];
  employees: IEmployee[] = [];
  constructor(private http: HttpClient) { }
  getTeams() {
    if (this.teams.length > 0) return of(this.teams);

    return this.http.get<ITeam[]>(this.baseUrl + 'Team/teams').pipe(
      map(data => this.teams = data)
    );
  }

  getUnassigned() {
    return this.http.get<IEmployee[]>(`${this.baseUrl}Employee/Unassigned`);
  }
  getTeamDetailsById(teamId: number) {
    return this.http.get<ITeamwithDetails>(`${this.baseUrl}Team / team / ${teamId}`);
  }
}
