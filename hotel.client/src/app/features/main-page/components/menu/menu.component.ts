import { Component, OnInit } from '@angular/core';
import { Products, ProductService } from '../../../../services/product.service';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  products: Products[] = [];
  breakfastProducts: Products[] = [];
  lunchProducts: Products[] = [];
  dinnerProducts: Products[] = [];
  drinksProducts: Products[] = [];
  isAdmin: boolean = false;

  constructor(
    private productService: ProductService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.isAdmin = this.authService.isUserAdmin();

    this.productService.getAllProducts().subscribe(
      (data) => {
        this.products = data;
        console.log(this.products);

        this.breakfastProducts = this.products.filter(p => p.meal_Id === 1);
        console.log(this.breakfastProducts);
        this.lunchProducts = this.products.filter(p => p.meal_Id === 3);
        this.dinnerProducts = this.products.filter(p => p.meal_Id === 2);
        this.drinksProducts = this.products.filter(p => p.meal_Id === 1002);
      },
      (error) => {
        console.error('Error fetching products', error);
      }
    );
  }

  updateProductPrice(product: Products): void {
    const updatedPrice = prompt('Enter the new price:', product.price.toString());
    if (updatedPrice && !isNaN(Number(updatedPrice))) {
      product.price = Number(updatedPrice);
      this.productService.updateProduct(product).subscribe({
        next: () => alert('Product price updated successfully!'),
        error: (err) => alert('Error updating product price: ' + err),
      });
    }
  }

  updateProductName(product: Products): void {
  const updatedName = prompt('Enter the new product name:', product.name_Product);
  if (updatedName !== null && updatedName.trim() !== '') {
    product.name_Product = updatedName.trim();
    this.productService.updateProduct(product).subscribe({
      next: (updatedProduct) => {
        alert('Product name updated successfully!');
      },
      error: (err) => {
        alert('Error updating product: ' + (err.error?.message || err.message));
      }
    });
  }
}

  updateProductDescription(product: Products): void {
    const updatedDescription = prompt('Enter the new product description:', product.description_Product);
    if (updatedDescription !== null && updatedDescription.trim() !== '') {
      product.description_Product = updatedDescription.trim();
      this.productService.updateProduct(product).subscribe({
        next: () => alert('Product description updated successfully!'),
        error: (err) => alert('Error updating product description: ' + err),
      });
    }
  }
}
