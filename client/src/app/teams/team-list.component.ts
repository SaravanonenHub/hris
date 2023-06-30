import { Component, OnInit } from '@angular/core';
import { ITeam, ITeamDetails } from '../domain/models/master';
import { TeamService } from './team.service';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  teams: ITeam[] = [];
  selectedTeam?: ITeam;
  selectedTeamDetails?: ITeamDetails[];
  imageUrl = environment.filesUrl;
  constructor(private service: TeamService) { }
  ngOnInit(): void {
    this.service.getTeams().subscribe((data) => {
      this.teams = data;
      console.log(this.teams);
      this.selectedTeam = this.teams[0];
      if (this.selectedTeam != null) {
        this.service.getTeamDetailsById(this.selectedTeam.id).subscribe((details) => {
          this.selectedTeamDetails = details.teamDetails
          this.selectedTeamDetails.forEach(element => {
            let pathStr: string[] = element.employee.imagePath?.split("\\")!;
            let path: string = pathStr[pathStr.length - 1];
            element.employee.imagePath = `${this.imageUrl}${path}`
            console.log(element.employee.imagePath);

          });
        })
      }

    })
  }

}
