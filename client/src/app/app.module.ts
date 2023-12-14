import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AutoCompleteModule } from 'primeng/autocomplete';
import { TooltipModule } from 'primeng/tooltip';
import { TableModule } from 'primeng/table'
import { DropdownModule } from 'primeng/dropdown'
import { RadioButtonModule } from 'primeng/radiobutton'
import { CalendarModule } from 'primeng/calendar'
import { SplitterModule } from 'primeng/splitter'
import { ToastModule } from 'primeng/toast'

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppFooterComponent } from './app.footer.component';
import { AppMainComponent } from './app.main.component';
import { AppMenuComponent } from './app.menu.component';
import { AppTopBarComponent } from './app.topbar.component';
import { AppConfigService } from './service/appconfigservice';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'
import { InputTextModule } from 'primeng/inputtext';

import { CommonModule } from '@angular/common';
import { EmployeeService } from './employees/employee.service';
import { EmployeesModule } from './employees/employees.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { UserInterceptor } from './core/interceptors/user-interceptor.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faHeart } from '@fortawesome/free-regular-svg-icons';
// import { StoreModule } from '@ngrx/store';
// import { StoreDevtoolsModule } from '@ngrx/store-devtools';
// import { EffectsModule } from '@ngrx/effects';
@NgModule({
  declarations: [
    AppComponent,
    AppMainComponent,
    AppFooterComponent,
    AppMenuComponent,
    AppTopBarComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    AutoCompleteModule,
    FormsModule,
    ReactiveFormsModule,
    TooltipModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    TableModule,
    DropdownModule,
    HttpClientModule,
    InputTextModule,
    RadioButtonModule,
    CalendarModule,
    SplitterModule,
    CoreModule,
    SharedModule,
    ToastModule,
    AvatarModule,
    // StoreModule.forRoot({}),
    // EffectsModule.forRoot(),
    //  StoreDevtoolsModule.instrument({
    //   maxAge: 25, // Retains last 25 states
    //   logOnly: !isDevMode(), // Restrict extension to log-only mode
    //   autoPause: true, // Pauses recording actions and state changes when the extension window is not open
    //   trace: false, //  If set to true, will include stack trace for every dispatched action, so you can see it in trace tab jumping directly to that part of code
    //   traceLimit: 75, // maximum stack trace frames to be stored (in case trace option was provided as true)
    //   //connectOutsideZone: true // If set to true, the connection is established outside the Angular zone for better performance
    // }),
  ],
  providers: [AppConfigService, EmployeeService, MessageService
    , { provide: HTTP_INTERCEPTORS, useClass: UserInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    // Add the icons you want to use to the library
    library.add(faHeart);
  }
 }
