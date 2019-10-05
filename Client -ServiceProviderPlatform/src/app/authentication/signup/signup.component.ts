import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Signup } from '../../models/signup';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  signup: Signup;
  confirmPassword: string;
  isEnabled: boolean = false;
  private readonly notifier: NotifierService;
  errorOccured: boolean;
  categories: any;
  selectedCategory: any;

  constructor(private authentcationService: AuthenticationService, private router: Router, notifierService: NotifierService) {
    this.signup = new Signup();
    this.notifier = notifierService;
  }

  ngOnInit() {
    this.getAllCategories();
  }

  ngOnChanges(event) {
    if (this.signup.firstName != null && this.signup.lastName != null && this.signup.address != null
      && this.signup.companyName != null && this.signup.contactNumber != null && this.signup.email != null &&
      this.selectedCategory != null && this.signup.password != null) {
      this.isEnabled = true;
    } else {
      this.isEnabled = false;
    }
  }


  signUp() {
    this.signup.userRole = 2;
    this.signup.JobCategoryId = this.selectedCategory.Id;
    this.authentcationService.signup(this.signup).subscribe((result: any) => {
      console.log(result);
      if (result.HasError) {
        this.signup = null;
        this.errorOccured = true;
      } else {
        this.router.navigate(['login']);
      }
    });
  }

  getAllCategories() {
    this.authentcationService.getCategories().subscribe((categoryList: any) => {
      this.categories = categoryList;
      this.selectedCategory = this.categories[0];
    });
  }

}
