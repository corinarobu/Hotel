import { Component } from '@angular/core';
import { Meal, MealService } from '../../../../services/meal.service';
import { AuthService } from '../../../../services/auth.service';


@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrl: './restaurant.component.css'
})
export class RestaurantComponent {
  meals: Meal[] = [];
  userFullName: string | null = null;

  constructor(private mealService: MealService, private authService: AuthService) { }

  ngOnInit(): void {
    this.mealService.getAllMeals().subscribe(
      (data) => {
        this.meals = data;
      },
      (error) => {
        console.error('Error fetching meals', error);
      }
    );
    this.userFullName = this.authService.getUserFullName();
  }
 
}
