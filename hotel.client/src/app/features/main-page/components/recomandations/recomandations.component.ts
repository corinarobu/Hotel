import { Component, OnInit } from '@angular/core';
import { RecomandationServiceService, Recommendation } from '../../../../services/recomandation-service.service';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-recomandations',
  templateUrl: './recomandations.component.html',
  styleUrls: ['./recomandations.component.css']
})
export class RecomandationsComponent implements OnInit {
  recommendations: Recommendation[] = [];
  paginatedRecommendations: Recommendation[] = []; 
  newRecommendation: Recommendation = {
    id_Recomandation: 0,
    name: '',
    description: '',
    address: '',
    entryFee: '',
    distanceFromHotel: '',
    userId: 0
  };
  showForm: boolean = false;
  userId: number | null = null;
  userFullName: string | null = null;
  currentPage: number = 1;
  itemsPerPage: number = 5;
  totalPages: number = 1;
  isAdmin: boolean = false;

  constructor(private recomandationService: RecomandationServiceService, private authService: AuthService) { }

  ngOnInit() {
    this.userFullName = this.authService.getUserFullName();
    this.isAdmin = this.authService.isUserAdmin();
    this.authService.getUserId().subscribe({
      next: (id) => {
        this.userId = id;
      },
      error: (err) => {
        console.error('Error fetching user ID:', err);
      }
    });

    this.loadRecommendations();
  }

  loadRecommendations() {
    this.recomandationService.getAllRecomandations().subscribe(
      (recommandations) => {
        this.recommendations = recommandations;
        this.updatePagination(); 
      },
      (error) => console.error('Error fetching recommendations', error)
    );
  }

  addRecommendation() {
    if (this.userId === null) {
      console.error('User ID is missing. Ensure user is logged in.');
      return;
    }
    if (!this.newRecommendation.description.trim() || !this.newRecommendation.address.trim()) return;

    this.recomandationService.addRecomandation({ ...this.newRecommendation, userId: this.userId! }).subscribe(
      (newRec: Recommendation) => {
        this.recommendations.push(newRec);
        this.newRecommendation = { id_Recomandation: 0, name: '', description: '', address: '', entryFee: '', distanceFromHotel: '', userId: this.userId! };
        this.updatePagination(); 
      },
      (error) => console.error('Error adding recommendation', error)
    );
  }

  deleteRecommendation(id: number) {
    if (!id) {
      console.error('Invalid ID:', id);
      return;
    }

    console.log('Deleting recommendation with ID:', id);
    this.recomandationService.deleteRecomandation(id).subscribe(
      () => {
        this.recommendations = this.recommendations.filter((rec) => rec.id_Recomandation !== id);
        this.updatePagination(); 
      },
      (error) => console.error('Error deleting recommendation', error)
    );
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  updatePagination(): void {
    this.totalPages = Math.ceil(this.recommendations.length / this.itemsPerPage);
    this.paginate();
  }

  paginate(): void {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.paginatedRecommendations = this.recommendations.slice(start, end);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.paginate();
    }
  }
}
