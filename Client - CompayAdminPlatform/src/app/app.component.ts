import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'Invoice Handling';
  isLoggedIn: boolean;

  constructor(@Inject(SESSION_STORAGE) private storage: StorageService, private router: Router) {

    this.isLoggedIn = true;

    const userSession = this.storage.get('userId');
    if (!userSession) {
      this.isLoggedIn = false;
      this.router.navigate(['login']);
    } else {
      this.router.navigate(['dashboard']);
    }
  }

  onInit() {
  }
}
