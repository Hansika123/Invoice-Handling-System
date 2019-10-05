import { FeedbackComponent } from './feedback/feedback.component';
import { UserAccountComponent } from './user-account/user-account.component';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivityLogComponent } from '../app/activity-log/activity-log.component';
import { DashboardComponent } from '../app/dashboard/dashboard.component';
import { InvoiceComponent } from '../app/invoice/invoice.component';
import { TaskComponent } from '../app/task/task.component';
import { WorkOrderComponent } from '../app/work-order/work-order.component';
import { SignupComponent } from './authentication/signup/signup.component';
import { LoginComponent } from './authentication/login/login.component';
import { ErrorHandlingComponent } from '../app/error-handling/error-handling.component';
import { LandingPageComponent } from '../app/landing-page/landing-page.component';
import { QuotationReceivedComponent } from '../app/quotation/quotation-received/quotation-received.component';
import { QuotationRquestComponent } from '../app/quotation/quotation-rquest/quotation-rquest.component';
import { AuthGuardService as AuthGuard } from '../app/services/auth/auth-guard.service';

const routes: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'signup', component: SignupComponent
  },
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'home', component: LandingPageComponent, canActivate: [AuthGuard] },
      { path: 'activitylog', component: ActivityLogComponent, canActivate: [AuthGuard] },
      { path: 'receivedquotation', component: QuotationReceivedComponent, canActivate: [AuthGuard] },
      { path: 'requestquotation', component: QuotationRquestComponent, canActivate: [AuthGuard] },
      { path: 'task', component: TaskComponent, canActivate: [AuthGuard] },
      { path: 'workorder', component: WorkOrderComponent, canActivate: [AuthGuard] },
      { path: 'error', component: ErrorHandlingComponent, canActivate: [AuthGuard] },
      { path: 'invoice', component: InvoiceComponent, canActivate: [AuthGuard] },
      { path: 'useraccount', component: UserAccountComponent, canActivate: [AuthGuard] },
      { path: 'feedback', component: FeedbackComponent, canActivate: [AuthGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
