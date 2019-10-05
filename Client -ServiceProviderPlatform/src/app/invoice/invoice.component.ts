import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceService } from '../services/invoice.service';
import { InvoiceDetail } from '../models/invoiceDetail';
import { NotifierService } from 'angular-notifier';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Item } from '../models/item';
import { parse } from 'querystring';
import { isUndefined } from 'util';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent implements OnInit {

  private readonly notifier: NotifierService;
  hasError: boolean;
  errorMessage: string;
  invoiceDetails: InvoiceDetail[] = [];
  invoice: InvoiceDetail;
  serviceProviderId: number;
  item: Item;
  userId: number;
  userRoleId: number;
  itemList: Array<Item> = [];
  isNewRequest: boolean;
  errorOccured: boolean;
  
  constructor(private invoiceService: InvoiceService, private router: Router, notifierService: NotifierService,
    @Inject(SESSION_STORAGE) private storage: StorageService ) {
    this.invoice = new InvoiceDetail();
    this.notifier = notifierService;
    this.item = new Item();
    this.invoice.subTotal = 0.00;
  }

  ngOnInit() {
    this.userId = this.storage.get('userId');
    this.userRoleId = this.storage.get('userRoleId');
    this.invoice.serviceProvideId = this.storage.get('serviceProviderId');
    if (this.userId != null && this.userRoleId === 2) {
      this.getAllInvoices();
    } else {
      this.router.navigate(['home']);
    }
  }

  getAllInvoices() {
    this.invoiceService.getAllInvoiceByServiceProviderId(this.invoice.serviceProvideId).subscribe((result: any) => {
      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;

        this.errorOccured = true;

      } else {
        this.invoiceDetails = result.Entities;
      }
    });
  }

  setAsCreateNew() {
    this.isNewRequest = true;
    this.invoice.description = null;
    this.invoice.invoiceSubject = null;
    this.item.itemDescription = null;
    this.item.itemName = null;
    this.item.estimatedQuentity = null;
    this.item.unitPrice = null;
    this.invoice.subTotal = null;
    this.invoice.serviceFee = null;
    this.invoice.total = null;
  }
  setAsViewAll() {
    this.isNewRequest = false;
  }

  createQuotationModel(item) {
    this.invoice = item;
    this.isNewRequest = true;
  }

  addItems(item) {
    const tableItem = new Item();
    tableItem.itemCode = item.itemCode;
    tableItem.itemName = item.itemName;
    tableItem.unitPrice = item.unitPrice;
    tableItem.itemDescription = item.itemDescription;
    tableItem.estimatedQuentity = item.estimatedQuentity;
    this.invoice.quentity = item.estimatedQuentity;

    if(tableItem.unitPrice != null){
    this.itemList.push(tableItem);
    this.invoice.items = this.itemList;
    this.item.unitPrice = null;
    this.item.itemName = null;
    this.item.estimatedQuentity = null;
    this.item.itemDescription = null;
    }
    else{
      alert("Please provide valid details");
    }
    this.invoice.subTotal = 0;

    this.invoice.items.forEach(
      p => this.invoice.subTotal = (parseFloat(p.unitPrice.toPrecision()) * parseFloat(p.estimatedQuentity.toPrecision())) + 
      parseFloat(this.invoice.subTotal.toString())
    );
    this.invoice.total = parseFloat(this.invoice.subTotal.toString()) + parseFloat(this.invoice.serviceFee.toString());
    this.item.itemName = null;
  }

  createInvoice() {
    var valid: Boolean = true;

    if(this.invoice.description == ''||isUndefined(this.invoice.description)||this.invoice.invoiceSubject==''||isUndefined(this.invoice.invoiceSubject)||
    this.invoice.quentity==null||isUndefined(this.invoice.quentity)||this.invoice.serviceFee==null||
    isUndefined(this.invoice.serviceFee)||this.invoice.subTotal==null||isUndefined(this.invoice.subTotal)||
    this.invoice.total==null||isUndefined(this.invoice.total)){
      valid = false;
    }
    if(valid){
    this.invoiceService.createInvoice(this.invoice).subscribe((result: any) => {

      if(result.HasError)
      {
        this.errorOccured = true;
      }
      else
      {
        this.setAsViewAll();
        this.getAllInvoices();
        this.itemList = [];
      }
    });
  }
}

  onChanage(event) {
    this.invoice.total = parseInt(this.invoice.subTotal.toString()) + parseInt(event);
  }

}
