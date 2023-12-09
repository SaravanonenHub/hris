import { NgModule, isDevMode } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DropdownModule } from 'primeng/dropdown'
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { LeavesRoutingModule } from './leaves-routing.module';
import { LeaveRequestsComponent } from './leave-requests.component';
import { CreateRequestsComponent } from './create-requests/create-requests.component';
import { CalendarModule } from 'primeng/calendar';
import {ColorPickerModule} from 'primeng/colorpicker'
import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';
import { FieldsetModule } from 'primeng/fieldset';
import { LeaveDetailsComponent } from './leave-details/leave-details.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { LeaveDetailInfoComponent } from './leave-detail-info.component';
// import { StoreModule } from '@ngrx/store';
// import { StoreDevtoolsModule } from '@ngrx/store-devtools';
// import { reducer } from './store/reducers';
// import { LeaveEffect } from './store/effects';
// import { EffectsModule } from '@ngrx/effects';
@NgModule({
  declarations: [
    LeaveRequestsComponent,
    CreateRequestsComponent,
    LeaveDetailsComponent,
    LeaveDetailInfoComponent,

  ],
  imports: [
    CommonModule,
    LeavesRoutingModule,
    DropdownModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    CalendarModule,
    FileUploadModule,
    TableModule,
    FieldsetModule,
    RadioButtonModule
    // StoreModule.forFeature('leaves',reducer),
    // EffectsModule.forFeature([LeaveEffect])
  ]
})
export class LeavesModule { }
