import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  createBankAccount(data: any) {
    return this.http.post(this.url + '/CreateBankAccount', data);
  }

  getBankAccountByServiceProviderId(serviceProviderId: number) {
    return this.http.get(this.url + '/GetBankAccountByServiceProviderId?serviceProviderId=' + serviceProviderId);
  }

}
