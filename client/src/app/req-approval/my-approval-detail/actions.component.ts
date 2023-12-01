import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IRequest } from 'src/app/domain/models/request';

@Component({
  selector: 'app-actions',
  template: `
    <p>
      {{request.requestId}}
    </p>
  `,
  styles: [
  ]
})
export class ActionsComponent {
  @Input() request!:IRequest
  //@Output() requestSelect!:EventEmitter<IRequest>;

}
