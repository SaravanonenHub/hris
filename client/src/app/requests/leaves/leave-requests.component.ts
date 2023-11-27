import { Component, OnInit } from '@angular/core';
import { ILeave, ILeaveEntitlement } from 'src/app/domain/models/leave';
import { LeaveService } from './leave.service';
import { AccountService } from 'src/app/account/account.service';
import { EmployeeService } from 'src/app/employees/employee.service';
import { leavePolicyParams } from 'src/app/shared/models/leavePolicyParams';
import { Observable, map, observable } from 'rxjs';
// import { Store, select } from '@ngrx/store';
// import * as LeaveActions from './store/actions'
// import { leaveSelector } from './store/selector';
import { AppInterfaceState } from 'src/app/domain/app.interface';
@Component({
  selector: 'app-leave-requests',
  templateUrl: './leave-requests.component.html',
  styleUrls: ['./leave-requests.component.scss']
})
export class LeaveRequestsComponent implements OnInit {

  leaveRequests: ILeave[] = []
  entitlement?: ILeaveEntitlement;
  selectedRequest?: ILeave | null = null;
  leavePolicyParam: leavePolicyParams = {};
  customToggleIcon: string = "pi pi-angle-down";
  isExpanded: boolean = true;
  request$!: Observable<ILeave[]>;
  constructor(private leaveService: LeaveService
    , private accountService: AccountService
    , private empService: EmployeeService
    // , private store:Store<AppInterfaceState>
  ) { }
  ngOnInit(): void {
    // this.store.dispatch(LeaveActions.getLeaves());
    // this.request$ = this.store.pipe(select(leaveSelector)).pipe(
    //   map((lev) => lev.map((val) => {
    //     const prefix = 'RQ';
    //     const id = val.id;
    //     const numericLength = 10;
    //     const numberString = id.toString();
    //     const zeroNeeded = numericLength - numberString.length;
    //     //val.idStr = prefix + '0'.repeat(zeroNeeded) + numberString;
    //     const newVal = {
    //       ...val, // Copy the existing properties
    //       idStr: prefix + '0'.repeat(zeroNeeded) + numberString,
    //     };
    //     const fromdate = new Date(val.fromDate);
    //     const todate = new Date(val.toDate);
    //     const fyear = fromdate.getFullYear().toString();
    //     const fmonth = (fromdate.getMonth() + 1).toString().padStart(2,'0');
    //     const fdate = fromdate.getDate().toString().padStart(2,'0');
    //     newVal.fDate = `${fyear}/${fmonth}/${fdate}`;
    //     newVal.tDate = `${todate.getFullYear().toString()}/${(todate.getMonth() + 1).toString().padStart(2,'0')}/${todate.getDate().toString().padStart(2,'0')} `
    //     return newVal;
    //   }))
    // );
    // this.accountService.currentUser$.subscribe((emp) => {
    //   this.empService.getEmployeesBaseByCode(emp?.email!).subscribe((employee) => {
    //     this.getRequests(employee?.id);
    //     this.getEntitlement(employee.id!);

    //   })
    // });

  }
  onBeforeToggle() {
    debugger;
    this.isExpanded = !this.isExpanded;
    this.customToggleIcon = this.isExpanded ? 'pi pi-angle-up' : 'pi pi-angle-down'
  }
  onEdit() {
    console.log(this.selectedRequest);

    // this.router.navigateByUrl(`/employee/personal-edit/${this.selectedRequest?.id}`);
  }
  getEntitlement(empId: number) {
    this.leavePolicyParam.empId = empId;
    this.leavePolicyParam.leaveType = "";
    this.leavePolicyParam.policyName = "";
    this.leaveService.getEntitlement(this.leavePolicyParam!).subscribe({
      next: requests => this.entitlement = requests
    })
  }
  getRequests(empId: number | undefined) {
    this.leaveService.getRequests(empId!).pipe(
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
        const fmonth = (fromdate.getMonth() + 1).toString().padStart(2, '0');
        const fdate = fromdate.getDate().toString().padStart(2, '0');
        val.fDate = `${fyear}/${fmonth}/${fdate}`;
        val.tDate = `${todate.getFullYear().toString()}/${(todate.getMonth() + 1).toString().padStart(2, '0')}/${todate.getDate().toString().padStart(2, '0')} `
        return val;
      }))
    ).subscribe({
      next: requests => this.leaveRequests = requests
    })
  }
}
