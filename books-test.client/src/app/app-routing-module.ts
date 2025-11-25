import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './auth/login';
import { SignUp } from './auth/signup';
import { MainPage } from './mainpage/mainpage';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', pathMatch: 'full', component: Login },
  { path: 'signup', pathMatch: 'full', component: SignUp },
  { path: 'mainpage', pathMatch: 'full', component: MainPage },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)], 
  exports: [RouterModule]
})
export class AppRoutingModule { }
