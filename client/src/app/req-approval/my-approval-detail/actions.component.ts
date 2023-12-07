import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';

@Component({
  selector: 'app-actions',
  template: `
  <p-card header={{request.employee.displayName}} subheader={{request.employee.department?.departmentName}} (click) = "OnRequestClick(request)" [style]="{ width: '360px' }">
 
  <p>{{request.type.templateName}}
  </p>
  <!-- <input type="button" value="Approve"/> -->
  <ng-template pTemplate="footer">
      <!-- <p-button label="Primary"></p-button> -->
      <p-button label="Approve" icon="pi pi-check" (click) = "onApprovalClick(request)"></p-button>
      <p-button label="Cancel" icon="pi pi-times" styleClass="p-button-secondary" [style]="{ 'margin-left': '.5em' }"></p-button>
  </ng-template>
</p-card>
  `,
  styles: [`
  :host ::ng-deep .p-card {
    transition: background-color 0.3s ease-in-out; /* Add a smooth transition effect */
    cursor:default;
    height:10rem;
    margin-bottom:0.5rem;
  }

  :host ::ng-deep .p-card .p-card-body {
      padding: 0.25rem;
  }
  :host ::ng-deep .p-card .p-card-title {
      font-size: 1.5rem;
      font-weight: 700;
      margin-bottom: 0rem;
  }
  :host ::ng-deep .p-card .p-card-subtitle {
      font-weight: 400;
      margin-bottom: 0rem;
      color: #6c757d;
  }
  :host ::ng-deep .p-card .p-card-content {
      padding: 0 0;
  }
  :host ::ng-deep .p-card .p-card-footer {
      padding: 0 0 0 0;
  }
  :host ::ng-deep .p-card:hover {
    background-color: #f0f0f0; /* Change the background color on hover */
  }
  :host ::ng-deep .p-button{
    height:2rem;
  }`
  ]
})
export class ActionsComponent {
  @Input() request!: IRequest
  @Output() requestSelect: EventEmitter<IRequest> = new EventEmitter<IRequest>();
  @Output() requestApprove: EventEmitter<IRequest> = new EventEmitter<IRequest>();
  OnRequestClick(req: IRequest) {
    this.requestSelect.emit(req);
  }
  onApprovalClick(req: IRequest) {
    this.requestApprove.emit(req);
  }
}
