import { QuatationDetail } from './../models/quatationDetail';
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

  getAllQuotationRequests() {
    return this.http.get(this.url + '/GetAllQuoteRequest');
  }

  createQuotationRequest(data: any) {
    return this.http.post(this.url + '/CreateQuatationRequest', data);
  }

  GetQuoteRequestByServiceProviderId(serviceProviderId: number) {
    return this.http.get(this.url + '/GetQuoteRequestByServiceProviderId');
  }

  CreateQuatation(data: any) {
    return this.http.post(this.url + '/CreateQuatation', data);
  }

  GetAllQuatations() {
    // return this.http.get(this.url + '/GetAllQuatations');
    return this.http.get(this.url + '/GetAllQuatations').pipe(map((data: any) => {
      return data.Entities as QuatationDetail[];
    }));
  }

  GetAllQuatationsByServiceProviderId(serviceProviderId: number) {
    return this.http.get(this.url + '/GetAllQuatationsByServiceProviderId');
  }

  UpdateQuatationStatus(quatationId: number, statusId: number) {
    return this.http.post(this.url + '/UpdateQuatationStatus?quatationId=' + quatationId + '&statusId=' + statusId, null);
  }
}
