import { InvoiceDetail } from './../models/invoiceDetail';
import { InvoiceService } from './../services/invoice.service';
import { TaskDetail } from './../models/taskDetail';
import { TaskService } from './../services/task.service';
import { ActivityLogDetail } from './../models/activityLogDetail';
import { ActivitylogService } from './../services/activitylog.service';
import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  hasError: boolean;
  errorMessage: string;
  activityList: ActivityLogDetail[] = [];
  taskDetails: TaskDetail[] = [];
  invoiceDetails: InvoiceDetail[] = [];
  size = 5;

  constructor(private router: Router, @Inject(SESSION_STORAGE) private storage: StorageService,
    private activitylogService: ActivitylogService, private taskService: TaskService,
    private invoiceService: InvoiceService) {

    const userRoleId = this.storage.get('userRoleId');
    if (userRoleId === 3) {
      this.router.navigate(['task']);
    }
  }

  ngOnInit() {
    this.getAllActivityLog();
    this.getPaymentStatus();
    this.getUpdatedTask();
  }

  getAllActivityLog() {
    this.activitylogService.getAllActivities().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        if (result) {
          this.activityList = result.slice(0, this.size);
        }
      }
    });
  }

  getPaymentStatus() {
    this.invoiceService.getAllInvoices().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        if (result) {
          this.invoiceDetails = result.slice(0, this.size);
        }
      }
    });
  }

  getUpdatedTask() {
    this.taskService.getAllTasks().subscribe(result => {
      if (result) {
        this.taskDetails = result.slice(0, this.size);
      }
    });
  }
}
