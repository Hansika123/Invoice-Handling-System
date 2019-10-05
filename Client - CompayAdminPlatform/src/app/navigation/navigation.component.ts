import { Component, OnInit, Inject } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  isCaller: boolean;
  userFirstName: string;
  userLastName: string;
  quoteDetails: any;

  constructor(@Inject(SESSION_STORAGE) private storage: StorageService, private router: Router, private data: DataService) {
    this.userFirstName = this.storage.get('appUserFirstName');
    this.userLastName = this.storage.get('appUserLastName');
   }

  ngOnInit() {
    const userRoleId = this.storage.get('userRoleId');
    this.isCaller = userRoleId === 3 ? true : false;

    this.data.currentMessage.subscribe(message => this.quoteDetails = message);
  }

  logout() {
    this.storage.remove('userId');
    this.router.navigate(['login']);
  }

  gotToQuatationRequest() {
    this.data.changeQuatationRequestDetail(
      {
        isNewRequest: false,
        PropertyHolderName: '',
        PHPhoneNumber: '',
        PHAddress: ''
      });
    this.router.navigate(['requestquotation']);
  }

  goToWorkOrder() {
    this.data.changeWorkOrderRequestDetail(
      {
        isNewRequest: false,
        PropertyHolderName: '',
        PHPhoneNumber: '',
        PHAddress: ''
      });
    this.router.navigate(['workorder']);
  }
}
