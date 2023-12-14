import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../leave.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { RequestTemplate } from 'src/app/domain/models/master';
import { DatePipe } from '@angular/common';
import { ILeave, ILeaveEntitlement, LeaveType, Session } from 'src/app/domain/models/leave';
import { Observable, firstValueFrom, map, take } from 'rxjs';
import { PgmessageService } from 'src/app/shared/services/pgmessage.service';
import { AccountService } from 'src/app/account/account.service';
import { EmployeeService } from 'src/app/employees/employee.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { Notify, NotifyType } from 'src/app/shared/models/notify';
import { Message } from 'primeng/api';
import { leavePolicyParams } from 'src/app/shared/models/leavePolicyParams';

@Component({
  selector: 'app-edit-requests',
  templateUrl: './edit-requests.component.html',
  styleUrls: ['./edit-requests.component.scss'],
  providers: [DatePipe]

})
export class EditRequestsComponent implements OnInit{
  leave!:ILeave;
  entitlement!: ILeaveEntitlement;
  isExpanded: boolean = true;
  isDisabled = true;
  morethanOneDay = false;
  showToDate = true;
  showFullDescription: boolean = false;
  formData = new FormData();
  message!: Message[];
  minDate: Date = new Date();
  leavePolicyParam: leavePolicyParams = {};
  datenow: Date = new Date();
  selectedType: { label: LeaveType; Value: string } | any = {};
  leaveTypes = Object.keys(LeaveType).map((key, value) => ({ label: Object.values(LeaveType)[value], value: key }));
  sessions = Object.keys(Session).map((key, value) => ({ label: Object.values(Session)[value], value: key }));
  leaveReqForm = this.fb.group({
    employeeId: ['', [Validators.required]],
    leaveType: ['', [Validators.required]],
    fromDate: [this.datePipe.transform(new Date(), 'dd/MM/yyyy'), [Validators.required]],
    toDate: [this.datePipe.transform(new Date(), 'dd/MM/yyyy'), [Validators.required]],
    balance: ['-'],
    days: ['1'],
    session: ['-', [Validators.min(1)]],
    reason: ['', [Validators.min(1)]],
    status: ['Submitted'],
    cancellationStatus: ['N'],
    templateId: [RequestTemplate.Leave],
    dayType: ['One', [Validators.required]]
  });

