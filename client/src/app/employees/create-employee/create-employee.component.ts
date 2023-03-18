import { Component } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { faXmark } from '@fortawesome/free-solid-svg-icons';
import { first } from 'rxjs';
import { EmployeeNature, Gender, IBranch, IDepartment, IDesignation, IDivision, ITeam, MartialStatus, OptionalSaturday, Role } from 'src/app/domain/models/master';
import { EmployeeService } from '../employee.service';
import { AlertService } from 'src/app/shared/services/alertService';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
class ImageSnippet {
  constructor(public src: string, public file: File) {

  }
}
@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.scss']
})
export class CreateEmployeeComponent {
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

  roles = Object.values(Role).filter((f) => !isNaN(Number(f))).map((key, index) => ({
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
    birthDate: [this.datenow.toDateString, [Validators.required]],
    age: [0],
    joinDate: [this.datenow.toDateString, [Validators.required]],
    emailId: ['', [Validators.required, Validators.email]],
    gender: ['', [Validators.required]],
    bloodGroup: ['', [Validators.required]],
    martialStatus: ['', [Validators.required]],
    employeeNature: ['', [Validators.required]],
    optionalSaturday: ['Y', [Validators.required]],
    teamId: [0, [Validators.min(1)]],
    teamRoleId: [0, [Validators.min(1)]],
    empimage: [null]
  })
  constructor(private fb: FormBuilder, private empService: EmployeeService
    , private alertService: AlertService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    // console.log(this.imageUrl);
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

    if (this.id) {
      this.empService.getEmployeesBaseById(this.id).subscribe(x => {
        this.employeeForm.patchValue(x);
        // this.employeeForm.get('birthDate')?.setValue(x.birthDate);
        // this.employeeForm.get('joinDate')?.setValue(x.joinDate);
        this.imageUrl = `${this.imageUrl}${x.employeeCode}.png`;
        // console.log(`${this.imageUrl}${x.employeeCode}.png`);
        // console.log(this.f.teamRoleId.value);
      })

    }
  }

  get f() {
    return this.employeeForm.controls;
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
    this.alertService.errorAlert('User saved');
    let result = Object.assign({}, this.f);
    console.log(`Result: ${result}`);
    // this.employeeForm.get('birthDate')?.setValue()
    Object.keys(this.f).forEach((key: any) => {
      const abstractControl = this.employeeForm.get(key);
      // console.log(key, abstractControl?.value)
      this.formData.append(key, abstractControl?.value);
    });

    this.empService.create(this.formData)
      .pipe(first())
      .subscribe({
        next: () => {
          console.log("Success")
        },
        error: error => {
          console.log("Error")
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
