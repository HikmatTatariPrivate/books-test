import { Component } from '@angular/core';
import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-mainpage',
  standalone: false,
  templateUrl: './mainpage.html',
  styleUrl: './mainpage.css',
})
export class MainPage {
  faLogout = faRightFromBracket;
  active = 'books';

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    const token = localStorage.getItem('access_token');
    if (!token) {
      this.router.navigate(['/']);
    }
  }

  logout() {
    this.auth.clearTokens();
    this.router.navigate(['/']);


  }
  
}
