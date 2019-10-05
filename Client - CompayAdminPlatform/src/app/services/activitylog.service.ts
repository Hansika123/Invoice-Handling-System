import { ActivityLogDetail } from './../models/activityLogDetail';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ActivitylogService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  getAllActivities() {

    return this.http.get(this.url + '/GetAllActivityLogs').pipe(map((data: any) => {
      return data.Entities as ActivityLogDetail[];
    }));
  }
}
