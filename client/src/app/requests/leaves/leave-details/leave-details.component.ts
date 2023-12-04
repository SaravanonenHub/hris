import { Component, Input } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';

@Component({
  selector: 'app-leave-details',
  templateUrl: './leave-details.component.html',
  styleUrls: ['./leave-details.component.scss']
})
export class LeaveDetailsComponent {
  @Input() request!: IRequest;
}
