import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = environment.host;

  constructor(private http: HttpClient,
    @Inject(SESSION_STORAGE) private storage: StorageService) { }

  public isAuthenticated(): boolean {

    try {

      const user = this.storage.get('userId');
      if (user) {
        return true;
      } else {
        return false;
      }

    } catch (e) {
      return false;
    }
  }

  signup(data: any) {
    return this.http.post(this.url + '/signup', data);
  }

  login(data: any) {
    return this.http.post(this.url + '/signin', data);
  }
}
