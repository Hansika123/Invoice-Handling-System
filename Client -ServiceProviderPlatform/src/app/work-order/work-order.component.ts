import { Component, OnInit, Inject } from '@angular/core';
import { WorkorderService } from '../services/workorder.service';
import { WorkOrder } from '../models/workOrder';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-work-order',
  templateUrl: './work-order.component.html',
  styleUrls: ['./work-order.component.css']
})
export class WorkOrderComponent implements OnInit {

  workOrders: WorkOrder[] = [];
  selectedWorkOrder: WorkOrder = <WorkOrder>{};
  serviceProviderId: number;
  hasError: boolean;
  errorMessage: string;
  errorOccured: boolean;
  isUpdating: boolean;
  selectedValue: any;
  isDisable: boolean;

  constructor(private workOrderService: WorkorderService,
    @Inject(SESSION_STORAGE) private storage: StorageService, private router: Router) {
    this.selectedValue = 0;
    this.isDisable = true;
  }

  ngOnInit() {
    this.serviceProviderId = this.storage.get('serviceProviderId');
    this.getWorkOrders();
  }

  getWorkOrders() {
    this.workOrderService.getWorkOrderListByServiceProvider(this.serviceProviderId).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
        this.errorOccured = true;
      } else {
        this.workOrders = result.Entities;
      }
    });
  }

  updateStatus(selectedItem: WorkOrder) {
    this.isUpdating = true;
    this.selectedWorkOrder = selectedItem;
  }

  updateWorkOrder() {
    this.selectedWorkOrder.status = this.selectedValue;
    this.workOrderService.updateWorkOrderStatus(this.selectedWorkOrder).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
        this.errorOccured = true;
      } else {
        this.getWorkOrders();
        this.isUpdating = false;
      }
    });
  }

  setAsViewAll() {
    this.isUpdating = false;
  }
}
