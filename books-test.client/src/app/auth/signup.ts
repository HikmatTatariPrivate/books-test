import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { HttpErrorResponse } from '@angular/common/http'; 


@Component({
  selector: 'app-signup',
  standalone: false,
  templateUrl: './auth.html',
  styleUrl: './auth.css',
})
export class SignUp {
  title = "Sign up"
  submitError = ""

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }
  form: any;


  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
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

    this.auth.signup(payload).subscribe({
      complete: () => {
        this.router.navigate(['/']);
      },
      error: (error: HttpErrorResponse) => {
        this.submitError = error.error.message;
      }
    });
  }
}
