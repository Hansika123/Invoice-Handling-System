import { InvoiceDetail } from './../models/invoiceDetail';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private url = environment.host;
  constructor(private http: HttpClient) { }

  createInvoice(data: any) {
    return this.http.post(this.url + '/CreateInvoice', data);
  }

  getAllInvoices() {
    return this.http.get(this.url + '/GetAllInvoice').pipe(map((data: any) => {
      return data.Entities as InvoiceDetail[];
    }));
  }

  getInvoiceById(invoiceId: number) {
    return this.http.post(this.url + '/GetAllInvoice', invoiceId);
  }

  updateInvoice(invoiceId: number, invoiceStatus: number) {
    return this.http.post(this.url + '/UpdateInvoiceStatus?invoiceId=' + invoiceId + '&invoiceStatus=' + invoiceStatus, null);
  }

  deleteInvoice(invoiceId: number) {
    return this.http.post(this.url + '/DeleteInvoice', invoiceId);
  }
}
