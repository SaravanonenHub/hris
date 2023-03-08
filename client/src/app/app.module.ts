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
import { TestComponent } from './test/test.component';
import { TestOneComponent } from './test-one/test-one.component'
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { HttpClientModule } from '@angular/common/http'
import { InputTextModule } from 'primeng/inputtext';
import { EmployeesModule } from './employees/employees.module';
@NgModule({
  declarations: [
    AppComponent,
    AppMainComponent,
    AppFooterComponent,
    AppMenuComponent,
    AppTopBarComponent,
    TestComponent,
    TestOneComponent
  ],
  imports: [
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
    EmployeesModule
  ],
  providers: [AppConfigService],
  bootstrap: [AppComponent]
})
export class AppModule { }
