import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-admin-searchbar',
  templateUrl: './admin-searchbar.component.html',
  styleUrl: './admin-searchbar.component.css'
})
export class AdminSearchbarComponent {
  searchTerm: string = '';

  @Output() search = new EventEmitter<string>();

  onSearch() {
    this.search.emit(this.searchTerm); 
  }
}
