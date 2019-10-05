import { ServiceProviderDetail } from './../models/ServiceProviderDetail';
import { ServiceProviderService } from './../services/service-provider.service';
import { WorkorderService } from './../services/workorder.service';
import { Component, OnInit } from '@angular/core';
import { WorkOrderDetail } from '../models/workOrder';
import { Router } from '@angular/router';
import { DataService } from '../services/data.service';
import { isUndefined } from 'util';

@Component({
  selector: 'app-work-order',
  templateUrl: './work-order.component.html',
  styleUrls: ['./work-order.component.css']
})
export class WorkOrderComponent implements OnInit {

  workOrderList: WorkOrderDetail[] = [];
  workOrder: WorkOrderDetail = <WorkOrderDetail>{};
  isNewRequest: boolean;
  hasError: boolean;
  errorMessage: string;
  serviceProviders: ServiceProviderDetail[] = [];
  selectedServiceProvider: any;
  selectedValue: any;
  workOrderDetails: any;
  private todate = new Date();

  constructor(private router: Router, private workOrderService: WorkorderService,
    private serviceProviderService: ServiceProviderService, private data: DataService) {
    this.selectedValue = 0;
  }

  ngOnInit() {
    this.getAllWorkOrders();
    this.getAllServiceProviders();

    this.data.currentMessage.subscribe(message => this.workOrderDetails = message);
    this.isNewRequest = this.workOrderDetails.isNewRequest;
  }

  getAllWorkOrders() {
    this.workOrderService.getAllWorkOrders().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.workOrderList = result;
      }
    });
  }

  setAsCreateNew() {
    this.isNewRequest = true;
    this.workOrder = <WorkOrderDetail>{};
  }

  setAsViewAll() {
    this.isNewRequest = false;
  }

  createWorkOrder() {
    // 0: not accepted, 1: accepted
    var valid: Boolean = true;
    this.workOrder.TaskId = this.workOrderDetails.TaskId;
    this.workOrder.Status = this.selectedValue;
    this.workOrder.ServiceProviderID = this.selectedServiceProvider.Id;
    this.workOrder.PropertyHolderName = this.workOrderDetails.PropertyHolderName;
    this.workOrder.PHAddress = this.workOrderDetails.PHAddress;
    this.workOrder.PHPhoneNumber = this.workOrderDetails.PHPhoneNumber;

    if(this.workOrder.Description == ''||isUndefined(this.workOrder.Description)||this.workOrder.PropertyHolderName==''||isUndefined(this.workOrder.PropertyHolderName)||
     this.workOrder.PHAddress==''||isUndefined(this.workOrder.PHAddress)||this.workOrder.PHPhoneNumber==''||isUndefined(this.workOrder.PHPhoneNumber)||
     this.workOrder.ServiceProviderID==null||isUndefined(this.workOrder.ServiceProviderID)||this.workOrder.DueDate==null||isUndefined(this.workOrder.DueDate))
     { 
       valid = false;
     } 
     
     if(valid){
      this.workOrderService.createWorkOrder(this.workOrder).subscribe((result: any) => {

      this.isNewRequest = false;
      if (result.HasError) {
        this.router.navigate(['error']);
      } else {
        this.getAllWorkOrders();
      }
    });
  }
 }

  getAllServiceProviders() {
    this.serviceProviderService.getServiceProviders().subscribe((serviceProviders: any) => {
      this.serviceProviders = serviceProviders;
      console.log(this.serviceProviders);
      this.selectedServiceProvider = this.serviceProviders[0];
    });
  }

}
