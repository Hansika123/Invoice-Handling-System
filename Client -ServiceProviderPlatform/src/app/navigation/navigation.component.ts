import { Component, OnInit, Inject } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  userFirstName: string;
  userLastName: string;

  constructor(@Inject(SESSION_STORAGE) private storage: StorageService, private router: Router) {
    this.userFirstName = this.storage.get('appUserFirstName');
    this.userLastName = this.storage.get('appUserLastName');
  }

  ngOnInit() {
  }

  logout() {
    this.storage.remove('serviceProviderId');
    this.router.navigate(['login']);
  }

}
