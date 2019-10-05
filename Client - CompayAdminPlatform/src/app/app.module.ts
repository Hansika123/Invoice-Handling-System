import { FeedbackComponent } from './feedback/feedback.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { LayoutComponent } from './layout/layout.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { StorageServiceModule } from 'angular-webstorage-service';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InvoiceComponent } from './invoice/invoice.component';
import { WorkOrderComponent } from './work-order/work-order.component';
import { ActivityLogComponent } from './activity-log/activity-log.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TaskComponent } from './task/task.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './authentication/signup/signup.component';
import { LoginComponent } from './authentication/login/login.component';
import { FormsModule } from '@angular/forms';
import { ErrorHandlingComponent } from './error-handling/error-handling.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { NavigationComponent } from './navigation/navigation.component';
import { QuotationReceivedComponent } from './quotation/quotation-received/quotation-received.component';
import { QuotationRquestComponent } from './quotation/quotation-rquest/quotation-rquest.component';
import { NotifierModule } from 'angular-notifier';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { AuthService } from './services/auth/auth.service';
import { AuthStatusNotifierService } from './services/auth/auth-status-notifier.service';
import { DataService } from './services/data.service';

@NgModule({
  declarations: [
    AppComponent,
    InvoiceComponent,
    WorkOrderComponent,
    ActivityLogComponent,
    DashboardComponent,
    TaskComponent,
    SignupComponent,
    LoginComponent,
    ErrorHandlingComponent,
    LandingPageComponent,
    NavigationComponent,
    QuotationReceivedComponent,
    QuotationRquestComponent,
    LayoutComponent,
    UserAccountComponent,
    FeedbackComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StorageServiceModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NotifierModule
  ],
  providers: [AuthGuardService, AuthService, AuthStatusNotifierService, DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
