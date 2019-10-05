import { ServiceProviderService } from './../../services/service-provider.service';
import { Component, OnInit } from '@angular/core';
import { QuotationService } from '../../services/quotation.service';
import { RequestQuotation } from '../../models/requestQuotation';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { ServiceProviderDetail } from '../../models/ServiceProviderDetail';
import { DataService } from '../../services/data.service';
import { isUndefined } from 'util';

@Component({
  selector: 'app-quotation-rquest',
  templateUrl: './quotation-rquest.component.html',
  styleUrls: ['./quotation-rquest.component.css']
})
export class QuotationRquestComponent implements OnInit {

  isNewRequest: boolean;
  quoteRequest: RequestQuotation;
  quoteRequests: RequestQuotation[] = [];
  hasError: boolean;
  errorMessage: string;
  serviceProviders: ServiceProviderDetail[] = [];
  private readonly notifier: NotifierService;
  selectedServiceProvider: any;
  categories: any;
  selectedCategory: any;
  quotDetails: any;
  private todate = new Date();

  constructor(private quoteService: QuotationService, private router: Router, notifierService: NotifierService,
    private serviceProviderService: ServiceProviderService, private data: DataService) {

    this.quoteRequest = new RequestQuotation();
    this.notifier = notifierService;
    this.quotDetails = {};
  }

  ngOnInit() {
    this.getAllQuoteRequests();
    this.getAllServiceProviders();
    this.getAllCategories();

    this.data.currentMessage.subscribe(message => this.quotDetails = message);
    this.isNewRequest = this.quotDetails.isNewRequest;
  }

  getAllQuoteRequests() {
    this.quoteService.getAllQuotationRequests().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.quoteRequests = result.Entities;
      }
    });
  }

  setAsCreateNew() {
    this.isNewRequest = true;
    this.quotDetails = {};
    this.quoteRequest.dueDate = null;
    this.quoteRequest.requestName='';
    this.quoteRequest.requestDescription='';
    this.selectedServiceProvider='Provider';
  }

  setAsViewAll() {
    this.isNewRequest = false;
  }

  createQuoteRequest() {
    var valid: Boolean = true;
    this.quoteRequest.JobCategoryId = this.selectedCategory.Id;
    this.quoteRequest.PropertyHolderName = this.quotDetails.PropertyHolderName;
    this.quoteRequest.PHPhoneNumber = this.quotDetails.PHPhoneNumber;
    this.quoteRequest.PHAddress = this.quotDetails.PHAddress;
    this.quoteRequest.TaskId = this.quotDetails.TaskId;
    this.quoteRequest.serviceProviderId = this.selectedServiceProvider.Id;

    if(this.quoteRequest.requestName == ''|| isUndefined(this.quoteRequest.requestName)){

    if(this.quoteRequest.requestName == '' ||isUndefined(this.quoteRequest.requestName)||this.quoteRequest.PropertyHolderName==''||isUndefined(this.quoteRequest.PropertyHolderName)||
    this.quoteRequest.PHAddress==''||isUndefined(this.quoteRequest.PHAddress)||this.quoteRequest.PHPhoneNumber==null||isUndefined(this.quoteRequest.PHPhoneNumber)||
    this.quoteRequest.serviceProviderId==null||isUndefined(this.quoteRequest.serviceProviderId)||
    this.quoteRequest.dueDate==null||isUndefined(this.quoteRequest.dueDate))
    {
      valid = false;
    }
    
    }
    if(valid){
    this.quoteService.createQuotationRequest(this.quoteRequest).subscribe((result: any) => {

      this.isNewRequest = false;
      if (result.HasError) {
        this.router.navigate(['error']);
      } else {
        this.getAllQuoteRequests();
      }
    });
  }
}

  onProviderSelected() {
    // console.log(this.selectedServiceProvider);
  }

  getAllServiceProviders() {
    this.serviceProviderService.getServiceProviders().subscribe((serviceProviders: any) => {
     this.serviceProviders = serviceProviders;
     this.selectedServiceProvider = this.serviceProviders[0];
    });
  }

  getAllCategories() {
    this.serviceProviderService.getCategories().subscribe((categoryList: any) => {
      this.categories = categoryList;
      this.selectedCategory = this.categories[0];
    });
  }
}
