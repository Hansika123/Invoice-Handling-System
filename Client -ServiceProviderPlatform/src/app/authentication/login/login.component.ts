import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { AuthenticationService } from '../../services/authentication.service';
import { Login } from '../../models/login';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;
  hasError: boolean;
  errorMessage: string;
  errorOccured: boolean;
  private readonly notifier: NotifierService;

  constructor(private authentcationService: AuthenticationService, private router: Router,
    @Inject(SESSION_STORAGE) private storage: StorageService, notifierService: NotifierService) {
    this.login = new Login();
    this.notifier = notifierService;
  }

  ngOnInit() {
  }

  signin() {
    this.authentcationService.login(this.login).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
        this.errorOccured = true;
      } else if (result.Entity == null) {
        this.errorOccured = true;

      } else if (result.Entity.ID === 0) {
        this.hasError = true;
        this.login = null;
        this.errorOccured = true;
      } else {
        if (result.Entity.ID != null && result.Entity.UserRole != null) {
          this.storage.set('userId', result.Entity.ID);
          this.storage.set('appUserFirstName', result.Entity.FirstName);
          this.storage.set('appUserLastName', result.Entity.LastName);
          this.storage.set('userRoleId', result.Entity.UserRole);
          this.storage.set('serviceProviderId', result.Entity.ServiceProviderId);
          this.storage.set('serviceProviderCategoryId', result.Entity.ServiceProviderCategoryId);
          this.router.navigate(['dashboard']);
        }
      }
    });
  }

}
