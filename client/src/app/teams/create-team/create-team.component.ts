import { Component, OnInit } from '@angular/core';
import { TeamService } from '../team.service';
import { ITeam, ITeamDetails, Role, TeamDetails } from 'src/app/domain/models/master';
import { IEmployee } from 'src/app/domain/models/employee';
import { first, map } from 'rxjs';
import { EmployeeService } from 'src/app/employees/employee.service';
import { EmployeeParams } from 'src/app/shared/models/employeeParams';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Message } from 'primeng/api';

@Component({
  selector: 'app-create-team',
  templateUrl: './create-team.component.html',
  styleUrls: ['./create-team.component.scss']
})
export class CreateTeamComponent implements OnInit {
  messages: Message[] | undefined;
  expanded:boolean=false;
  teams: ITeam[] = [];
  selectedTeam?: ITeam;
  manger!: IEmployee[];
  teamLeader!: IEmployee[];
  members!: IEmployee[];
  emptyRows!: IEmployee[];
  empParam: EmployeeParams = { code:"",search: "", status: "", nature: "", departmentIDs: [], role: Role.Manager };
  selectedEmployees!: IEmployee[];
  teamDetail: TeamDetails[] = [];
  formData = new FormData();
  teamForm = this.fb.group({
    teamName:['', [Validators.required]],
    departmentId:[0, [Validators.required]],
    displayName:'test',
    teamDetails: new FormControl<TeamDetails[]|null>(null)
  })
  constructor(private service: TeamService,
    private empService: EmployeeService, private fb:FormBuilder) { }
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
        console.log(data);
        this.teamLeader = data.filter(x => x.teamRole == "TeamLeader");
        this.members = data.filter(x => x.teamRole == "Member");

      })
    ).subscribe();

    
  }
  addManager(event: any) {
    let employee: IEmployee = event.value;
    if (this.teamDetail.length > 0) {
      this.teamDetail.splice(this.teamDetail.findIndex(x => x.roleName == Role.Manager), 1);
    }
    let manager: TeamDetails = {
      departmentId: employee.department!.id,
      roleName: Role.Manager,
      employeeId: employee.id!,
      sort: 1,
      displayName:employee.displayName!
    }
    this.teamDetail.push(manager);
    this.teamDetail.sort((n1, n2) => {
      if (n1.sort > n2.sort) { return 1; }
      else { return -1; }
    });
    console.log(this.teamDetail);
  }
  addTeamLeader(event: any) {
    let employee: IEmployee = event.value;
    if (this.teamDetail.length > 0) {
      if (this.teamDetail.find(x => x.roleName == Object.keys(Role)[Object.values(Role).indexOf('TeamLeader' as unknown as Role)]))
        this.teamDetail.splice(this.teamDetail.findIndex(x => x.roleName == Object.keys(Role)[Object.values(Role).indexOf('TeamLeader' as unknown as Role)]), 1);
    }
    let teamLeader: TeamDetails = {
      departmentId: employee.department!.id,
      roleName: Object.keys(Role)[Object.values(Role).indexOf('TeamLeader' as unknown as Role)],
      employeeId: employee.id!,
      sort: 2,
      displayName:employee.displayName!
    }
    this.teamDetail.push(teamLeader);
    this.teamDetail.sort((n1, n2) => {
      if (n1.sort > n2.sort) { return 1; }
      else { return -1; }
    });
    console.log(this.teamDetail);
  }
  addMember(event: any) {
    if(this.teamDetail.filter(x => x.roleName == Role.Member).length > 0)
    {
      //console.log(this.teamDetails.filter(x => x.role == Role.Manager).length);
      this.teamDetail.splice(this.teamDetail.findIndex(x => x.sort == 3));
    }
      
    if(this.selectedEmployees.length > 0)
    {
      let count:number =0;
      this.selectedEmployees.forEach(emp => {
        count++
        let teamLeader: TeamDetails = {
          departmentId: emp.department!.id,
          roleName: Role.Member,
          employeeId: emp.id!,
          sort: 2 + count,
          displayName:emp.displayName!
        }
        this.teamDetail.push(teamLeader);
      });
    }
    console.log(this.teamDetail);
    // this.teamForm.controls.empDetails.setValue(2)
    this.teamForm.controls.teamDetails.setValue(this.teamDetail);
  }
  get f() {
    return this.teamForm.controls;
  }
  onSubmit()
  {
    //console.log(this.teamForm);
    // this.submitted = true;
    
    let result = Object.assign({}, this.f);
    console.log(`Result: ${result}`);
    //this.employeeForm.get('birthDate')?.setValue(new Date(this.datepipe.transform(this.date, "dd/MM/yyyy")))
    Object.keys(this.f).forEach((key: any) => {
      const abstractControl = this.teamForm.get(key);
      console.log("Key:Value= "+key, abstractControl?.value)
      this.formData.append(key, abstractControl?.value);
    });
    for (var i = 0; i < this.teamDetail.length; i++) {
      this.formData.append('teamDetails[0].EmployeeId', this.teamDetail[i].employeeId.toString());
      this.formData.append('teamDetails[1].Role', this.teamDetail[i].roleName);
    }
    console.log(`Form Data: ${this.formData}`);
    this.service.create(this.teamForm.value)
      .pipe(first())
      .subscribe({
        next: () => {
           this.messages = [{ severity: 'success', summary: 'Success:', detail: 'Team information saved' }];
          // this.messageService.setEmitter(this.message);
          this.teamForm.reset();
          //this.alertService.successAlert('User saved');
          console.log("Success")
        },
        error: error => {
          // this.alertService.errorAlert('User saved');
          // this.message = [ { severity: 'error', summary: 'Error!', detail: error}],
          // this.messageService.setEmitter(this.message);
          console.log(error)
        }
      })

    // this.employee.
    // this.empService.postEmployee(this.employeeForm.value).subscribe((result) => {
    //   this.router.navigateByUrl('/profile/employee/overview');
    console.log(this.teamForm);
  }
  onFilter(event:any)
  {
    this.expanded = !this.expanded;
  }
}
