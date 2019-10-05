import { BankAccountComponent } from './bank-account/bank-account.component';
import { AuthStatusNotifierService } from './services/auth/auth-status-notifier.service';
import { AuthService } from './services/auth/auth.service';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { StorageServiceModule } from 'angular-webstorage-service';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { WorkOrderComponent } from './work-order/work-order.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './authentication/signup/signup.component';
import { LoginComponent } from './authentication/login/login.component';
import { FormsModule } from '@angular/forms';
import { ErrorHandlingComponent } from './error-handling/error-handling.component';
import { NavigationComponent } from './navigation/navigation.component';
import { QuotationComponent } from './quotation/quotation.component';
import { LayoutComponent } from './layout/layout.component';
import { NotifierModule } from 'angular-notifier';
import {SnotifyModule, SnotifyService, ToastDefaults} from 'ng-snotify';


@NgModule({
  declarations: [
    AppComponent,
    InvoiceComponent,
    WorkOrderComponent,
    DashboardComponent,
    SignupComponent,
    LoginComponent,
    ErrorHandlingComponent,
    NavigationComponent,
    QuotationComponent,
    LayoutComponent,
    BankAccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StorageServiceModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NotifierModule,
    SnotifyModule
  ],
  providers: [AuthGuardService, AuthService, AuthStatusNotifierService,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults},
    SnotifyService],
  bootstrap: [AppComponent]
})
export class AppModule { }
