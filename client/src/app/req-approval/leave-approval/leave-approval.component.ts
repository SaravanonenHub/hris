import { Component } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { ILeave } from 'src/app/domain/models/leave';
import { EmployeeService } from 'src/app/employees/employee.service';
import { LeaveService } from 'src/app/requests/leaves/leave.service';

@Component({
  selector: 'app-leave-approval',
  templateUrl: './leave-approval.component.html',
  styleUrls: ['./leave-approval.component.scss']
})
export class LeaveApprovalComponent {
  leaveRequests: ILeave[] = []
  selectedRequest?: ILeave | null = null;
  constructor(private leaveService: LeaveService
    , private accountService: AccountService
    , private empService: EmployeeService) { }
  ngOnInit(): void {

    this.accountService.currentUser$.subscribe((emp) => {
      this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((employee) => {
        this.getPendingRequests(employee?.id)
      })
    });

  }
  async onEdit() {
    console.log(this.selectedRequest?.id!);

   
    this.leaveService.Actioncreate(this.selectedRequest?.id!).subscribe(() => {
      this.accountService.currentUser$.subscribe((emp) => {
        this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((employee) => {
          this.getPendingRequests(employee?.id)
        })
      });
    });
    // this.router.navigateByUrl(`/employee/personal-edit/${this.selectedRequest?.id}`);
  }
  getRequests(empId: number | undefined) {
    
    this.leaveService.getRequests(empId!).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
  getPendingRequests(empId: number | undefined) {
    
    this.leaveService.getPendingRequests(empId!).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
}
