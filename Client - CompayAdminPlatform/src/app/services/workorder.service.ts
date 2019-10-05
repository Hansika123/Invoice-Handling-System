import { WorkOrderDetail } from './../models/workOrder';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WorkorderService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  getAllWorkOrders() {

    return this.http.get(this.url + '/GetAllWorkOrders').pipe(map((data: any) => {
      return data.Entities as WorkOrderDetail[];
    }));
  }

  createWorkOrder(data: any) {
    return this.http.post(this.url + '/CreateWorkOrder', data);
  }
}
