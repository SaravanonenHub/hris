import { Component, Input } from '@angular/core';
import { IActionHistory, RequestActions } from '../domain/models/request';

@Component({
  selector: 'app-request-journey',
  template: `
    <ng-container *ngIf="actions" class="summary-box">
        <h2>{{actions[actions.length - 1].action}}</h2>
        <p>{{actions[actions.length - 1].actionDate | date}}
        <p>Request was realized successfully</p>
    </ng-container>
    <p-timeline [value]="requestAction" layout="horizontal" align="top">
    <ng-template pTemplate="content" let-event>
      <div [ngClass]="{'p-timeline-event': true, 'p-timeline-current': event}">
        <div class="p-timeline-event-content">
          {{event}}
        </div>
      </div>    
    </ng-template>
</p-timeline>
  `,
  styles: [`
  .summary-box {
    margin: 20px;
    padding: 10px;
    border: 1px solid #ccc;
    max-height: 200px;
    overflow-y: auto;
}`
  ]
})
export class RequestJourneyComponent {
  @Input() actions: IActionHistory[] = []
  requestAction=Object.values(RequestActions);
}
