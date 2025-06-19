import { Component, OnInit } from '@angular/core';
import { AdminDashboardService, Users } from '../../../app/services/admin-dashboard.service';  
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  users: Users[] = [];
  isAdmin: boolean = false;

  constructor(private dashboardService: AdminDashboardService, private authService: AuthService) { }

  ngOnInit(): void {
    this.isAdmin = this.authService.isUserAdmin();
    this.loadUsers();
  }

  loadUsers() {
    this.dashboardService.getAllUsers().subscribe(users => {
      this.users = users;
    });
  }

  onSearch(searchTerm: string) {
    if (searchTerm) {
      this.dashboardService.searchUsers(searchTerm).subscribe(users => {
        this.users = users;
      });
    } else {
      this.loadUsers();
    }
  }

  deleteUser(userId: number) {
    this.dashboardService.deleteUser(userId).subscribe(() => {
      this.users = this.users.filter(user => user.id !== userId);
    });
  }
}
