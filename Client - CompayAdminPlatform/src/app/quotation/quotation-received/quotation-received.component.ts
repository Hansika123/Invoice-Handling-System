import { QuotationService } from './../../services/quotation.service';
import { QuatationDetail } from './../../models/quatationDetail';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-quotation-received',
  templateUrl: './quotation-received.component.html',
  styleUrls: ['./quotation-received.component.css']
})
export class QuotationReceivedComponent implements OnInit {

  quatationList: QuatationDetail[] = [];
  hasError: boolean;
  errorMessage: string;
  quatation: QuatationDetail = <QuatationDetail>{};
  isViewQuatation: boolean;

  constructor(private quotationService: QuotationService) { }

  ngOnInit() {
    this.getAllQuatation();
  }

  getAllQuatation() {
    this.quotationService.GetAllQuatations().subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.quatationList = result;
        console.log(this.quatationList);
      }
    });
  }

  viewQuatation(quatation: QuatationDetail) {
    this.isViewQuatation = true;
    this.quatation = quatation;
    console.log(quatation);
  }

  setAsViewAll() {
    this.isViewQuatation = false;
  }

  acceptQuatation(status: number) {
    console.log(status);
    this.quotationService.UpdateQuatationStatus(this.quatation.Id, status).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.getAllQuatation();
        this.isViewQuatation = false;
      }
    });
  }

  rejectQuatation(status: number) {
    this.quotationService.UpdateQuatationStatus(this.quatation.Id, status).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.getAllQuatation();
        this.isViewQuatation = false;
      }
    });
  }
}