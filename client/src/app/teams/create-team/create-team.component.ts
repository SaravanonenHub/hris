import { Component, OnInit } from '@angular/core';
import { TeamService } from '../team.service';
import { ITeam, ITeamDetails, Role, TeamDetails } from 'src/app/domain/models/master';
import { IEmployee } from 'src/app/domain/models/employee';
import { map } from 'rxjs';
import { EmployeeService } from 'src/app/employees/employee.service';
import { EmployeeParams } from 'src/app/shared/models/employeeParams';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.scss']
})
export class CreateTeamComponent implements OnInit {
  teams: ITeam[] = [];
  selectedTeam?: ITeam;
  manger!: IEmployee[];
  teamLeader!: IEmployee[];
  members!: IEmployee[];
  empParam: EmployeeParams = { search: "", status: "", nature: "", departmentIDs: [], role: Role.Manager };
  selectedEmployees!: IEmployee[];
  teamDetails: TeamDetails[] = [];
  constructor(private service: TeamService,
    private empService: EmployeeService) { }
  ngOnInit(): void {
    this.service.getTeams().subscribe((data) => {
      this.teams = data;
    });
    this.empService.getEmployeesBase(this.empParam).subscribe({
      next: employees => {
        this.manger = employees;
      }
    })

    this.service.getUnassigned().pipe(
      map(data => {
        this.teamLeader = data.filter(x => x.teamRole == "Team Leader");
        this.members = data.filter(x => x.teamRole == "Member");

      })
    ).subscribe();
  }
  addManager(event: any) {
    let employee: IEmployee = event.value;
    if (this.teamDetails.length > 0) {
      this.teamDetails.splice(this.teamDetails.findIndex(x => x.role == Role.Manager), 1);
    }
    let manager: TeamDetails = {
      departmentId: employee.department!.id,
      role: Role.Manager,
      employeeId: employee.id!,
      sort: 1
    }
    this.teamDetails.push(manager);
    this.teamDetails.sort((n1, n2) => {
      if (n1.sort > n2.sort) { return 1; }
      else { return -1; }
    });
    console.log(this.teamDetails);
  }
  addTeamLeader(event: any) {
    let employee: IEmployee = event.value;
    if (this.teamDetails.length > 0) {
      if (this.teamDetails.find(x => x.role == Role.TeamLeader))
        this.teamDetails.splice(this.teamDetails.findIndex(x => x.role == Role.TeamLeader), 1);
    }
    let teamLeader: TeamDetails = {
      departmentId: employee.department!.id,
      role: Role.TeamLeader,
      employeeId: employee.id!,
      sort: 2
    }
    this.teamDetails.push(teamLeader);
    this.teamDetails.sort((n1, n2) => {
      if (n1.sort > n2.sort) { return 1; }
      else { return -1; }
    });
    console.log(this.teamDetails);
  }
  addMember(event: any) {

    console.log(event);
    console.log(this.selectedEmployees);
  }
}
