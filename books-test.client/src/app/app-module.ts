import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Login } from './auth/login';
import { SignUp } from './auth/signup';
import { MainPage } from './mainpage/mainpage'; 

import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { authorizeInterceptor } from './authorize-interceptor';
import { Books } from './books/books';
import { Citations } from './citations/citations';


@NgModule({
  declarations: [
    App,
    Login,
    SignUp,
    MainPage,
    Books,
    Citations
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    NgbModule,
    FormsModule,
    FontAwesomeModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([authorizeInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
