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
  private registerUrl = "Users/register";
  private loginUrl = "Login";

  constructor(private http: HttpClient) { }

  public getUsers(): Observable<User[]> {

    return this.http.get<User[]>(`${environment.apiUrl}/${this.url}`);
  }

  public register(user: User): Observable<any> {
    return this.http.post(`${environment.apiUrl}/${this.registerUrl}`, user);
  }

  public login(user: User): Observable<string> {
    return this.http.post(`${environment.apiUrl}/${this.loginUrl}`, user, { responseType: 'text' });
  }
}
