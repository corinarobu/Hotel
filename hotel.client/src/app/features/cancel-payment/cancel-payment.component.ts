import { Component } from '@angular/core';
import { PaymentserviceService } from '../../services/paymentservice.service'; // adjust path if needed
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-cancel-payment',
  templateUrl: './cancel-payment.component.html',
  styleUrls: ['./cancel-payment.component.css']
})
export class CancelPaymentComponent {
  bookingId: number | null = null;

  constructor(
    private paymentService: PaymentserviceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Assume bookingId is passed as a query param or route param
    this.route.queryParams.subscribe(params => {
      this.bookingId = +params['bookingId'] || null;
      if (!this.bookingId) {
        console.error('Booking Id is missing!');
      }
    });
  }

  cancelBooking() {
    if (this.bookingId == null) {
      console.error('Booking ID is missing');
      return;
    }

    this.paymentService.cancelBooking(this.bookingId).subscribe({
      next: () => {
        alert('Booking canceled successfully.');
        this.router.navigate(['/mainpage']); // navigate after cancel
      },
      error: (err) => {
        console.error('Error canceling booking:', err);
        alert('Failed to cancel booking.');
      }
    });
  }
}
