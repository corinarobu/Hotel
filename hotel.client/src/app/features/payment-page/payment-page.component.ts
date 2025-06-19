import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookRoomService, Room, Faq } from '../../services/book-room.service';
import { PaymentserviceService } from '../../services/paymentservice.service';
import { AuthService } from '../../services/auth.service';
import { BookingService } from '../../services/booking.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-payment-page',
  templateUrl: './payment-page.component.html',
  styleUrls: ['./payment-page.component.css']
})
export class PaymentPageComponent implements OnInit {
  room: Room = {} as Room;
  checkInDate!: string;
  checkOutDate!: string;
  pricePerNight: number | null = null;
  userId: number | null = null;
  todayDate: string = '';
  minCheckoutDate: string = '';
  faqs: Faq[] = [];
  hasBankAccount: boolean = true;
  roomBookedMessage: string = '';
  userFullName: string|null = null;

  constructor(
    private bookRoomService: BookRoomService,
    private paymentService: PaymentserviceService,
    private bookingService: BookingService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private cd: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.todayDate = new Date().toISOString().split('T')[0];
    this.userFullName = this.authService.getUserFullName();
    this.authService.getUserId().subscribe({
      next: (id) => {
        this.userId = id;
        this.checkBankAccount(id);
        console.log(this.checkBankAccount(id));
      },
      error: (err) => {
        console.error('Error fetching user ID:', err);
      }
    });

    const roomId = this.route.snapshot.paramMap.get('roomId');
    if (roomId) {
      this.bookRoomService.getRoomById(parseInt(roomId, 10)).subscribe({
        next: (room) => {
          if (room) {
            this.room = room;
            this.pricePerNight = room.pricePerNight;
            this.loadFaqs();
          } else {
            this.router.navigate(['/']);
          }
        },
        error: (err) => {
          console.error('Error fetching room:', err);
          this.router.navigate(['/']);
        }
      });
    }
  }

  checkBankAccount(userId: number): void {
    this.paymentService.hasBankAccount(userId).subscribe({
      next: (hasAccount) => {
        this.hasBankAccount = hasAccount;
      },
      error: (err) => {
        console.error('Error checking bank account:', err);
        this.hasBankAccount = false;
      }
    });
  }

  loadFaqs(): void {
    this.bookRoomService.getAllFaqs().subscribe({
      next: (faqs) => {
        this.faqs = faqs.filter(faq => faq.idRoom === this.room.id_Room);
      },
      error: (err) => {
        console.error('Error fetching FAQs:', err);
      }
    });
  }

  updateCheckoutMinDate(): void {
    if (this.checkInDate) {
      const nextDay = new Date(this.checkInDate);
      nextDay.setDate(nextDay.getDate() + 1);
      this.minCheckoutDate = nextDay.toISOString().split('T')[0];
    }
  }

  getTotalPrice(): number {
    if (!this.room || !this.checkInDate || !this.checkOutDate || !this.pricePerNight) return 0;

    const checkIn = new Date(this.checkInDate);
    const checkOut = new Date(this.checkOutDate);

    if (checkOut <= checkIn) return 0;

    const nights = Math.ceil((checkOut.getTime() - checkIn.getTime()) / (1000 * 60 * 60 * 24));
    return this.pricePerNight * nights;
  }

  bookRoom(): void {
    if (!this.userId) {
      console.error('User ID is missing. Ensure user is logged in.');
      return;
    }

    if (!this.hasBankAccount) {
      alert('You need to add a bank account before booking.');
      return;
    }

    if (!this.room || !this.checkInDate || !this.checkOutDate) return;

    // Initialize or reset the booking status message
    this.roomBookedMessage = '';

    this.bookingService.hasUserBookedRoomInAPeriod(this.room.id_Room, this.checkInDate, this.checkOutDate).subscribe({
      next: (response) => {
        if (response === true) {
          this.roomBookedMessage = 'This room is already booked for the selected dates.';
          this.cd.detectChanges();
        } else {
          const bookingData = {
            userId: this.userId,
            roomId: this.room.id_Room,
            pricePerNight: this.room.pricePerNight,
            checkInDate: this.checkInDate,
            checkOutDate: this.checkOutDate,
          };

          this.paymentService.bookRoom(bookingData).subscribe(
            (bookingResponse) => {
              const totalAmount = this.getTotalPrice();

              this.paymentService.createCheckoutSession(totalAmount, bookingResponse.id).subscribe(
                (paymentResponse) => {
                  if (paymentResponse.url) {
                    window.location.href = paymentResponse.url;
                  } else {
                    console.error('Payment URL missing:', paymentResponse);
                  }
                },
                (paymentError) => {
                  console.error('Payment creation failed:', paymentError);
                }
              );
            },
            (bookingError) => {
              console.error('Booking Failed:', bookingError);
            }
          );
        }
      },
      error: (error) => {
        console.error('Error checking booking status:', error);
      }
    });
  }

}
