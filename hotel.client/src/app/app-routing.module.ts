import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './features/main-page/components/login/login.component';
import { RegisterComponent } from './features/main-page/components/register/register.component';
import { MainPageComponent } from './features/main-page/main-page.component';
import { AuthGuard } from './auth.guard';
import { MenuComponent } from './features/main-page/components/menu/menu.component';
import { BookRoomComponent } from './features/main-page/components/book-room/book-room.component';
import { PaymentPageComponent } from './features/payment-page/payment-page.component';
import { AddBankComponent } from './features/add-bank/add-bank.component';
import { CancelPaymentComponent } from './features/cancel-payment/cancel-payment.component';
import { PaymentSuccedComponent } from './features/payment-succed/payment-succed.component';
import { AdminDashboardComponent } from './features/admin-dashboard/admin-dashboard.component';
import { MyReservationsComponent } from './features/my-reservations/my-reservations.component';

const routes: Routes = [
  { path: '', redirectTo: 'mainpage', pathMatch: 'full' }, 
  { path: 'mainpage', component: MainPageComponent }, 
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'book-room', component: BookRoomComponent },
  { path: 'payment-page/:roomId', component: PaymentPageComponent },
  { path: 'add-bank', component: AddBankComponent },
  { path: 'cancel-payment', component: CancelPaymentComponent },
  { path: 'succed-payment', component: PaymentSuccedComponent },
  { path: 'admin-dashboard', component:AdminDashboardComponent},
  { path: 'my-reservations', component: MyReservationsComponent},
  { path: '**', redirectTo: 'mainpage' } 
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
