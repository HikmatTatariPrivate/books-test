import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-books',
  standalone: false,
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books {
  faPlus = faPlus;
  constructor(private auth: AuthService) { }

  addBook() {
  }
  
}
