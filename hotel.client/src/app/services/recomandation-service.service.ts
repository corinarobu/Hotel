import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Recommendation {
  id_Recomandation: number;
  name: string;
  description: string;
  address: string;
  entryFee: string;
  distanceFromHotel: string;
  userId: number ;
}


@Injectable({
  providedIn: 'root'
})
export class RecomandationServiceService {
  private apiUrl = 'https://localhost:7250/api/Recomandation';

  constructor(private http: HttpClient) { }

  addRecomandation(recommendation: Recommendation): Observable<Recommendation> {
    const token = this.getAuthToken();

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.post<Recommendation>(this.apiUrl, recommendation, {headers});
  }

  getAllRecomandations(): Observable<Recommendation[]> {
    return this.http.get<Recommendation[]>(this.apiUrl);
  }

  deleteRecomandation(id: number): Observable<void> {
    if (id == null || id === undefined) {
      console.error('Invalid ID');
      
    }

    const token = this.getAuthToken();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }

  private getAuthToken(): string {
    console.log('Authorization Token:', localStorage.getItem('token'));
    return localStorage.getItem('token') || '';
  }

}
