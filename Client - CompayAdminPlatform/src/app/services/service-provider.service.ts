import { CategoryDetail } from './../models/categoryDetails';
import { ServiceProviderDetail } from './../models/ServiceProviderDetail';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ServiceProviderService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  getServiceProviders() {
    return this.http.get(this.url + '/GetAllServiceProviders').pipe(map((data: any) => {
      return data.Entities as ServiceProviderDetail[];
    }));
  }

  getCategories() {
    return this.http.get(this.url + '/GetAllCategories').pipe(map((data: any) => {
      return data.Entities as CategoryDetail[];
    }));
  }
}
