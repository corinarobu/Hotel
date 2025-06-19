import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7250/api/Product';

  constructor(private http: HttpClient, private authService: AuthService) { }

  getAllProducts(): Observable<Products[]> {
    return this.http.get<Products[]>(this.apiUrl);
  }
  updateProduct(product:Products): Observable<Products> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`,
      'Content-Type': 'application/json',
    });

    return this.http.put<Products>(`${this.apiUrl}/${product.id_Product}`, product, { headers });
  }

}
export interface Products {
  id_Product: number;
  name_Product: string;
  description_Product: string;
  typeofproduct: string;
  price: number;
  meal_Id: number;
}
