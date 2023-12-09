import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IRequest, IRequestDetails } from 'src/app/domain/models/request';
import { RequestService } from '../../request.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-leave-details',
  templateUrl: './leave-details.component.html',
  styleUrls: ['./leave-details.component.scss']
})
export class LeaveDetailsComponent implements OnInit {
  // @Input() request!: IRequestDetails;
  requestId!: number;
  request$!: Observable<IRequestDetails>
  constructor(private route: ActivatedRoute, private service: RequestService) {
    this.route.queryParams.subscribe(params => {
      this.requestId = params['requestId'];
      // Now you can use inputProperty in your component.
    });
    // Now you can use inputProperty in your component.
  }
  ngOnInit(): void {
    if (this.requestId) {
      this.request$ = this.service.getRequestDetail(this.requestId);
    }

  }
}
