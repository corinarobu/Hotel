import { Component, OnInit } from '@angular/core';
import { BookRoomService, PlanMeal, Rating, Room, ViewType } from '../../../../services/book-room.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-book-room',
  templateUrl: './book-room.component.html',
  styleUrls: ['./book-room.component.css']
})
export class BookRoomComponent implements OnInit {
  selectedCapacity?: number;
  selectedAvailability?: boolean;
  selectedViewType?: ViewType;
  selectedBreakfast?: boolean;
  selectedMealPlan?: PlanMeal;
  maxPrice?: number;
  filteredRoomsData: Room[] = [];
  room: Room[] = [];
  selectedRoom: Room[] = [];
  rate: Rating[] = [];
  userId: number | null = null;
  averageRatings: { [key: number]: number } = {};
  isAdmin: boolean = false;

  constructor(private bookRoomService: BookRoomService, private router: Router, private http: HttpClient, private authService: AuthService,) { }

  ngOnInit(): void {
    this.isAdmin = this.authService.isUserAdmin();
    console.log(this.isAdmin);
    this.bookRoomService.getAllRooms().subscribe(
      (data) => {
        this.room = data.filter(room => room.capacity > 0);
        this.filteredRoomsData = data.filter(room => room.capacity > 0);

        this.room.forEach(room => {
          this.getAverageRating(room.id_Room);
        });
      },
      (error) => {
        console.error('Error fetching rooms', error);
      }
    );

    this.authService.getUserId().subscribe({
      next: (id) => {
        this.userId = id;
      },
      error: (err) => {
        console.error('Error fetching user ID:', err);
      }
    });
  }

  applyFilters(): void {
    this.bookRoomService.getFilteredRooms(
      this.selectedCapacity,
      undefined,
      this.maxPrice,
      this.selectedAvailability,
      this.selectedViewType,
      this.selectedBreakfast,
      this.selectedMealPlan
    ).subscribe((rooms: Room[]) => {
      this.filteredRoomsData = rooms
        .filter(room => room.capacity > 0)

    });
  }

  getViewTypeName(viewType: ViewType | string): string {
    switch (viewType) {
      case ViewType.street:
      case 'street':
        return 'Street';
      case ViewType.mountain:
      case 'mountain':
        return 'Mountain';
      default:
        return 'Unknown';
    }
  }

  getMealPlanName(plan: PlanMeal | string): string {
    switch (plan) {
      case PlanMeal.none:
      case 'none':
        return 'Standard';
      case PlanMeal.allinclusive:
      case 'allinclusive':
        return 'All Inclusive';
      default:
        return 'Unknown';
    }
  }


  getRoomImage(capacity: number): string {
    switch (capacity) {
      case 2:
        return '/double.jpg';
      case 3:
        return '/triple.jpg';
      case 4:
        return '/quadruple.jpg';
      default:
        return '/room1.jpg';
    }
  }
  selectRoom(room: Room): void {
    this.bookRoomService.selectRoom(room);

  }
  submitRating(room: Room) {

    if (this.userId === null) {
      alert("User is not logged in. Please log in to submit a rating.");
      return;
    }
    if (!room.userRating) {
      alert("Please select a rating before submitting.");
      return;
    }

    const rating: Rating = {
      userId: this.userId,
      roomId: room.id_Room,
      score: room.userRating
    };

    this.bookRoomService.submitRating(rating).subscribe({
      next: () => alert("Rating submitted successfully!"),
      error: (err) => alert(err.error || "Error submitting rating."),
    });
  }
  getAverageRating(roomId: number): void {
    this.bookRoomService.getAverageRating(roomId).subscribe({
      next: (response) => {
        if (response && response.averageRating !== undefined && response.averageRating !== 0) {
          this.averageRatings[roomId] = response.averageRating;
        } else {
          console.log(`No valid average rating for roomId ${roomId}. Skipping.`);
        }
      },
      error: (err) => {
        console.error('Error fetching average rating:', err);
      }
    });
  }
  updateRoomPrice(room: Room): void {
    const updatedPrice = prompt('Enter the new price:', room.pricePerNight.toString());
    if (updatedPrice && !isNaN(Number(updatedPrice))) {
      room.pricePerNight = Number(updatedPrice);
      this.bookRoomService.updateRoom(room).subscribe({
        next: () => alert('Room price updated successfully!'),
        error: (err) => alert('Error updating room price: ' + err),
      });
    }
  }

  updateRoomView(room: Room): void {
    const updatedViewType = prompt('Enter the new view type (0 for Street, 1 for Mountain):', room.viewType.toString());
    if (updatedViewType !== null && !isNaN(Number(updatedViewType))) {
      room.viewType = Number(updatedViewType);
      this.bookRoomService.updateRoom(room).subscribe({
        next: () => alert('Room view updated successfully!'),
        error: (err) => alert('Error updating room view: ' + err),
      });
    }
  }

  updateRoomBreakfast(room: Room): void {
    const updatedBreakfast = confirm('Do you want to include breakfast?');
    room.hasBreakfastIncluded = updatedBreakfast;
    this.bookRoomService.updateRoom(room).subscribe({
      next: () => alert('Room breakfast option updated successfully!'),
      error: (err) => alert('Error updating room breakfast option: ' + err),
    });
  }

  updateRoomMealPlan(room: Room): void {
    const updatedMealPlan = prompt('Enter the new meal plan (0 for Standard, 1 for All Inclusive):', room.mealPlan.toString());
    if (updatedMealPlan !== null && !isNaN(Number(updatedMealPlan))) {
      room.mealPlan = Number(updatedMealPlan);
      this.bookRoomService.updateRoom(room).subscribe({
        next: () => alert('Room meal plan updated successfully!'),
        error: (err) => alert('Error updating room meal plan: ' + err),
      });
    }
  }

  updateRoomAvailability(room: Room): void {
    const updatedAvailability = confirm('Do you want to mark this room as available?');
    room.isAvailable = updatedAvailability;
    this.bookRoomService.updateRoom(room).subscribe({
      next: () => alert('Room availability updated successfully!'),
      error: (err) => alert('Error updating room availability: ' + err),
    });
  }


}
