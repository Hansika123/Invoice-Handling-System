import { AccountService } from './../services/account.service';
import { BankAccountDetail } from './../models/bankAccountDetail';
import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { SESSION_STORAGE, StorageService } from 'angular-webstorage-service';

@Component({
  selector: 'app-bank',
  templateUrl: './bank-account.component.html',
  styleUrls: ['./bank-account.component.css']
})
export class BankAccountComponent implements OnInit {

  bankAccountDetail: BankAccountDetail = <BankAccountDetail>{};
  hasError: boolean;
  errorMessage: string;

  constructor(@Inject(SESSION_STORAGE) private storage: StorageService, private accountService: AccountService) {
  }

  ngOnInit() {
    this.bankAccountDetail.ServiceProviderId = this.storage.get('serviceProviderId');
    this.getBankAccountByServiceProviderId();
  }

  getBankAccountByServiceProviderId() {
    this.accountService.getBankAccountByServiceProviderId(this.bankAccountDetail.ServiceProviderId).subscribe((result: any) => {
      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.bankAccountDetail = result.Entity;
      }
    });
  }

  createBankAccount() {
    this.accountService.createBankAccount(this.bankAccountDetail).subscribe((result: any) => {

      if (result.HasError) {
        this.hasError = true;
        this.errorMessage = result.ErrorMessage;
      } else {
        this.bankAccountDetail = result.Entity;
      }
    });
  }
}