  constructor(private service:LeaveService
    , private fb:FormBuilder
    , private datePipe: DatePipe
    , private route:ActivatedRoute
    , private accountService: AccountService
    , private router: Router
    , private messageService: PgmessageService
    , private empService: EmployeeService
    , private notifyService: NotificationService){}
  ngOnInit(): void {
   
    
    this.route.queryParams.subscribe((params) => {
      const requestId = params['requestId'];

      this.service.getRequest(requestId).subscribe((leaveData) => {
        console.log(leaveData);
        this.leave = leaveData;
        this.leavePolicyParam.empId = leaveData.request.employee.id;
        this.leavePolicyParam.leaveType = "";
        this.leavePolicyParam.policyName = "";
        this.entitlement = leaveData.entitlement;
        console.log(leaveData.entitlement);
        const leaveProvided = this.entitlement.details.find(x => x.leaveType.leaveName == leaveData.leaveType)?.provided
        const leaveTaken = leaveData.entitlement.details.find(x => x.leaveType.leaveName == leaveData.leaveType)?.taken;
        const bal:Number = leaveProvided! - leaveTaken!;
        this.onRadioDaytypeClick(leaveData.days == 1 ? 'One' : 'Multi');
        this.leaveReqForm.patchValue({
          employeeId: leaveData.request.employee.id!.toString(),
          leaveType: leaveData.leaveType,
          fromDate: this.datePipe.transform(leaveData.fromDate, 'dd/MM/yyyy'),
          toDate: this.datePipe.transform(leaveData.tDate, 'dd/MM/yyyy'),
          days: leaveData.days.toString(),
          session:leaveData.session,
          reason: leaveData.reason,
          status: leaveData.status,
          cancellationStatus: leaveData.cancellationStatus,
          templateId: RequestTemplate.Leave,
          dayType: leaveData.days == 1 ? 'One' : 'Multi',
          balance: bal.toString()
        });
      });
      // Use the requestId as needed
    });
    
    
  }
  public control(name: string): AbstractControl | null {
    return this.leaveReqForm.get(name);
  }
  toggleDescription() {
    this.showFullDescription = !this.showFullDescription;
  }
  onDateChange(event: any) {
    this.dateFunction();
    let dateParts = this.f?.fromDate?.value!.split("/");
    let fdate = new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
    this.minDate = new Date(fdate);

    // console.log(this.minDate);
  }
  dateFunction() {
    let one_day = 1000 * 60 * 60 * 24;
    let dateParts = this.f?.fromDate?.value!.split("/");
    let toDateParts = this.f?.toDate?.value!.split("/");
    let fdate = new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
    let tdate = new Date(+toDateParts[2], +toDateParts[1] - 1, +toDateParts[0]);
    let selectedSession = this.leaveReqForm.get('session')?.value?.toString();
    if (!this.morethanOneDay) {
      this.leaveReqForm.controls['toDate'].setValue(this.f?.fromDate.value);
      this.leaveReqForm.controls['days'].setValue('1');
      this.changeFunction(selectedSession!);
      this.checkBalanceFunction();
    }
    else {
      if (fdate <= tdate) {
        console.log(dateParts);
        let days = Math.floor((tdate.getTime() - fdate.getTime()) / one_day);
        this.leaveReqForm.controls['days'].setValue((days + 1).toString());
      }
      else { this.leaveReqForm.controls['days'].setValue('-'); }
      this.changeFunction(selectedSession!);
      this.checkBalanceFunction();
    }

  }
  checkBalanceFunction() {
    const balance = parseInt(this.f.balance.value!);
    const days = parseInt(this.f.days.value!);
    if (days > balance) {
      console.log('Insuffiecient balance');
    }
  }
  changeFunction(session: string) {
    const selectedSession = session;
    this.showToDate = selectedSession === 'FULLDAY'; // Show "To Date" if the session is not "Half"
    if (selectedSession === "FIRST SESSION" || selectedSession === "SECOND SESSION") {
      const fdate = this.leaveReqForm.get('fromDate')?.value;
      this.leaveReqForm.get('fromDate')?.setValue(fdate!);
      this.f.days.setValue('0.5');
      console.log(this.leaveReqForm.get('days'));
    }
  }
  onSessionChange(event: any) {
    this.dateFunction();

  }
  get f() {
    return this.leaveReqForm.controls;
  }
  leaveTypeChange(ev: any) {
    console.log(ev.value);
    let days = 0;
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: async user => {
        const employee = await firstValueFrom(this.empService.getEmployeesBaseByCode(user?.email!))
        this.leavePolicyParam.empId = employee.id;
        this.leavePolicyParam.leaveType = "";
        this.leavePolicyParam.policyName = "";

        this.service.getEntitlement(this.leavePolicyParam!).pipe(map((ent) => {
          ent.details.forEach((detail) => {
            console.log(detail.leaveType.leaveName === ev.value)
            if (detail.leaveType.leaveName === ev.value) {
              let balance = 0;
              balance = detail.provided - detail.taken;
              console.log(balance);
              this.leaveReqForm.controls['balance'].setValue(balance.toString());
            }
          })
        })).subscribe();
      }

    })

    this.checkBalanceFunction()
  }
  onRadioDaytypeClick(val: any) {
    console.log(val.value);
    if (val.value == "Multi") {
      this.morethanOneDay = true;
      console.log("More than one day");
    } else { this.morethanOneDay = false; }
  }
  getMinDate(): Date {
    const fromDate = this.f?.fromDate?.value!;
    console.log(this.f?.fromDate?.value!);

    return new Date(fromDate!);
  }
  onCancel(){
    this.service.cancelRequest(this.leave.id,this.leave).subscribe((res) => {
      console.log("Cancelled Request successfully");
    });
  }
  onSubmit() {
    // of(10).subscribe(() => {
    //   this.router.navigate(['request/services/leave/leaveDetails', 11]);
    // });
    debugger;
    this.formData = new FormData();
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.leaveReqForm.controls['employeeId'].setValue(user?.email!)
    })
    Object.keys(this.f).forEach((key: any) => {
      const abstractControl = this.leaveReqForm.get(key);
      // console.log(key, abstractControl?.value)
      this.formData.append(key, abstractControl?.value);
    });
    this.accountService.currentUser$.subscribe(data => {
      this.formData.append('CreatedBy', data != null ? data.email : '')
    });


    this.service.create(this.formData).subscribe({
      next: (res) => {
        debugger;
        console.log(res);

        this.empService.getEmployeesBaseByCode(res.request.employee?.employeeCode!).subscribe((emp) => {
          let notify = new Notify({
            type: NotifyType.Request,
            team: emp.team,
            message: 'New Leave Request',
            routeUrl: '',
            autoclose: true,
            keepAfterRouteChange: false,
            fade: false
          });
          console.log("get employee succeed!");
          this.notifyService.onNotify(notify);
          this.router.navigate(['request/services/leave/leaveDetails'], { queryParams: { requestId: res.id } });;
          // this.router.navigate(['./details', res.id]);
        });


      },
      error: (err) => {
        debugger;
        this.message = [{ severity: 'error', summary: 'Error!', detail: err.error.message }],
          this.messageService.setEmitter(this.message);
      },
    })
  }
}
