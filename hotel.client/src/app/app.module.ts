import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './features/main-page/components/login/login.component';
import { RegisterComponent } from './features/main-page/components/register/register.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MainPageComponent } from './features/main-page/main-page.component';
import { NavBarComponent } from './features/main-page/components/nav-bar/nav-bar.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { MapComponent } from './features/main-page/components/map/map.component';
import { PhotoCarouselComponent } from './features/main-page/components/photo-carousel/photo-carousel.component';
import { HotelFacilitiesComponent } from './features/main-page/components/hotel-facilities/hotel-facilities.component';
import { OurRoomsComponent } from './features/main-page/components/our-rooms/our-rooms.component';
import { RestaurantComponent } from './features/main-page/components/restaurant/restaurant.component';
import { MenuComponent } from './features/main-page/components/menu/menu.component';
import { BookRoomComponent } from './features/main-page/components/book-room/book-room.component';
import { CancelPaymentComponent } from './features/cancel-payment/cancel-payment.component';
import { PaymentSuccedComponent } from './features/payment-succed/payment-succed.component';
import { PaymentPageComponent } from './features/payment-page/payment-page.component';
import { AddBankComponent } from './features/add-bank/add-bank.component';
import { RecomandationsComponent } from './features/main-page/components/recomandations/recomandations.component';
import { AdminDashboardComponent } from './features/admin-dashboard/admin-dashboard.component';
import { AdminSearchbarComponent } from './features/admin-dashboard/components/admin-searchbar/admin-searchbar.component';
import { MyReservationsComponent } from './features/my-reservations/my-reservations.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MainPageComponent,
    NavBarComponent,
    MapComponent,
    PhotoCarouselComponent,
    HotelFacilitiesComponent,
    OurRoomsComponent,
    RestaurantComponent,
    MenuComponent,
    BookRoomComponent,
    CancelPaymentComponent,
    PaymentSuccedComponent,
    PaymentPageComponent,
    AddBankComponent,
    RecomandationsComponent,
    AdminDashboardComponent,
    AdminSearchbarComponent,
    MyReservationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    ReactiveFormsModule,
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
