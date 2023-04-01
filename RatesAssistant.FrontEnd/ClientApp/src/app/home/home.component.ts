import { Component, OnDestroy, OnInit } from '@angular/core';
import { ExchangeRateService } from '../services/exchange-rate.service';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  constructor(
    private _exchangeRateService: ExchangeRateService,
    private _toastrService: ToastrService
  ) {}

  subscriptions: Subscription[] = [];

  availableCurrencies: string[] = [];
  selectedCurrency!: string;
  selectedDate!: string; // ISO string

  displayedExchangeRate = 0;

  ngOnInit() {
    this.load();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((s) => s.unsubscribe());
  }

  load() {
    this.subscriptions.push(
      this._exchangeRateService
        .getAvailableCurrencies()
        .subscribe((currencies) => {
          this.availableCurrencies = currencies;
        })
    );
  }

  getExchangeRate() {
    if (!this.validateFields()) {
      return;
    }

    this.subscriptions.push(
      this._exchangeRateService
        .getExchangeRate(this.selectedCurrency, this.selectedDate)
        .subscribe(
          (rate) => {
            this.displayedExchangeRate = rate;
          },
          (err) => {
            this._toastrService.error(err.error);
          }
        )
    );
  }

  validateFields() {
    if (!this.selectedCurrency) {
      this._toastrService.warning('Please, select currency');

      return false;
    }

    if (!this.selectedDate) {
      this._toastrService.warning('Please, select date');

      return false;
    }

    return true;
  }
}
