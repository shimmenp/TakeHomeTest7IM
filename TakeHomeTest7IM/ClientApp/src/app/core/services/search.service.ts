import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonDto } from '../models/personDto';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient) { }

  search(searchTerm: string): Observable<PersonDto[]> {
    const url = `/api/search?searchTerm=${searchTerm}`;
    return this.http.get<PersonDto[]>(url);
  }
}
