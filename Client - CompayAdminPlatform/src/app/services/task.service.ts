import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

import { TaskDetail } from '../models/taskDetail';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  getAllTasks() {

    return this.http.get(this.url + '/GetAllTasks').pipe(map((data: any) => {
      return data.Entities as TaskDetail[];
    }));
  }

  getTasksByCallerSystemId(callerSystemUserId: number) {

    return this.http.get(this.url + '/GetTasksByUserId?callerSystemUserId=' + callerSystemUserId).pipe(map((data: any) => {
      return data.Entities as TaskDetail[];
    }));
  }

  createTask(data: any) {
    return this.http.post(this.url + '/CreateTask', data);
  }

  updateTask(data: any) {
    return this.http.post(this.url + '/UpdateTask', data);
  }
}
