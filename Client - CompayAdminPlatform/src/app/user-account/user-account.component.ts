import { ServiceProviderDetail } from './../models/ServiceProviderDetail';
import { UserService } from './../services/user.service';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  hasError: boolean;
  errorMessage: string;
  userList: ServiceProviderDetail[] = [];
  userDetail: ServiceProviderDetail = <ServiceProviderDetail>{};
  isViewMode: boolean;

  constructor(private userService: UserService) {
    this.isViewMode = false;
  }

  ngOnInit() {
    this.getAllUser();
  }

  getAllUser() {
    this.userService.getAllUsers().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.userList = result;
      }
    });
  }

  viewUserDetails(user: ServiceProviderDetail) {
    this.userDetail = user;
    console.log(this.userDetail);
    this.isViewMode = true;
  }

  goBack() {
    this.isViewMode = false;
  }
}
