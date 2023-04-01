import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ExchangeRateService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  getAvailableCurrencies(): Observable<string[]> {
    return this.http.get<string[]>(
      this.baseUrl + 'api/exchange-rate/available-currencies'
    );
  }

  getExchangeRate(currency: string, isoDate: string) {
    return this.http.get<number>(
      this.baseUrl + `api/exchange-rate/${currency}/${isoDate}`
    );
  }
}
