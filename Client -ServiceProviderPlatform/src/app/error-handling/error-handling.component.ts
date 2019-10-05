import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-error-handling',
  templateUrl: './error-handling.component.html',
  styleUrls: ['./error-handling.component.css']
})
export class ErrorHandlingComponent implements OnInit {
  message: string;
  constructor(private router: Router,private route: ActivatedRoute) { }

  ngOnInit() {
   this.message = this.route.snapshot.paramMap.get('message');
    if(this.message == null)
    {
      this.message = 'Internal error occured. Please try again.';
    }
  }

}
