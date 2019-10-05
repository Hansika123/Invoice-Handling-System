import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private url = environment.host;
  constructor(private http: HttpClient) { }

  signup(data: any) {
    return this.http.post(this.url + '/signup', data);
  }

  login(data: any) {
    return this.http.post(this.url + '/signin', data);
  }

  getCategories() {
    return this.http.get(this.url + '/GetAllCategories').pipe(map((data: any) => {
      return data.Entities as any[];
    }));
  }
}
