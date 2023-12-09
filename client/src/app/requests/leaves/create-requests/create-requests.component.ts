import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Observable, first, firstValueFrom, map, of, take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { ILeaveEntitlement, Leave, LeaveType, Session } from 'src/app/domain/models/leave';
import { LeaveService } from '../leave.service';
import { EmployeeService } from 'src/app/employees/employee.service';
import { Notify, NotifyType } from 'src/app/shared/models/notify';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { IEmployee } from 'src/app/domain/models/employee';
import { leavePolicyParams } from 'src/app/shared/models/leavePolicyParams';
import { RequestTemplate } from 'src/app/domain/models/master';
import { Route, Router } from '@angular/router';
import { Message } from 'primeng/api';
import { PgmessageService } from 'src/app/shared/services/pgmessage.service';
export interface City {
  code: string;
  name: string;
}
export enum FileTypesEnum {
  CSV = "CSV",
  JSON = "JSON",
  XML = "XML",
}
export const FileType2LabelMapping: Record<FileTypesEnum, string> = {
  [FileTypesEnum.CSV]: "Here's Csv",
  [FileTypesEnum.JSON]: "Here's Json",
  [FileTypesEnum.XML]: "Here's Xml",
};
@Component({
  selector: 'app-create-requests',
  templateUrl: './create-requests.component.html',
  styleUrls: ['./create-requests.component.scss'],
  providers: [DatePipe]
})

export class CreateRequestsComponent implements OnInit {
  message!: Message[];
  cities: City[] = [];
  showFullDescription: boolean = false;
  entitlement$!: Observable<ILeaveEntitlement>;
  customToggleIcon: string = "pi pi-angle-down";
  isExpanded: boolean = true;
  isDisabled = true;
  public FileType2LabelMapping = FileType2LabelMapping;
  public fileTypes = Object.values(FileTypesEnum);
  leavePolicyParam: leavePolicyParams = {};
  showToDate = true;
  morethanOneDay = false;
  minDate: Date = new Date();
  // selectedCountry: string | any;
  datenow: Date = new Date();
  selectedType: { label: LeaveType; Value: string } | any = {};
  leaveTypes = Object.keys(LeaveType).map((key, value) => ({ label: Object.values(LeaveType)[value], value: key }));
  sessions = Object.keys(Session).map((key, value) => ({ label: Object.values(Session)[value], value: key }));
  formData = new FormData();
  leaveReqForm = this.fb.group({
    employeeId: ['', [Validators.required]],
    leaveType: ['', [Validators.required]],
    fromDate: [this.datePipe.transform(this.datenow, 'dd/MM/yyyy'), [Validators.required]],
    // fromDate: [''],
    toDate: [this.datePipe.transform(this.datenow, 'dd/MM/yyyy'), [Validators.required]],
    balance: ['-'],
    days: ['1'],
    session: [0, [Validators.min(1)]],
    reason: ['', [Validators.min(1)]],
    status: ['Submitted'],
    cancellationStatus: ['N'],
    templateId: [RequestTemplate.Leave],
    dayType: ['One', [Validators.required]]
  })
  @ViewChild('myFormRef') myFormRef: any;
  constructor(private fb: FormBuilder, private service: LeaveService
    , private datePipe: DatePipe, private accountService: AccountService
    , private router: Router, private messageService: PgmessageService
    , private empService: EmployeeService, private notifyService: NotificationService) {

    this.leaveReqForm.controls['days'].disable();
  }
  async ngOnInit() {
    const user = await firstValueFrom(this.accountService.currentUser$);
    const employee = await firstValueFrom(this.empService.getEmployeesBaseByCode(user?.email!))
    this.leavePolicyParam.empId = employee.id;
    this.leavePolicyParam.leaveType = "";
    this.leavePolicyParam.policyName = "";
    this.entitlement$ = this.service.getEntitlement(this.leavePolicyParam!)
  }
  get f() {
    return this.leaveReqForm.controls;
  }
  onBeforeToggle() {
    debugger;
    this.isExpanded = !this.isExpanded;
    this.customToggleIcon = this.isExpanded ? 'pi pi-angle-up' : 'pi pi-angle-down'
  }
  onDateChange(event: any) {
    this.dateFunction();
    let dateParts = this.f?.fromDate?.value!.split("/");
    let fdate = new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
    this.minDate = new Date(fdate);

    console.log(this.minDate);
  }
  submitForm() {
    this.onSubmit();
  }
  getMinDate(): Date {
    const fromDate = this.f?.fromDate?.value!;
    console.log(this.f?.fromDate?.value!);

    return new Date(fromDate!);
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

  onSessionChange(event: any) {
    this.dateFunction();

  }
  toggleDescription() {
    this.showFullDescription = !this.showFullDescription;
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
  convertToSQLDateTime(inputDate: string): string {
    const parts = inputDate.split('/');
    const day = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10);
    const year = parseInt(parts[2], 10);

    // Note: Months in JavaScript Date are 0-indexed, so we subtract 1 from the month.
    const dateObject = new Date(year, month - 1, day);

    // Formatting the date into "YYYY-MM-DD HH:mm:ss" format
    const formattedDate = dateObject.toISOString().slice(0, 19).replace('T', ' ');

    return formattedDate;
  }
  onRadioDaytypeClick(val: any) {
    console.log(val.value);
    if (val.value == "Multi") {
      this.morethanOneDay = true;
      console.log("More than one day");
    } else { this.morethanOneDay = false; }
  }
  sqlDateFormatValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const inputValue = control.value;
      if (!inputValue) {
        return null; // Return null if the control is empty (valid).
      }

      const sqlDateFormat = this.convertToSQLDateTime(inputValue);

      // Update the control's value to the SQL formatted date.
      control.setValue(sqlDateFormat);

      return null; // Return null if the control is valid.
    };
  }
}
