import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookRoomService {
  private apiUrl = 'https://localhost:7250/api/Room';
  private ratingUrl = 'https://localhost:7250/api/Rating';

  private selectedRoom: Room[] = [];

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) { }


  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.apiUrl);
  }
  selectRoom(room: Room): void {
    this.router.navigate(['/payment-page', room.id_Room]);
  }
  getRoomById(roomId: number): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/${roomId}`);
  }
  checkRoomAvailability(roomId: number, checkIn: string, checkOut: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/check-availability`, {
      params: { roomId, checkIn, checkOut }
    });
  }
  getAllFaqs(): Observable<Faq[]> {
    return this.http.get<Faq[]>(`https://localhost:7250/api/FAQ`);
  }
  getFilteredRooms(
    capacity?: number,
    minPrice?: number,
    maxPrice?: number,
    isAvailable?: boolean,
    viewType: ViewType | null = null,
    hasBreakfastIncluded?: boolean,
    mealPlan: PlanMeal | null = null
  ): Observable<Room[]> {
    let params = new HttpParams();

    if (capacity !== undefined && capacity !== null) {
      params = params.append('capacity', capacity.toString());
    }
    if (minPrice !== undefined && minPrice !== null) {
      params = params.append('minPrice', minPrice.toString());
    }
    if (maxPrice !== undefined && maxPrice !== null) {
      params = params.append('maxPrice', maxPrice.toString());
    }
    if (isAvailable !== undefined && isAvailable !== null) {
      params = params.append('isAvailable', isAvailable.toString());
    }
    if (viewType !== undefined && viewType !== null) {
      params = params.append('viewType', viewType.toString());
      console.log('ViewType', viewType);
    }
    if (hasBreakfastIncluded !== undefined && hasBreakfastIncluded !== null) {
      params = params.append('hasBreakfastIncluded', hasBreakfastIncluded.toString());
    }
    if (mealPlan !== undefined && mealPlan !== null) {
      params = params.append('mealPlan', mealPlan.toString());
    }

    return this.http.get<Room[]>(`${this.apiUrl}/filtered`, { params });
  }

  submitRating(rating: Rating): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json'
    });
    if (!rating.score) {
      alert("Please select a rating before submitting.");
      return new Observable();
    }

    const ratingData = {
      userId: rating.userId,
      roomId: rating.roomId,
      score: rating.score
    };

    return this.http.post(`https://localhost:7250/api/Rating/SubmitRating`, ratingData, { headers });
  }
  getAverageRating(roomId: number): Observable<any> {
    return this.http.get<any>(`${this.ratingUrl}/average/${roomId}`);
  }
  updateRoom(room: Room): Observable<Room> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json',
    });

    return this.http.put<Room>(`${this.apiUrl}/${room.id_Room}`, room, { headers });
  }

}

export interface Room {
  id_Room: number;
  roomNumber: number;
  description: string;
  capacity: number;
  pricePerNight: number;
  isAvailable: boolean;
  viewType: ViewType;
  hasBreakfastIncluded: boolean;
  mealPlan: PlanMeal;
  startDate: Date;
  endDate: Date;
  userRating?: number;
}
export interface Faq {
  question: string;
  answer: string;
  idRoom: number;
}
export interface Rating {
  userId: number;
  roomId: number;
  score: number;
}
export enum ViewType {
  street = 0,
  mountain = 1,
}
export enum PlanMeal {
  allinclusive = 1,
  none = 0,
}
