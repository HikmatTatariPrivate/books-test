import { Component, signal } from '@angular/core';
import { ThemeService } from '../services/theme-service';


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App  {
  protected readonly title = signal('books-test.client');

  constructor(public themeService: ThemeService) {
  }


  toggle() {
    this.themeService.toggleDarkTheme();
  }
}
