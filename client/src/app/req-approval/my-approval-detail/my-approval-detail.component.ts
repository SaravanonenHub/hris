import { ChangeDetectorRef, Component, Type, ViewChild, ViewContainerRef } from '@angular/core';
import { IRequest, IRequestDetails } from 'src/app/domain/models/request';
import { ApprovalService } from '../approval.service';
import { LeaveDetailsComponent } from 'src/app/requests/leaves/leave-details/leave-details.component';
import { NgComponentOutlet } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Observable, firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-my-approval-detail',
  templateUrl: './my-approval-detail.component.html',
  styleUrls: ['./my-approval-detail.component.scss'],
})
export class MyApprovalDetailComponent {
  openApprovals: IRequest[] = [];
  selectedRequest?: IRequest;
  requestDetails$!: Observable<IRequestDetails>;
  @ViewChild('detailContainer', { read: ViewContainerRef }) detailContainer!: ViewContainerRef;
  leavedetailcom!: LeaveDetailsComponent;
  constructor(private service: ApprovalService, private cdr: ChangeDetectorRef, private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.service.getOpenApproval().then((val) => {
      val.subscribe((data) => {
        console.log(data);
        this.openApprovals = data
      });
    });
    const requestId = this.route.snapshot.queryParams['id'];
    this.service.getRequestDetail(requestId).subscribe((data) => {
      console.log(data);
      // this.selectedRequest = data
      this.loadDetailComponent(LeaveDetailsComponent, data);
    });
  }
  loadDetailComponent(componentType: Type<any>, req: IRequestDetails) {
    this.detailContainer.clear();
    const detailsComponent =
      this.detailContainer.createComponent(componentType);
    const inputs: Record<string, IRequestDetails> = { "request": req };
    detailsComponent.instance.request = req; // Set request after creation
  }
  loadComponent() {
    const result = this.service.getDetailComponent(this.selectedRequest!);
    console.log(result);
    return result;
  }
  async onRequestSelect(req: IRequest) {
    debugger;
    this.selectedRequest = req;
    console.log(this.selectedRequest);
    this.requestDetails$ = await this.service.getRequestDetail(req.id);
    const detailContent = await firstValueFrom(this.requestDetails$);
    this.loadDetailComponent(LeaveDetailsComponent, detailContent);
    // const details$ = await this.service.getRequestDetail(req.id).subscribe((data) => {
    //   console.log(data);
    //   // this.selectedRequest = data
    //   this.loadDetailComponent(LeaveDetailsComponent, data);
    // });
    //this.loadDetailComponent(LeaveDetailsComponent, req);
    // this.cdr.detectChanges();
  }
  onApprove(req: IRequest) {

    this.service.requestApprove(req).subscribe(() => {
      const index = this.openApprovals.findIndex(item => item.id === req.id);
      this.openApprovals[index].IsRemoving = true;
      setTimeout(() => {
        this.openApprovals.splice(index, 1);
      }, 500);
      console.log("Approved");
    })
  }
}
