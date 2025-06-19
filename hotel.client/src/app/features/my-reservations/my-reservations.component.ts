import { Component, OnInit } from '@angular/core';
import { BookingService } from '../../services/booking.service';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-my-reservations',
  templateUrl: './my-reservations.component.html',
  styleUrls: ['./my-reservations.component.css']
})
export class MyReservationsComponent implements OnInit {
  paginatedReservations: BookGetDTO[] = [];
  reservations: BookGetDTO[] = [];
  userId: number | null = null;
  currentPage: number = 1;
  itemsPerPage: number = 8;
  totalPages: number = 1;

  constructor(
    private bookingService: BookingService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.authService.getUserId().subscribe({
      next: (id) => {
        this.userId = id;
        this.fetchReservations(id);
      },
      error: (err) => {
        console.error('Error fetching user ID:', err);
      }
    });
  }

  fetchReservations(userId: number): void {
    if (userId !== null) {
      this.bookingService.getBookingsByUser(userId).subscribe({
        next: (data) => {
          console.log('Reservations fetched:', data);
          this.reservations = data;
          this.currentPage = 1;
          this.updatePagination();
        },
        error: (err) => {
          console.error('Failed to fetch reservations:', err);
          console.log('Error response body:', err.error);
        }
      });
    }
  }
  updatePagination(): void {
    this.totalPages = Math.ceil(this.reservations.length / this.itemsPerPage);
    this.paginate();
  }

  // Paginate the reservations
  paginate(): void {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.paginatedReservations = this.reservations.slice(start, end);
  }

  // Change page for pagination
  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.paginate();
  }
}
export interface BookGetDTO {
  id: number;
  userId: number;
  roomId: number;
  pricePerNight: number;
  status: BookingStatus;
  checkInDate: string;
  checkOutDate: string;
  paymentStatus: PaymentStatus;
}

export enum BookingStatus {
  Pending = 'Pending',
  Confirmed = 'Confirmed',
  Canceled = 'Canceled',
  CheckedIn = 'CheckedIn',
  CheckedOut = 'CheckedOut'
}

export enum PaymentStatus {
  Pending = 'Pending',
  Paid = 'Paid',
  Failed = 'Failed',
  Refunded = 'Refunded'
} 
