import { Component, OnInit } from '@angular/core';
import { ITeam, ITeamDetails, ITeamwithDetails } from '../domain/models/master';
import { TeamService } from './team.service';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRetryPolicy } from '@microsoft/signalr';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  teams: ITeam[] = [];
  selectedTeam?: ITeam;
  selectedTeamDetails?: ITeamDetails[];
  imageUrl = environment.filesGetUrl;
  constructor(private service: TeamService) { }
  ngOnInit(): void {
    this.service.getTeams().subscribe((data) => {
      this.teams = data;
      console.log(this.teams);
      this.selectedTeam = this.teams[3];
      if (this.selectedTeam != null) {
        this.service.getTeamDetailsById(this.selectedTeam.id).subscribe((details) => {
          this.selectedTeamDetails = details.teamDetails
          // this.selectedTeamDetails.forEach(element => {
          //   let pathStr: string[] = element.employee.imagePath?.split("\\")!;
          //   let path: string = pathStr[pathStr.length - 1];
          //   element.employee.imagePath = `${this.imageUrl}${path}`
          //   console.log(element.employee.imagePath);

          // });
        })
      }

    })
  }
  onTeamSelect(event:any)
  {
    console.log(event.data);
    let team:ITeamwithDetails[] = event.data;
    this.selectedTeamDetails = event.data.teamDetails;
    // this.selectedTeamDetails?.forEach(element => {
    //   let pathStr: string[] = element.employee.imagePath?.split("\\")!;
    //   let path: string = pathStr[pathStr.length - 1];
    //   element.employee.imagePath = `${this.imageUrl}${path}`
    //   console.log(`Path string : ${pathStr}`);
    //   console.log(`Path: ${path}`);
    //   console.log(`Image Path : ${element.employee.imagePath}`);

    // });
  }
}
