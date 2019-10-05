import { ActivitylogService } from './../services/activitylog.service';
import { Router } from '@angular/router';
import { ActivityLogDetail } from '../models/activityLogDetail';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-activity-log',
  templateUrl: './activity-log.component.html',
  styleUrls: ['./activity-log.component.css']
})
export class ActivityLogComponent implements OnInit {

  activityList: ActivityLogDetail[] = [];
  hasError: boolean;
  errorMessage: string;

  constructor(private router: Router, private activitylogService: ActivitylogService) { }

  ngOnInit() {
    this.getAllActivities();
  }   

  getAllActivities() {
    this.activitylogService.getAllActivities().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.activityList = result;
      }
    });
  }

}
