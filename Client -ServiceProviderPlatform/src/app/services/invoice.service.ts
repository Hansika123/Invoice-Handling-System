import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private url = environment.host;
  constructor(private http: HttpClient) { }

  createInvoice(data: any) {
    return this.http.post(this.url + '/CreateInvoice', data);
  }

  getAllInvoiceByServiceProviderId(serviceProviderId: number) {
    return this.http.get(this.url + '/GetInvoiceByServiceProviderId?serviceProviderId=' + serviceProviderId);
  }

}
