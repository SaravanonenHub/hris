import { Component, OnInit } from '@angular/core';
import { ITeam, ITeamDetails } from '../domain/models/master';
import { TeamService } from './team.service';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  teams:ITeam[] = [];
  selectedTeam?: ITeam;
  selectedTeamDetails?:ITeamDetails[];
  constructor(private service:TeamService){}
  ngOnInit(): void {
    this.service.getTeams().subscribe((data) => {
      this.teams = data;
      this.selectedTeam = this.teams[0];
      if(this.selectedTeam != null)
      {
        this.service.getTeamDetailsById(this.selectedTeam.id).subscribe((details) => {
          this.selectedTeamDetails = details.teamDetails;
          console.log(details);
        })
      }
    })
  }

}
