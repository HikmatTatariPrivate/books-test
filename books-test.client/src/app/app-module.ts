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
import { ReactiveFormsModule } from '@angular/forms';
import { authorizeInterceptor } from './authorize-interceptor';

@NgModule({
  declarations: [
    App,
    Login,
    SignUp,
    MainPage
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    NgbModule,
    FontAwesomeModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([authorizeInterceptor]))
  ],
  bootstrap: [App]
})
export class AppModule { }
