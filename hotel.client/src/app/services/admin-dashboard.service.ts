import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminDashboardService {
  private apiUrl = "https://localhost:7250/api/Account";

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<Users[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Users[]>(this.apiUrl, { headers });
  }

  deleteUser(userId: number): Observable<void> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.delete<void>(`${this.apiUrl}/${userId}`, { headers });
  }

  searchUsers(searchTerm: string): Observable<Users[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<Users[]>(`${this.apiUrl}?searchTerm=${searchTerm}`, { headers });
  }
}

export interface Users {
  id: number;
  username: string;
  email: string;
}
