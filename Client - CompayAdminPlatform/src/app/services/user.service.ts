import { WorkOrderDetail } from './../models/workOrder';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  getAllUsers() {

    return this.http.get(this.url + '/GetAllServiceProviders').pipe(map((data: any) => {
      return data.Entities as any[];
    }));
  }
}
