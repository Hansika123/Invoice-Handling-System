import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WorkorderService {
  private url = environment.host;

  constructor(private http: HttpClient) { }

  getWorkOrderListByServiceProvider(serviceProviderId: number) {

    // const params = new HttpParams();
    // params.append('serviceProviderId', serviceProviderId.toString());
    return this.http.get(this.url + '/GetWorkOrdersByServiceProviderId?serviceProviderId=' + serviceProviderId);
  }

  updateWorkOrderStatus(data: any) {
    return this.http.post(this.url + '/UpdateWorkOrder', data);
  }
}
