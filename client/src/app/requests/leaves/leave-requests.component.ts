import { Component, OnInit } from '@angular/core';
import { ILeave, ILeaveEntitlement } from 'src/app/domain/models/leave';
import { LeaveService } from './leave.service';
import { AccountService } from 'src/app/account/account.service';
import { EmployeeService } from 'src/app/employees/employee.service';
import { leavePolicyParams } from 'src/app/shared/models/leavePolicyParams';

@Component({
  selector: 'app-leave-requests',
  templateUrl: './leave-requests.component.html',
  styleUrls: ['./leave-requests.component.scss']
})
export class LeaveRequestsComponent implements OnInit {
  leaveRequests: ILeave[] = []
  entitlement?:ILeaveEntitlement;
  selectedRequest?: ILeave | null = null;
  leavePolicyParam: leavePolicyParams ={};
  customToggleIcon:string="pi pi-angle-down";
  isExpanded:boolean=true;
  constructor(private leaveService: LeaveService
    , private accountService: AccountService
    , private empService: EmployeeService) { }
  ngOnInit(): void {

    this.accountService.currentUser$.subscribe((emp) => {
      this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((employee) => {
        this.getRequests(employee?.id);
        this.getEntitlement(employee.id!);
        
      })
    });

  }
  onBeforeToggle(){
    debugger;
    this.isExpanded = !this.isExpanded;
    this.customToggleIcon = this.isExpanded ? 'pi pi-angle-up':'pi pi-angle-down'
  }
  onEdit() {
    console.log(this.selectedRequest);

    // this.router.navigateByUrl(`/employee/personal-edit/${this.selectedRequest?.id}`);
  }
  getEntitlement(empId: number) {
    this.leavePolicyParam.empId = empId;
    this.leavePolicyParam.leaveType="";
    this.leavePolicyParam.policyName="";
    this.leaveService.getEntitlement(this.leavePolicyParam!).subscribe({
      next: requests => this.entitlement = requests
    })
  }
  getRequests(empId: number | undefined) {
    this.leaveService.getRequests(empId!).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
}
