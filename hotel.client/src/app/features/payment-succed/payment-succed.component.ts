import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentserviceService } from '../../services/paymentservice.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-payment-succed',
  templateUrl: './payment-succed.component.html',
  styleUrls: ['./payment-succed.component.css']
})
export class PaymentSuccedComponent implements OnInit {
  bookingId: number | null = null;
  userFullName: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentserviceService,
    private router: Router,
    private authService : AuthService
  ) { }

  ngOnInit(): void {
    this.userFullName = this.authService.getUserFullName();
    this.route.queryParams.subscribe(params => {
      this.bookingId = +params['bookingId'] || null;
      if (!this.bookingId) {
        console.error('Booking Id is missing!');
      }
    });
  }

  confirmPayment(): void {
    if (!this.bookingId) {
      console.error('Cannot confirm payment: bookingId is null.');
      return;
    }

    this.paymentService.updatePaymentStatus(this.bookingId, 'Paid').subscribe({
      next: () => {
        console.log('Payment status updated.');
        this.router.navigate(['/main-page']); // or /payment-success
      },
      error: (err) => {
        console.error('Failed to update payment status:', err);
      }
    });
  }
}
