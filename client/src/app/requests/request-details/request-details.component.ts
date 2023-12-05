import { Component, OnInit } from '@angular/core';
import { RequestService } from '../request.service';
import { ActivatedRoute } from '@angular/router';
import { IRequestDetails } from 'src/app/domain/models/request';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.scss']
})
export class RequestDetailsComponent implements OnInit {
  id!: number;
  requestDetails?: IRequestDetails
  constructor(private service: RequestService, private route: ActivatedRoute) { }
  ngOnInit(): void {

    this.id = this.route.snapshot.params['id'];
    if (this.id != null) {
      this.service.getRequestDetail(this.id).subscribe((data) => {
        this.requestDetails = data;
        console.log(data);
      })
    }

  }

}
