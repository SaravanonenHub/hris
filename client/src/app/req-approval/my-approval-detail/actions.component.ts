import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';

@Component({
  selector: 'app-actions',
  template: `
  <p-card header={{request.type.templateName}} subheader={{request.description}} (click) = "OnRequestClick(request)" [style]="{ width: '360px' }">
 
  <p>{{request.employee.displayName}}
  </p>
  <ng-template pTemplate="footer">
      <p-button label="Approve" icon="pi pi-check" (click) = "onApprovalClick(request)"></p-button>
      <p-button label="Cancel" icon="pi pi-times" styleClass="p-button-secondary" [style]="{ 'margin-left': '.5em' }"></p-button>
  </ng-template>
</p-card>
  `,
  styles: [`
  :host ::ng-deep .p-card {
    transition: background-color 0.3s ease-in-out; /* Add a smooth transition effect */
    cursor: pointer;
  }
  
  :host ::ng-deep .p-card:hover {
    background-color: #f0f0f0; /* Change the background color on hover */
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
