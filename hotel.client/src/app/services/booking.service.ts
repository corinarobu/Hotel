import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private baseUrl = 'https://localhost:7250/api/bookings';

  constructor(private http: HttpClient, private authService: AuthService) { }

  addBooking(bookingData: any, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/add-booking?userId=${userId}`, bookingData);
  }

  getBooking(bookingId: number): Observable<any> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get(`${this.baseUrl}/${bookingId}`, {headers});
  }
  getBookingsByUser(userId: number): Observable<any[]> {
    const token = this.authService.getToken(); 
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<any[]>(`${this.baseUrl}/user/${userId}`, {headers});
  }
  hasUserBookedRoomInAPeriod(roomId: number, checkInDate: string, checkOutDate: string): Observable<boolean> {
    const token = this.authService.getToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    const formattedCheckInDate = this.formatDate(checkInDate);
    const formattedCheckOutDate = this.formatDate(checkOutDate);

    const params = new HttpParams()
      .set('roomId', roomId.toString())
      .set('checkInDate', formattedCheckInDate)
      .set('checkOutDate', formattedCheckOutDate);

    return this.http.get<boolean>(`${this.baseUrl}/HasUserBookedRoomInAPeriod`, { headers, params });
  }


  formatDate(date: string): string {
    const formattedDate = new Date(date);
    return formattedDate.toISOString().split('T')[0]; 
  }

}
