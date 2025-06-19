import { Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7250/api/AccountManagement';
  currentUser = signal<User | null>(null);
  private tokenKey = 'token';

  constructor(private http: HttpClient) { }

  login(model: any): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Login', model).pipe(
      map(response => {
        if (response) {
          this.setUser(response);
        }
        return response;
      })
    );
  }

  register(register: RegisterDto): Observable<User> {
    return this.http.post<User>(this.apiUrl + '/Register', register).pipe(
      map(response => {
        if (response) {
          this.setUser(response);
        }
        return response;
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  
  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  private setUser(user: User): void {
    localStorage.setItem('token', user.token);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  getUserFullName(): string | null {
    const user = localStorage.getItem('user');
    console.log(user);
    return user ? JSON.parse(user).username : null;
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserId(): Observable<number> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('token')}`);
    return this.http.get<number>(`${this.apiUrl}/current-user-id`, { headers });
  }
  isUserAdmin(): boolean {
    const token = this.getToken();

    if (!token) {
      return false;
    }

    try {
      const decodedToken: any = jwtDecode(token);
      return decodedToken && decodedToken.role && decodedToken.role.includes('Admin');
    } catch (error) {
      console.error('Error decoding token:', error);
      return false;
    }
  }


}
export interface User {
  username: string;
  email: string
  token: string;
}

export interface RegisterDto {
  fullname: string;
  email: string;
  password: string;
}
