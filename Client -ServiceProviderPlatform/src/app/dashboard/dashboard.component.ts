import { InvoiceService } from './../services/invoice.service';
import { WorkOrder } from './../models/workOrder';
import { Quotation } from './../models/quotation';
import { WorkorderService } from './../services/workorder.service';
import { ServiceProvider } from './../models/serviceProvider';
import { QuotationService } from './../services/quotation.service';
import { InvoiceDetail } from './../models/invoiceDetail';
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
  invoiceDetails: InvoiceDetail[] = [];
  quotation: Quotation[] = [];
  workOrders: WorkOrder[] = [];
  invoices: InvoiceDetail[] = [];
  size = 5;
  serviceProviderId: number;

  constructor(private route: Router, @Inject(SESSION_STORAGE) private storage: StorageService,
    private quotationService: QuotationService, private workorderService: WorkorderService,
    private invoiceService: InvoiceService) {

    this.serviceProviderId = this.storage.get('serviceProviderId');
  }

  ngOnInit() {
    this.getQuatations();
    this.getWorkOrders();
    this.getInvoices();
  }

  getQuatations() {
    this.quotationService.GetAllQuatationsByServiceProviderId(this.serviceProviderId).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        if (result) {
          this.quotation = result.slice(0, this.size);
        }
      }
    });
  }

  getWorkOrders() {
    this.workorderService.getWorkOrderListByServiceProvider(this.serviceProviderId).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        if (result) {
          this.workOrders = result.Entities.slice(0, this.size);
        }
      }
    });
  }

  getInvoices() {
    this.invoiceService.getAllInvoiceByServiceProviderId(this.serviceProviderId).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        if (result) {
          this.invoices = result.Entities.slice(0, this.size);
        }
      }
    });
  }

}
