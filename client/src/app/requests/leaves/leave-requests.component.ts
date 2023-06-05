import { Component, OnInit } from '@angular/core';
import { ILeave } from 'src/app/domain/models/leave';
import { LeaveService } from './leave.service';
import { AccountService } from 'src/app/account/account.service';
import { EmployeeService } from 'src/app/employees/employee.service';

@Component({
  selector: 'app-leave-requests',
  templateUrl: './leave-requests.component.html',
  styleUrls: ['./leave-requests.component.scss']
})
export class LeaveRequestsComponent implements OnInit {
  leaveRequests: ILeave[] = []
  selectedRequest?: ILeave | null = null;
  constructor(private leaveService: LeaveService
    , private accountService: AccountService
    , private empService: EmployeeService) { }
  ngOnInit(): void {

    this.accountService.currentUser$.subscribe((emp) => {
      this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((employee) => {
        this.getRequests(employee?.id)
      })
    });

  }
  onEdit() {
    console.log(this.selectedRequest);

    // this.router.navigateByUrl(`/employee/personal-edit/${this.selectedRequest?.id}`);
  }
  getRequests(empId: number | undefined) {
    this.leaveService.getRequests(empId!).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
}
