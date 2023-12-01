import { Component } from '@angular/core';
import { IRequest } from '../domain/models/request';
import { ApprovalService } from './approval.service';

@Component({
  selector: 'app-my-closed-approval',
  template: `
       <p-accordion [activeIndex]="0">
    <p-accordionTab header="my closed approvals">
    <p-table [value]="closedApprovals"  [(selection)]="selectedRequest" selectionMode="single" dataKey="id" 
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
export class MyClosedApprovalComponent {
  closedApprovals:IRequest[]=[];
  selectedRequest!:IRequest;
  constructor(private service:ApprovalService){}
  ngOnInit(): void {
    debugger;
   this.service.getOpenApproval().then((val) => {
    val.subscribe((data) => this.closedApprovals = data);
   });
    
  }

  onRequestSelect(){
    console.log(this.selectedRequest);
  }
}
