import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { IRequest } from 'src/app/domain/models/request';
import { ApprovalService } from '../approval.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-my-approval',
  templateUrl: './my-approval.component.html',
  styleUrls: ['./my-approval.component.scss']
})
export class MyApprovalComponent {
  constructor(private router: Router) { }
  onRequestClick(req: IRequest) {
    this.router.navigate(['./approval/approve'], { queryParams: { 'id': req.id } })
  }
}
