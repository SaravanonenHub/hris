import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AutoCompleteModule } from 'primeng/autocomplete';
import { TooltipModule } from 'primeng/tooltip';
import { TableModule } from 'primeng/table'
import { DropdownModule } from 'primeng/dropdown'
import { RadioButtonModule } from 'primeng/radiobutton'
import { CalendarModule } from 'primeng/calendar'
import { SplitterModule } from 'primeng/splitter'

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppFooterComponent } from './app.footer.component';
import { AppMainComponent } from './app.main.component';
import { AppMenuComponent } from './app.menu.component';
import { AppTopBarComponent } from './app.topbar.component';
import { AppConfigService } from './service/appconfigservice';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http'
import { InputTextModule } from 'primeng/inputtext';

import { CommonModule } from '@angular/common';
import { EmployeeService } from './employees/employee.service';
import { EmployeesModule } from './employees/employees.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
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
    SharedModule
  ],
  providers: [AppConfigService, EmployeeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
