import { ServiceProviderService } from './../services/service-provider.service';
import { TaskService } from './../services/task.service';
import { Component, OnInit, Inject } from '@angular/core';
import { TaskDetail } from '../models/taskDetail';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  taskDetails: TaskDetail[] = [];
  taskDetail: TaskDetail = <TaskDetail>{};
  isVisible = false;
  userRoleId: number;
  callerId: number;
  isCaller: boolean;
  isNewTask: boolean;
  selectedValue: any;
  quoteDetails: any;
  categories: any;
  selectedCategory: any;
  private todate = new Date();

  constructor(private router: Router, private taskService: TaskService, @Inject(SESSION_STORAGE) private storage: StorageService,
    private data: DataService, private serviceProviderService: ServiceProviderService) {

    this.userRoleId = this.storage.get('userRoleId');
    this.callerId = this.storage.get('userId');
    this.isCaller = this.userRoleId === 3 ? true : false;
    this.selectedValue = 0;
  }

  ngOnInit() {
    // if (this.isCaller) {
    //   this.getTasksByCallerId(this.callerId);
    // } else {
    //   this.getAllTasks();
    // }
    this.getAllCategories();
    this.getAllTasks();
    this.data.currentMessage.subscribe(message => this.quoteDetails = message);
  }

  getAllTasks() {
    this.taskService.getAllTasks().subscribe(result => {
      this.taskDetails = result;

      this.taskDetails.map((item, index) => {
        if (!item.WorkOrderDetail.Status) {
          item.WorkOrderDetail.Status = 0;
        }
      });


      console.log(result);
    });
  }

  getTasksByCallerId(userId: number) {
    this.taskService.getTasksByCallerSystemId(userId).subscribe(result => {
      this.taskDetails = result;
    });
  }

  setUpdateTask(task: TaskDetail) {
    this.isVisible = true;
    this.taskDetail = task;
  }

  setCreateNewTask() {
    this.isNewTask = true;
  }

  setAsViewAll() {
    this.isNewTask = false;
    this.isVisible = false;
  }

  createNewTask() {

    this.taskDetail.CallerSystemUserId = this.callerId;
    this.taskDetail.AssignStatus = '0';
    this.taskDetail.JobCategoryId = this.selectedCategory.Id;
    this.taskService.createTask(this.taskDetail).subscribe((result: any) => {

      if (result.HasError) {
        this.router.navigate(['error']);
      } else {
        this.getTasksByCallerId(this.callerId);
      }
      this.isNewTask = false;
    });
  }

  updateTask() {

    this.taskDetail.CallerSystemUserId = this.callerId;
    this.taskDetail.AssignStatus = this.selectedValue;
    this.taskService.updateTask(this.taskDetail).subscribe((result: any) => {

      if (result.HasError) {
        this.router.navigate(['error']);
      } else {
        this.getAllTasks();
      }
      this.isVisible = false;
    });
  }

  gotToQuatationRequest() {
    this.data.changeQuatationRequestDetail(
      {
        isNewRequest: true,
        PropertyHolderName: this.taskDetail.PropertyHolderName,
        PHPhoneNumber: this.taskDetail.PHPhoneNumber,
        PHAddress: this.taskDetail.PHAddress,
        TaskId: this.taskDetail.Id
      });
    this.router.navigate(['requestquotation']);
  }

  goToWorkOrder() {
    this.data.changeWorkOrderRequestDetail(
      {
        isNewRequest: true,
        PropertyHolderName: this.taskDetail.PropertyHolderName,
        PHPhoneNumber: this.taskDetail.PHPhoneNumber,
        PHAddress: this.taskDetail.PHAddress,
        TaskId: this.taskDetail.Id
      });
    this.router.navigate(['workorder']);
  }

  getAllCategories() {
     this.serviceProviderService.getCategories().subscribe((categoryList: any) => {
      this.categories = categoryList;
      this.selectedCategory = this.categories[0];
    });
  }
}
