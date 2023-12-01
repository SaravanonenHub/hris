import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IRequest } from '../domain/models/request';
import { ApprovalService } from './approval.service';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';

@Component({
  selector: 'app-my-open-approval',
  template: `
    <p-accordion [activeIndex]="0">
    <p-accordionTab header="my opened approvals">
    <p-table [value]="openApprovals"  [(selection)]="selectedRequest" selectionMode="single" dataKey="id" 
        [tableStyle]="{'min-width': '50rem'}" (onRowSelect)="onRequestSelect()">
        <ng-template pTemplate="header" let-columns>
            <tr>
                <th>Request ID </th>
                <th>Description</th>
                <th>Status</th>
                <th>Request Date</th>
                <th>Type</th>
                <th></th>
            </tr>
        </ng-template>
        <ng-template pTemplate="body"
         let-request let-columns="columns">
            <tr [pSelectableRow]="request">
                <td>{{request.requestId}}</td>
                <td>{{request.description}}</td>
                <td>{{request.currentState}}</td>
                <td>{{request.requestDate}}</td>
                <td>{{request.type.templateName}}</td>
                <td>{{request.cancellationStatus}}</td>
            </tr>
        </ng-template>
    </p-table>
    </p-accordionTab>
</p-accordion>
  `,
  styles: [
  ]
})
export class MyOpenApprovalComponent {
  openApprovals:IRequest[]=[];
  selectedRequest!:IRequest;
  @Output() requestClick:EventEmitter<IRequest> = new EventEmitter<IRequest>();
  constructor(private service:ApprovalService){}
  ngOnInit(): void {
    debugger;
   this.service.getOpenApproval().then((val) => {
    val.subscribe((data) => this.openApprovals = data);
   });
    
  }

  onRequestSelect(){
    console.log(this.selectedRequest);
    this.requestClick.emit(this.selectedRequest);
  }
}
