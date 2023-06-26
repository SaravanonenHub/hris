import { Injectable } from '@angular/core';
import { ITeam, ITeamwithDetails } from '../domain/models/master';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TeamService {
  baseUrl = environment.apiUrl;
  teams: ITeam[] = [];
  constructor(private http: HttpClient) { }
  getTeams() {
    if (this.teams.length > 0) return of(this.teams);

    return this.http.get<ITeam[]>(this.baseUrl + 'Team/teams').pipe(
      map(data => this.teams = data)
    );
  }
  getTeamDetailsById(teamId: number) {
    return this.http.get<ITeamwithDetails>(`${this.baseUrl}Team/team/${teamId}`);
  }
}
