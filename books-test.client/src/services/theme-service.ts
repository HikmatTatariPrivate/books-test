import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private dark = false;

  constructor() {
    // Restore last theme from localStorage
    const savedTheme = localStorage.getItem('dark-theme');
    this.dark = savedTheme === 'true'; // true if saved, false otherwise
    this.updateBodyClass();
  }

  // Toggle between dark and light theme
  toggleDarkTheme(): void {
    this.dark = !this.dark;
    localStorage.setItem('dark-theme', this.dark ? 'true' : 'false');
    this.updateBodyClass();
  }

  // Apply theme class to body
  private updateBodyClass(): void {
    if (this.dark) {
      document.body.classList.add('dark-theme');
    } else {
      document.body.classList.remove('dark-theme');
    }
  }

  // Check if dark theme is active
  isDarkTheme(): boolean {
    return this.dark;
  }
}
