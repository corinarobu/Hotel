import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentserviceService {
 
  apiUrl = 'https://localhost:7250/api/stripe';

  constructor(private http: HttpClient, private auth:AuthService) { }
 

  getPaymentUrl(bookingId: number): Observable<{ paymentUrl: string }> {
    return this.http.get<{ paymentUrl: string }>(`${this.apiUrl}/payment/${bookingId}`)
      .pipe(
        catchError(error => {
          console.error('Error in getPaymentUrl:', error);
          throw error;
        })
      );
  }
  createCheckoutSession(amount: number, bookingId: number): Observable<{ url: string }> {
    const token = this.auth.getToken();
    const url = `${this.apiUrl}/create-checkout-session?amount=${amount}&bookingId=${bookingId}&successUrl=https://127.0.0.1:61529/succed-payment&cancelUrl=https://127.0.0.1:61529/cancel-payment`;

    console.log('Checkout request URL:', url);
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    console.log(token);
    return this.http.post<{ url: string }>(url, {}, {headers}).pipe(
      catchError(error => {
        console.error('Error in createCheckoutSession:', error);
        return throwError(() => new Error('Failed to create checkout session'));
      })
    );
  }


  bookRoom(bookingData: any): Observable<any> {
    const token = this.auth.getToken();

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.post('https://localhost:7250/api/bookings/add-booking', bookingData, { headers })
      .pipe(
        catchError(error => {
          console.error('Error in bookRoom:', error);
          throw error;
        })
      );
  }
  hasBankAccount(userId: number): Observable<boolean> {
    const token = this.auth.getToken();

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<boolean>(`https://localhost:7250/api/payments/has-bank-account/${userId}`, { headers });
  }
  updatePaymentStatus(bookingId: number, status: string) {
    const token = this.auth.getToken();
 //   console.log(token);

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    return this.http.put(
      `https://localhost:7250/api/bookings/update-payment-status/${bookingId}`,
       JSON.stringify(status),
      { headers }
    );
  }
  cancelBooking(bookingId: number): Observable<void> {
    const token = this.auth.getToken();

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.put<void>(
      `https://localhost:7250/api/bookings/${bookingId}/cancel`,
      {},
      { headers }
    ).pipe(
      catchError(error => {
        console.error('Error in cancelBooking:', error);
        return throwError(() => new Error('Failed to cancel booking'));
      })
    );
  }

}
