import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class DataService {

  private messageSource = new BehaviorSubject(Object);
  currentMessage = this.messageSource.asObservable();

  constructor() { }

  changeQuatationRequestDetail(message: any) {
    this.messageSource.next(message);
  }

  changeWorkOrderRequestDetail(message: any) {
    this.messageSource.next(message);
  }

}
