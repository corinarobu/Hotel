import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-our-rooms',
  templateUrl: './our-rooms.component.html',
  styleUrl: './our-rooms.component.css'
})
export class OurRoomsComponent {
  userFullName: string | null = null;
  constructor(private authService: AuthService) { }
  ngOnInit(): void {
    this.userFullName = this.authService.getUserFullName();
  }
}
