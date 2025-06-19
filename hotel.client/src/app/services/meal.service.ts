import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MealService {
  private apiUrl = 'https://localhost:7250/api/Meal'; 

  constructor(private http: HttpClient) { }  

  getAllMeals(): Observable<Meal[]> {
    return this.http.get<Meal[]>(this.apiUrl); 
  }
}

export interface Meal {
  meal_Id: number;
  meal_Name: string;
  meal_Description: string;
}
