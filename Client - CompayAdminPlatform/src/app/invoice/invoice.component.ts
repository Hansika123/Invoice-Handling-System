import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InvoiceService } from '../services/invoice.service';
import { InvoiceDetail } from '../models/invoiceDetail';
import { NotifierService } from 'angular-notifier';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';

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
  invoiceDetail: InvoiceDetail = <InvoiceDetail>{};
  isViewInvoice: boolean;

  constructor(private invoiceService: InvoiceService, private router: Router, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  ngOnInit() {
    this.getAllInvoices();
  }

  getAllInvoices() {
    this.invoiceService.getAllInvoices().subscribe((result: any) => {
      console.log(result);

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.invoiceDetails = result;
      }
    });
  }

  viewInvoice(invoice: InvoiceDetail) {
    this.isViewInvoice = true;
    this.invoiceDetail = invoice;
  }

  setAsViewAll() {
    this.isViewInvoice = false;
  }

  cancel() {
    this.isViewInvoice = false;
  }

  paidInvoice(status: number) {
    this.invoiceService.updateInvoice(this.invoiceDetail.Id, status).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.getAllInvoices();
        this.isViewInvoice = false;
      }
    });
  }

  public captureScreen() {

    const data = document.getElementById('contentToConvert');
    html2canvas(data).then(canvas => {

      // Few necessary setting options
      const imgWidth = 200;
      // const pageHeight = 295;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      // const heightLeft = imgHeight;

      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF
      const position = 0;

      const today = new Date();
      const dd = today.getDate();
      const mm = today.getMonth() + 1;
      const yyyy = today.getFullYear();
      const concatDate = dd + '_' + mm + '_' + yyyy;

      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save('Invoice_' + concatDate + '.pdf'); // Generated PDF
    });
  }

}
