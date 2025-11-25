import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './auth.html',
  styleUrl: './auth.css',
})
export class Login {
  title = "Sign in"
  submitError = ""

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }
  form: any;

  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });

    const token = localStorage.getItem('access_token');
    if (token) {
       this.router.navigate(['/mainpage']);
    }
  }

  submit() {
    this.submitError = ""
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const payload = {
      username: this.form.value.username,
      password: this.form.value.password
    };

    this.auth.login(payload).subscribe({
      next: (tokens) => {
        this.auth.storeTokens(tokens);
        this.router.navigate(['/mainpage']);
      },
      error: () => {
        this.submitError = 'Login failed. Please check your credentials and try again.';
      }
    });
  }
}
