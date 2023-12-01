import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';
import { ApprovalService } from '../approval.service';
import { LeaveDetailsComponent } from 'src/app/requests/leaves/leave-details/leave-details.component';

@Component({
  selector: 'app-my-approval-detail',
  templateUrl: './my-approval-detail.component.html',
  styleUrls: ['./my-approval-detail.component.scss']
})
export class MyApprovalDetailComponent {
  openApprovals:IRequest[]=[];
  selectedRequest!:IRequest;
  @ViewChild('detailContainer',{read:ViewContainerRef}) detailContainer!:ViewContainerRef;
  constructor(private service:ApprovalService){}
  ngOnInit(): void {
    debugger;
   this.service.getOpenApproval().then((val) => {
    val.subscribe((data) => 
      {
        console.log(data);
        this.openApprovals = data
      });
   });
    
  }
  loadDetailComponent(componentType:any){
    this.detailContainer.createComponent(componentType);
  }
  onRequestSelect(req:IRequest){
    console.log(req);
    if(req.type.templateName == "Leave")
    {
      this.loadDetailComponent(LeaveDetailsComponent);
    }
  }
}
