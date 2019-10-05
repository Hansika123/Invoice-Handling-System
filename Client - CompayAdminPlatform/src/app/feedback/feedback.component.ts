import { ServiceProviderDetail } from './../models/ServiceProviderDetail';
import { ServiceProviderService } from './../services/service-provider.service';
import { FeedbackDetail } from './../models/feedbackDetail';
import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '../services/feedback.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
})
export class FeedbackComponent implements OnInit {

  feedbackDetail: FeedbackDetail = <FeedbackDetail>{};
  hasError: boolean;
  errorMessage: string;
  selectedLevel: any;
  serviceProviders: ServiceProviderDetail[] = [];
  selectedServiceProvider: any;

  constructor(private feedbackService: FeedbackService, private serviceProviderService: ServiceProviderService) {
    this.selectedLevel = 1;
  }

  ngOnInit() {
    this.getAllServiceProviders();
  }

  createFeedback() {
    this.feedbackDetail.ServiceProviderId = this.selectedServiceProvider.Id;
    this.feedbackDetail.RatingLevel = this.selectedLevel;
    this.feedbackService.createFeedback(this.feedbackDetail).subscribe((result: any) => {

      this.feedbackDetail.RatingLevel = this.selectedLevel;
      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.feedbackDetail = <FeedbackDetail>{};
      }
    });
  }

  getAllServiceProviders() {
    this.serviceProviderService.getServiceProviders().subscribe((serviceProviders: any) => {
      this.serviceProviders = serviceProviders;
      this.selectedServiceProvider = this.serviceProviders[0];
    });
  }
}
