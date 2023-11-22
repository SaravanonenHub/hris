import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { RequestsRoutingModule } from './requests-routing.module';
import { MyRequestsComponent } from './my-requests.component';
import { OpenNewRequestComponent } from './open-new-request.component';
import { TableModule } from 'primeng/table';
import { RequestServicesComponent } from './request-services.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import {TimelineModule} from 'primeng/timeline'
import { DataViewModule } from 'primeng/dataview';
import { ReadMorePipe } from './pipes/read-more.pipe';
import { BrowserModule } from '@angular/platform-browser';
import { ReadMoreDirective } from './directives/read-more.directive';
import { RequestDetailsComponent } from './request-details/request-details.component';
import { RequestJourneyComponent } from './request-journey.component';
import { RequestSummaryComponent } from './request-summary.component';
import { SelectDirective } from './directives/select.directive';
import {TabViewModule} from 'primeng/tabview'

@NgModule({
  declarations: [
    MyRequestsComponent,
    OpenNewRequestComponent,
    RequestServicesComponent,
    ReadMorePipe,
    ReadMoreDirective,
    RequestDetailsComponent,
    RequestJourneyComponent,
    RequestSummaryComponent,
    SelectDirective
  ],
  imports: [
    CommonModule,
    RequestsRoutingModule,
    TableModule,
    CardModule,
    ButtonModule,
    DataViewModule,
    TimelineModule,
    TabViewModule 
  ]
})
export class RequestsModule { }
