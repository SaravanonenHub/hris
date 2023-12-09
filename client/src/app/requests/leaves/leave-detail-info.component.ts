import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { IRequestDetails } from 'src/app/domain/models/request';

@Component({
  selector: 'app-leave-detail-info',
  template: `
  <ng-container *ngIf="request$ | async as request">
  <section class="info">
      <div class="ticketNumber">
          <div class="info-label">
              <label class="label-key">Number</label>
              <label>{{request.requestId}}</label>
          </div>
      </div>
      <div class="infoDayStatus">
          <div class="info-label">
              <label class="label-key">Created</label>
              <label>{{request.requestDate}}</label>
          </div>
          <div class="info-label">
              <label class="label-key">Updated</label>
              <label>{{request.requestDate}}</label>
          </div>
          <div class="info-label">
              <label class="label-key">State</label>
              <label>{{request.currentState}}</label>
          </div>
      </div>

  </section>
  <section class="title">
      <header>{{request.type.templateName}}</header>
      <div class="info">
          <div class="info-label">
              <label class="label-key">Leave ID</label>
              <label>{{request.requestId}}</label>
          </div>
          <div class="info-label">
              <label class="label-key">Requested for</label>
              <label>{{request.employee.displayName}}</label>
          </div>
      </div>
  </section>
  <section class="details">
      <div class="info-label">
          <label class="label-key">Requested for</label>
          <label>{{request.employee.displayName}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Leave Type</label>
          <label>{{request.entries.leaveType}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Applied days</label>
          <label>{{request.entries.days}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">From Date</label>
          <label>{{request.entries.fromDate}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">To Date</label>
          <label>{{request.entries.toDate}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Session</label>
          <label>{{request.entries.session}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Status</label>
          <label>{{request.entries.status}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Cancell Status</label>
          <label>{{request.entries.cancellationStatus}}</label>
      </div>
      <div class="info-label">
          <label class="label-key">Reason</label>
          <label>{{request.entries.reason}}</label>
      </div>
  </section>
</ng-container>
  `,
  styles: [`.info {
    display: flex;
    flex-direction: row;

    .ticketNumber {

        display: flex;
        justify-content: flex-start;
        /* Change to flex-end to align on the right */

        /* Align items to the top */
    }

    .infoDayStatus {
        flex-grow: 1;
        display: flex;
        justify-content: flex-end;
        align-items: flex-start;
    }

    label {
        display: block;
    }
}

.info-label {
    margin-left: 0.5rem;
    margin-right: 0.5rem;
}

.label-key {
    color: gray;
    font-weight: bold;
}

.title {
    height: 8rem;
    border: 0.5px solid black;

    header {
        background-color: dodgerblue;
        color: white;
        font-size: large;
        padding-left: 0.5rem;
    }
}

.details {
    margin-top: 2rem;
    height: auto;
    border: 0.5px solid silver;

    .info-label {
        margin-bottom: 1rem;
    }

    label {
        display: block;
    }
}`
  ]
})
export class LeaveDetailInfoComponent {
  @Input() request$!: Observable<IRequestDetails>
}
