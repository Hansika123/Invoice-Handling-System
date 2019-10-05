import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
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
  private readonly notifier: NotifierService;

  constructor(private authentcationService: AuthService, private router: Router, notifierService: NotifierService) {
    this.signup = new Signup();
    this.notifier = notifierService;
  }

  ngOnInit() {
  }

  signUp() {
    this.signup.userRole = 3;
    this.authentcationService.signup(this.signup).subscribe((result: any) => {
      console.log(result);
      if (result.HasError) {
        this.signup = null;
        this.notifier.notify('Error', 'Internal Server Error Occured !');

      } else {
        this.router.navigate(['login']);
      }
    });
  }

}
