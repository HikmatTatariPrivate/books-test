import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Citation {
  id: number;
  text: string;
  userUsername?: string;
}

@Injectable({
  providedIn: 'root'
})
export class CitationsService {

  private apiUrl = 'https://localhost:5238/api/citations';

  constructor(private http: HttpClient) { }

  getCitations(): Observable<Citation[]> {
    return this.http.get<Citation[]>(this.apiUrl);
  }

  getCitation(id: number): Observable<Citation> {
    return this.http.get<Citation>(`${this.apiUrl}/${id}`);
  }

  addCitation(citation: Citation): Observable<Citation> {
    return this.http.post<Citation>(this.apiUrl, citation);
  }

  updateCitation(id: number, citation: Citation): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, citation);
  }

  deleteCitation(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
