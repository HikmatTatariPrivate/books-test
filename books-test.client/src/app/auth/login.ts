import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './auth.html',
  styleUrl: './auth.css',
})
export class Login {
  title = "Sign in"

  constructor(private fb: FormBuilder) { }
  form: any;


  ngOnInit(): void {
    this.form = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }


  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const payload = {
      username: this.form.value.username,
      password: this.form.value.password
    };
  }
}
