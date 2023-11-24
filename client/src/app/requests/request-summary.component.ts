import { Component, Input } from '@angular/core';
import { IRequestDetails } from '../domain/models/request';

@Component({
  selector: 'app-request-summary',
  template: `
    <h2>
      Request details
    </h2>
    <div class="form-container">
        <div class="column">
            <div class="field">
                <label for="name1">Type:</label>
                <label for="val1">Request</label>
            </div>
            <div class="field">
                <label for="value1">Request:</label>
                <label for="val2">{{requestDetails!.type!.templateName}}</label>
            </div>
        </div>

        <div class="column">
            <div class="field">
                <label for="name2">Created:</label>
                <label for="name2">{{requestDetails.requestDate | date}}</label>
            </div>
            <div class="field">
                <label for="value2">Closed:</label>
                <label for="name2">{{requestDetails.actions[requestDetails.actions.length-1].actionDate | date}}</label>
            </div>
        </div>

        <div class="column">
            <div class="field">
                <label for="name3">Status:</label>
                <label for="val3">{{requestDetails.entries.status}}</label>
            </div>
            <div class="field">
                <label for="value3">Cancellation:</label>
                <label for="val4">{{requestDetails.entries.cancellationStatus == "N"? "No" : "Yes"}}</label>
            </div>
        </div>
    </div>
    <h3>
      Summary
    </h3>
    <div class="summary-box">
        <div class="description">
            <strong>Overview</strong>
            <!-- Bind your description data here -->
            <p>{{requestDetails.description}}</p>
        </div>

        <div class="actions-summary">
          <strong>actions:</strong>
          <ng-template ngFor let-item [ngForOf]= requestDetails!.actions>
            <li>{{item.summary}}</li>
          </ng-template>
            <!-- Bind your actions summary list here -->
            <!-- <ul *ngFor="let item in requestDetails!.actions">
                <li></li>
            </ul> -->
        </div>
    </div>
  `,
  styles: [`
.form-container {
    display: flex;
    justify-content: space-around;
    margin: 20px;

    @media (max-width: 768px) {
        flex-direction: column;
    }
}

.column {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 20px;

    @media (max-width: 768px) {
        margin-bottom: 0;
    }

    .field {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 10px;

        label {
            margin-bottom: 5px;
        }

        input {
            padding: 5px;
        }

        @media (min-width: 768px) {
            flex-direction: row;

            label {
                margin-right: 10px;
                margin-bottom: 0;
            }

            input {
                margin-bottom: 0;
            }
        }
    }
}
.summary-box {
    margin: 20px;
    padding: 10px;
    border: 1px solid #ccc;
    max-height: 200px;
    overflow-y: auto;

    .description {
        margin-bottom: 10px;
    }

    .actions-summary {
        ul {
            list-style-type: none;
            padding: 0;

            li {
                margin-bottom: 5px;
            }
        }
    }
}
  `
  ]
})
export class RequestSummaryComponent {
  @Input() requestDetails!:IRequestDetails
}
