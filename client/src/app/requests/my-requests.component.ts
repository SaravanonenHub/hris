import { Component, OnInit } from '@angular/core';
import { IRequest } from '../domain/models/request';
import { RequestService } from './request.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-requests',
  template: `
    <div>
      <app-open-new-request></app-open-new-request>
      <div class="center-container">
      <p-table [value]="requests"  [(selection)]="selectedRequest" selectionMode="single" dataKey="id" 
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
                <td>{{request.status}}</td>
                <td>{{request.requestDate}}</td>
                <td>{{request.type.templateName}}</td>
                <td>{{request.cancellationStatus}}</td>
            </tr>
        </ng-template>
    </p-table>
      </div>
      
    </div>
  `,
  styles: [` .center-container {
    display: flex;
    justify-content: center;
  }
  p-table {
      min-width: 50rem;
    }`
  ]
})
export class MyRequestsComponent implements OnInit {
  requests:IRequest[]=[];
  selectedRequest!:IRequest;
  constructor(private service:RequestService, private router:Router){}
  ngOnInit(): void {
    this.service.getRequests().subscribe((data) => {
      this.requests = data;
    })
  }

  onRequestSelect(){
    this.router.navigateByUrl(`request/details/${this.selectedRequest.id}`);
  //   console.log(this.selectedRequest);
  }
}
