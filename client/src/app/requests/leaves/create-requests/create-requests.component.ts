import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { first, take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { LeaveType, Session } from 'src/app/domain/models/leave';
import { LeaveService } from '../leave.service';
import { EmployeeService } from 'src/app/employees/employee.service';
import { Notify, NotifyType } from 'src/app/shared/models/notify';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { IEmployee } from 'src/app/domain/models/employee';
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

export class CreateRequestsComponent {
  cities: City[] = [];
  isDisabled = true;
  public FileType2LabelMapping = FileType2LabelMapping;
  public fileTypes = Object.values(FileTypesEnum);
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
    days: ['-'],
    session: [0, [Validators.min(1)]],
    reason: ['', [Validators.min(1)]],
    status: ['Pending']
  })
  constructor(private fb: FormBuilder, private service: LeaveService
    , private datePipe: DatePipe, private accountService: AccountService
    , private empService: EmployeeService, private notifyService: NotificationService) {
    this.cities = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
    ];
    this.leaveReqForm.controls['days'].disable();
  }
  get f() {
    return this.leaveReqForm.controls;
  }
  onDateChange(event: any) {
    let one_day = 1000 * 60 * 60 * 24;
    let dateParts = this.f?.fromDate?.value!.split("/");
    let toDateParts = this.f?.toDate?.value!.split("/");
    let fdate = new Date(+dateParts[2], +dateParts[1] - 1, +dateParts[0]);
    let tdate = new Date(+toDateParts[2], +toDateParts[1] - 1, +toDateParts[0]);
    if (fdate <= tdate) {
      console.log(dateParts);
      let days = Math.floor((tdate.getTime() - fdate.getTime()) / one_day);
      this.leaveReqForm.controls['days'].setValue((days + 1).toString());
    }
    else { this.leaveReqForm.controls['days'].setValue('-'); }

  }
  onSubmit() {
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


    this.service.create(this.formData).pipe(first()).subscribe({
      next: (res) => {
        console.log(res);
        this.empService.getEmployeesBaseByCode(res.employee?.employeeCode!).subscribe((emp) => {
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
        });


      },
      error: () => console.log('error'),
    })
  }
  OnChange(ev: any) {
    console.log(ev);
  }
}
