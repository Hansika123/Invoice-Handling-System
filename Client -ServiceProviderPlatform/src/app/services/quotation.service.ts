import { Quotation } from './../models/quotation';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class QuotationService {
  private url = environment.host;

  constructor(private http: HttpClient) { }

  GetQuoteRequestByServiceProviderId(serviceProviderId: number) {
    // this.config.api.baseUrl + '/flats/' + flatId + '/users/' + userId,
    const params = new HttpParams();
    params.append('serviceProviderId', serviceProviderId.toString());
    return this.http.get(this.url + '/GetQuoteRequestByServiceProviderId?serviceProviderId=' + serviceProviderId);
  }

  CreateQuatation(data: any) {
    return this.http.post(this.url + '/CreateQuatation', data);
  }

  GetAllQuatationsByServiceProviderId(serviceProviderId: number) {
    return this.http.get(this.url + '/GetAllQuatationsByServiceProviderId?serviceProviderId=' + serviceProviderId)
      .pipe(map((data: any) => {
        return data.Entities as Quotation[];
      }));
  }

  GetQuoteRequestByServiceProviderCategoryId(categoryId: number) {
    // this.config.api.baseUrl + '/flats/' + flatId + '/users/' + userId,
    const params = new HttpParams();
    params.append('categoryId', categoryId.toString());
    return this.http.get(this.url + '/GetQuoteRequestByServiceProviderCategoryId?categoryId=' + categoryId);
  }
}
