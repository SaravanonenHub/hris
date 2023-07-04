import { AfterViewInit, Component, OnChanges, OnInit, SimpleChange, SimpleChanges } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common'
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { first } from 'rxjs';
import { EmployeeNature, Gender, IBranch, IDepartment, IDesignation, IDivision, ITeam, MartialStatus, OptionalSaturday, Role } from 'src/app/domain/models/master';
import { EmployeeService } from '../employee.service';
import { AlertService } from 'src/app/shared/services/alertService';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Employee, IEmployee } from 'src/app/domain/models/employee';
import { AccountService } from 'src/app/account/account.service';
import { PgmessageService } from 'src/app/shared/services/pgmessage.service';
import { Message } from 'primeng/api';
class ImageSnippet {
  constructor(public src: string, public file: File) {

  }
}
@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.scss'],
  providers: [DatePipe]
})
export class CreateEmployeeComponent implements OnInit {
  message! : Message[];
  faCircleXmark = faXmark;
  id?: number;
  gender: string | undefined;
  branches: IBranch[] = [];
  divisions: IDivision[] = [];
  departments: IDepartment[] = [];
  designations: IDesignation[] = [];
  teams: ITeam[] = [];
  imageUrl = environment.filesUrl;
  genders = Object.values(Gender).map(key => ({ label: Gender[key], value: key }));;
  martialStatuses = Object.values(MartialStatus).map(key => ({ label: MartialStatus[key], value: key }));
  natureOfEmployees = Object.values(EmployeeNature).map(key => ({ label: EmployeeNature[key], value: key }));;

  roles = Object.keys(Role).map((key, index) => ({
    label: Object.values(Role)[index], value: key
  }));
  optionalSaturday = Object.keys(OptionalSaturday).map((key, value) => ({ label: Object.values(OptionalSaturday)[value], value: key }));;
  selectedDepartment: number = 0;
  employee: any = { Employee: {} };
  loading = false;
  submitting = false;
  submitted = false;
  // employeeForm!: FormGroup;
  // imageURL: string = "D:/PROJECTS/ANGULAR2/HRIS/client/src/assets/images/themes/lara-light-blue.png";
  selectedImage?: ImageSnippet
  formData = new FormData();
  datenow: Date = new Date();
  employeeForm = this.fb.group({
    employeeCode: ['', [Validators.required]],
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    displayName: [''],
    imagePath: [''],
    branchId: [0, [Validators.min(1)]],
    divisionId: [0, [Validators.min(1)]],
    departmentId: [0, [Validators.min(1)]],
    designationId: [0, [Validators.min(1)]],
    qualification: ['', [Validators.required]],
    status: ['Live'],
    birthDate: [this.datePipe.transform(this.datenow, 'dd/MM/yyyy'), [Validators.required]],
    age: [0],
    joinDate: [this.datePipe.transform(this.datenow, 'dd/MM/yyyy'), [Validators.required]],
    emailId: ['', [Validators.required, Validators.email]],
    gender: ['', [Validators.required]],
    bloodGroup: ['', [Validators.required]],
    martialStatus: ['', [Validators.required]],
    employeeNature: ['', [Validators.required]],
    optionalSaturday: ['Y', [Validators.required]],
    teamId: [0, [Validators.min(1)]],
    teamRoleId: ['Member', [Validators.min(1)]],
    multiTeam:[false],
    empimage: [null]
  })
  constructor(private fb: FormBuilder, private empService: EmployeeService, private datePipe: DatePipe
    , private alertService: AlertService, private route: ActivatedRoute, private router: Router
    , private accountService: AccountService, private messageService:PgmessageService) { }
 

  ngOnInit() {
    this.alertService.successAlert("Records Received");
    console.log(this.datePipe.transform(this.datenow, 'dd/MM/yyyy'));
    this.employeeForm.get('multiTeam')?.setValue(false, { emitEvent: false })
    console.log(this.employeeForm.get('multiTeam')?.value)
    // this.imageUrl = `${this.imageUrl}default.png`;
    // console.log(Object.values(Role).filter((f) => !isNaN(Number(f))));
    // Object.keys(Role).filter((f) => !isNaN(Number(f))).map((key, value) => (

    //   console.log(Object.values(Role)[value], key)
    // ));

    // Object.values(Role).filter((f) => !isNaN(Number(f))).map((key, value) => (

    //   console.log(Object.values(Role)[value], key)
    // ));
    this.id = this.route.snapshot.params['id'];
    // this.employeeForm.patchValue({ 'empimage': new File([],'')})
    this.getDropdowns();
    this.employeeForm.valueChanges.subscribe(() => {
      this.employeeForm.get('displayName')?.setValue(this.referencePublicacionValues, { emitEvent: false })
    })
    this.employeeForm.get('teamRoleId')?.valueChanges.subscribe((value) => {
      console.log(value);
      if(value == Role.Manager){
        this.employeeForm.get('multiTeam')?.setValue(true);
      }
      else{
        this.employeeForm.get('multiTeam')?.setValue(false);
      }
      console.log(this.employeeForm.get('multiTeam')?.value);
    })
    
    if (this.id) {
      this.empService.getEmployeesBaseById(this.id).subscribe(x => {
        debugger;
        this.patchemp(x);


        // console.log(`${this.imageUrl}${x.employeeCode}.png`);
        // console.log(this.f.teamRoleId.value);
      })

    }
  }
 
