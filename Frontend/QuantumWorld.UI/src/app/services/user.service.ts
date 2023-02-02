import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = "Users";

  constructor(private http: HttpClient) { }

  public getUsers(): Observable<User[]> {

    return this.http.get<User[]>(`${environment.apiUrl}/${this.url}`);
  }
}
