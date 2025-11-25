import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';


@Component({
  selector: 'app-books',
  standalone: false,
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books {
  constructor( private auth: AuthService) { }
  
}
