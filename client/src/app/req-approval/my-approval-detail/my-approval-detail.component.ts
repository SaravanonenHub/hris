import { ChangeDetectorRef, Component, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';
import { ApprovalService } from '../approval.service';
import { LeaveDetailsComponent } from 'src/app/requests/leaves/leave-details/leave-details.component';
import { NgComponentOutlet } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-approval-detail',
  templateUrl: './my-approval-detail.component.html',
  styleUrls: ['./my-approval-detail.component.scss'],
})
export class MyApprovalDetailComponent {
  openApprovals: IRequest[] = [];
  selectedRequest?: IRequest;
  @ViewChild('detailContainer', { read: ViewContainerRef }) detailContainer!: ViewContainerRef;
  leavedetailcom!: LeaveDetailsComponent;
  constructor(private service: ApprovalService, private cdr: ChangeDetectorRef, private route: ActivatedRoute) { }
  ngOnInit(): void {
    debugger;
    this.service.getOpenApproval().then((val) => {
      val.subscribe((data) => {
        console.log(data);
        this.openApprovals = data
      });
    });
    const requestId = this.route.snapshot.queryParams['id'];
    this.service.getRequestDetail(requestId).subscribe((data) => {
      console.log(data);
      this.selectedRequest = data
      this.loadDetailComponent(LeaveDetailsComponent, this.selectedRequest);
    });
  }
  loadDetailComponent(componentType: Type<any>, req: IRequest) {
    this.detailContainer.clear();
    const detailsComponent =
      this.detailContainer.createComponent(componentType);
    const inputs: Record<string, IRequest> = { "request": req };
    detailsComponent.instance.request = req; // Set request after creation
  }
  loadComponent() {
    const result = this.service.getDetailComponent(this.selectedRequest!);
    console.log(result);
    return result;
  }
  onRequestSelect(req: IRequest) {
    debugger;

    this.selectedRequest = req;
    console.log(this.selectedRequest);
    this.loadDetailComponent(LeaveDetailsComponent, req);
    // this.cdr.detectChanges();
  }
}
