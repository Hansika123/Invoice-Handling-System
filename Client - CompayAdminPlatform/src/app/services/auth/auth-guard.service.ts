import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from './auth.service';
import { AuthStatusNotifierService } from './auth-status-notifier.service';

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private auth: AuthService, public router: Router, private authStatusNotifier: AuthStatusNotifierService) { }

  canActivate(): boolean {

    if (!this.auth.isAuthenticated()) {
      this.authStatusNotifier.setAuthStatus(false);
      this.router.navigate(['login']);
      return false;
    } else {
      this.authStatusNotifier.setAuthStatus(true);
      return true;
    }

  }
}
