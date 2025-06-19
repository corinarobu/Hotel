import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class BankAccountService {
  baseUrl = 'https://localhost:7250/api/BankAccount/';
  constructor(private http: HttpClient) { }
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
  getBankAccounts(): Observable<UserBankAccount[]> {
    return this.http.get<UserBankAccount[]>(this.baseUrl , {
      headers: this.getAuthHeaders()
    });
  }
  addBankAccount(iban: string, bankName: string): Observable<string> {
    const body = { iban, bankName };
    return this.http.post<string>(this.baseUrl, body, {
      headers: this.getAuthHeaders(),
      responseType: 'text' as 'json'
    });
  }
}
export interface UserBankAccount{
  id: number;
  iban: string;
  bankName: string;
}
