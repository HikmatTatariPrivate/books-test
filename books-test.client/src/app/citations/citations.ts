import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';


@Component({
  selector: 'app-citations',
  standalone: false,
  templateUrl: './citations.html',
  styleUrl: './citations.css',
})
export class Citations {
  constructor( private auth: AuthService) { }
  
}
