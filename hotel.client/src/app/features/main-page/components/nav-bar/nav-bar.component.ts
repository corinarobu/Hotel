import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  isAdmin: boolean = false;
  constructor(public authService: AuthService, private router: Router) { }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
  userFullName: string | null = null;

  ngOnInit(): void {
    this.isAdmin = this.authService.isUserAdmin();
    this.userFullName = this.authService.getUserFullName();
  }

}