  get f() {
    return this.employeeForm.controls;
  }
  public control(name: string): AbstractControl | null {
    return this.employeeForm.get(name);
  }
  private patchemp(emp: Employee) {
    debugger;
    this.control('employeeCode')?.patchValue(emp.employeeCode);
    this.control('firstName')?.patchValue(emp.firstName);
    this.control('lastName')?.patchValue(emp.lastName);
    this.control('displayName')?.patchValue(emp.fullName);
    this.control('branchId')?.patchValue(emp.branchId);
    this.control('divisionId')?.patchValue(emp.divisionId);
    this.control('departmentId')?.patchValue(emp.departmentId);
    this.control('designationId')?.patchValue(emp.designationId);
    this.control('qualification')?.patchValue(emp.qualification);
    this.control('status')?.patchValue(emp.status);
    this.control('birthDate')?.patchValue(new Date(emp.birthDate));
    this.control('age')?.patchValue(emp.age);
    this.control('joinDate')?.patchValue(this.datePipe.transform(new Date(emp.joinDate), 'dd/MM/yyyy'));
    this.control('emailId')?.patchValue(emp.emailID);
    this.control('gender')?.patchValue(emp.gender);
    this.control('bloodGroup')?.patchValue(emp.bloodGroup);
    this.control('martialStatus')?.patchValue(emp.martialStatus);
    this.control('employeeNature')?.patchValue(emp.employeeNature);
    this.control('optionalSaturday')?.patchValue(emp.optionalSaturday);
    this.control('teamId')?.patchValue(emp.teamId);
    this.control('teamRoleId')?.patchValue(emp.teamRoleId);
    this.imageUrl = `${this.imageUrl}${emp.employeeCode}.png`;
  }
  get referencePublicacionValues(): string {

    if ((this.employeeForm.get('firstName')?.value === '' || this.employeeForm.get('firstName')?.value === undefined)
      || (this.employeeForm.get('lastName')?.value === '' || this.employeeForm.get('lastName')?.value === undefined))
      return '';

    return `${this.employeeForm.get('firstName')?.value} , ${this.f.lastName?.value}`

  }
  getDropdowns() {
    this.empService.getBranches().subscribe({
      next: orders => this.branches = orders
    })
    this.empService.getDivisions().subscribe({
      next: orders => this.divisions = orders
    })
    this.empService.getDepartments().subscribe({
      next: orders => this.departments = orders
      // next: orders => console.log(orders)
    })
    this.empService.getDesignations().subscribe({
      next: orders => this.designations = orders
    })
    this.empService.getTeams().subscribe({
      next: orders => this.teams = orders
    })
  }

  onSubmit() {
    this.submitted = true;
    
    let result = Object.assign({}, this.f);
    // console.log(`Result: ${result}`);
    //this.employeeForm.get('birthDate')?.setValue(new Date(this.datepipe.transform(this.date, "dd/MM/yyyy")))
    Object.keys(this.f).forEach((key: any) => {
      const abstractControl = this.employeeForm.get(key);
      // console.log(key, abstractControl?.value)
      this.formData.append(key, abstractControl?.value);
    });
    this.accountService.currentUser$.subscribe(data => {
      this.formData.append('CreatedBy', data != null ? data.displayName : '')
    });
    this.empService.create(this.formData)
      .pipe(first())
      .subscribe({
        next: () => {
          this.message = [ { severity: 'success', summary: 'Success', detail: 'Message Content' }],
          this.messageService.setEmitter(this.message);
          this.employeeForm.reset();
          //this.alertService.successAlert('User saved');
          // console.log("Success")
        },
        error: error => {
          // this.alertService.errorAlert('User saved');
          this.message = [ { severity: 'error', summary: 'Error!', detail: error}],
          this.messageService.setEmitter(this.message);
          // console.log(error)
        }
      })

    // this.employee.
    // this.empService.postEmployee(this.employeeForm.value).subscribe((result) => {
    //   this.router.navigateByUrl('/profile/employee/overview');
    console.log(this.employeeForm);
  }
  onBasicUploadAuto(imageInput: any) {

    const file: File | any = imageInput.files[0];
    const reader = new FileReader();
    reader.addEventListener('load', (event: any) => {

      this.selectedImage = new ImageSnippet(event.target.result, file);
      this.imageUrl = file;
      this.formData.append('image', file, this.f.employeeCode.value!);
    })
    reader.readAsDataURL(file);
    debugger;
    this.employeeForm.get('empimage')?.setValue(file);
    // this.f.empimage = file;
  }
  onBirthDateChange(data: any) {
    debugger;
    // this.employeeForm.patchValue({ 'birthDate': formatDate(data.target.result, 'short', '') })
    console.log(data)
  }
  
}
