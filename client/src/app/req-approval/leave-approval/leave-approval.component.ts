import { Component } from '@angular/core';
import { lastValueFrom, map } from 'rxjs';
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
  selectedRequest!: ILeave;
  multipleSelectedRequest!:ILeave[];
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
    
    this.leaveService.getPendingRequests(empId!).pipe(
      map((lev) => lev.map((val) => {
        const prefix = 'RQ';
        const id = val.id;
        const numericLength = 10;
        const numberString = id.toString();
        const zeroNeeded = numericLength - numberString.length;
        val.idStr = prefix + '0'.repeat(zeroNeeded) + numberString;
        const fromdate = new Date(val.fromDate);
        const todate = new Date(val.toDate);
        const fyear = fromdate.getFullYear().toString();
        const fmonth = (fromdate.getMonth() + 1).toString().padStart(2,'0');
        const fdate = fromdate.getDate().toString().padStart(2,'0');
        val.fDate = `${fyear}/${fmonth}/${fdate}`;
        val.tDate = `${todate.getFullYear().toString()}/${(todate.getMonth() + 1).toString().padStart(2,'0')}/${todate.getDate().toString().padStart(2,'0')} `
        return val;
      }))
    ).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
  onBulkApprove(){
    const requestIDs :number[] =[];
    this.multipleSelectedRequest.forEach(req => {
      requestIDs.push(req.id);
    });
    this.leaveService.BulKAction(requestIDs.join(',')).subscribe({
      next: req => console.log("Succed"),
      error: err => console.log("Error")
    })
  }
}
