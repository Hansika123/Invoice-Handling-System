import { BankAccountComponent } from './bank-account/bank-account.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from '../app/dashboard/dashboard.component';
import { InvoiceComponent } from '../app/invoice/invoice.component';
import { WorkOrderComponent } from '../app/work-order/work-order.component';
import { SignupComponent } from './authentication/signup/signup.component';
import { LoginComponent } from './authentication/login/login.component';
import { ErrorHandlingComponent } from '../app/error-handling/error-handling.component';
import { QuotationComponent } from './quotation/quotation.component';
import { LayoutComponent } from './layout/layout.component';
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
      { path: 'quotation', component: QuotationComponent, canActivate: [AuthGuard] },
      { path: 'workorder', component: WorkOrderComponent, canActivate: [AuthGuard] },
      { path: 'error/:message', component: ErrorHandlingComponent, canActivate: [AuthGuard] },
      { path: 'invoice', component: InvoiceComponent, canActivate: [AuthGuard] },
      { path: 'bankaccount', component: BankAccountComponent, canActivate: [AuthGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
