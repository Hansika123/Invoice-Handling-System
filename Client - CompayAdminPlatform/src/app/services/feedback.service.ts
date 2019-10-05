import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  private url = environment.host;

  constructor(private http: HttpClient) { }

  createFeedback(data: any) {
    return this.http.post(this.url + '/CreateFeedback', data);
  }
}
