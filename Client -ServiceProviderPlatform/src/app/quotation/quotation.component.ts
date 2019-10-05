import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { QuotationService } from '../services/quotation.service';
import { RequestQuotation } from '../models/requestQuotation';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { ServiceProvider } from '../models/serviceProvider';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';
import { Quotation } from '../models/quotation';
import { Item } from '../models/item';
import { isUndefined } from 'util';
import { parse } from 'querystring';

@Component({
  selector: 'app-quotation',
  templateUrl: './quotation.component.html',
  styleUrls: ['./quotation.component.css']
})
export class QuotationComponent implements OnInit {

  isNewRequest: boolean;
  quotation: Quotation;
  quoteRequests: RequestQuotation[] = [];
  hasError: boolean;
  errorMessage: string;
  serviceProviders: ServiceProvider[] = [];
  private readonly notifier: NotifierService;
  selectedServiceProvider: number = null;
  userId: number;
  userRoleId: number;
  serviceProviderId: number;
  item: Item;
  errorOccured: boolean;
  itemList: Array<Item> = [];
  serviceProviderCategoryId: number;

  constructor(private quoteService: QuotationService, private router: Router, notifierService: NotifierService,
    @Inject(SESSION_STORAGE) private storage: StorageService) {
    this.quotation = new Quotation();
    this.notifier = notifierService;
    this.item = new Item();
    this.quotation.estimatedSubTotal = 0;
  }

  ngOnInit() {
    this.userId = this.storage.get('userId');
    this.userRoleId = this.storage.get('userRoleId');
    this.quotation.serviceProviderId = this.storage.get('serviceProviderId');
    this.serviceProviderCategoryId = this.storage.get('serviceProviderCategoryId');

    if (this.userId != null && this.userRoleId === 2) {
      this.getRequestListById();
    } else {
      this.router.navigate(['home']);
    }
  }

  getRequestListById() {
    this.quoteService.GetQuoteRequestByServiceProviderCategoryId(this.serviceProviderCategoryId).subscribe((result: any) => {
      console.log(result);
      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
        this.errorOccured = true;
      } else {
        this.quoteRequests = result.Entities;
      }
    });
  }

  setAsCreateNew() {
    this.isNewRequest = true;
    this.quotation.jobDescription=null;
    this.quotation.estimatedServiceFee=null;
    this.quotation.estimatedTotal=null;
    this.quotation.estimatedSubTotal=null;
    this.item.unitPrice=null;
    this.item.itemName=null;
    this.item.itemCode=null;
    this.item.itemDescription=null;
    this.item.estimatedQuentity=null;
  }

  setAsViewAll() {
    this.isNewRequest = false;
  }

  createQuotationModel(item) {
    this.quotation = item;
    this.isNewRequest = true;
  }

  addItems(item) {
    const tableItem = new Item();

    tableItem.itemCode = item.itemCode;
    tableItem.itemName = item.itemName;
    tableItem.unitPrice = item.unitPrice;
    tableItem.itemDescription = item.itemDescription;
    tableItem.estimatedQuentity = item.estimatedQuentity;

    if(tableItem.unitPrice != null){
    this.itemList.push(tableItem);
    this.quotation.items = this.itemList;

    this.item.unitPrice=null;
    this.item.itemName=null;
    this.item.itemCode=null;
    this.item.itemDescription=null;
    this.item.estimatedQuentity=null;
    }
     else{
       alert("please provide valid details ");
    }

    this.quotation.estimatedSubTotal = 0;

    this.quotation.items.forEach(
      p => this.quotation.estimatedSubTotal = (parseFloat(p.unitPrice.toString()) * parseFloat(p.estimatedQuentity.toString())) + parseFloat(this.quotation.estimatedSubTotal.toString())
    );
    this.quotation.estimatedTotal = parseInt(this.quotation.estimatedSubTotal.toString()) + parseInt(this.quotation.estimatedServiceFee.toString());
    this.item.itemName=null;
  }


  createQuotation() {
    var valid: Boolean = true;
    if(this.quotation.jobDescription == ''||isUndefined(this.quotation.jobDescription)||this.quotation.estimatedServiceFee==null||isUndefined(this.quotation.estimatedServiceFee)||this.quotation.estimatedSubTotal==null||isUndefined(this.quotation.estimatedTotal))  {
      valid = false;
    }
    if(valid){
    this.quotation.status = 0;
    this.quotation.serviceProviderId = this.storage.get('serviceProviderId');
    this.quoteService.CreateQuatation(this.quotation).subscribe((result: any) => {
      if (result.HasError) {
        this.errorOccured = true;
      } else {
        this.setAsViewAll();
        this.getRequestListById();
        this.itemList = [];
      }
    });
  }
 }
  onChanage(event) {
    this.quotation.estimatedTotal = parseInt(this.quotation.estimatedSubTotal.toString()) + parseInt(event);
  }
}
