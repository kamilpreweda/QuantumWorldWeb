import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  public register(username: string, password: string): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      username: username,
      password: password,
    }
    return this.http.post(`${environment.apiUrl}/${this.registerUrl}`, JSON.stringify(body), { headers });
  }

  public login(tokenId: string, username: string, password: string): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      tokenId: tokenId,
      username: username,
      password: password,
    }
    return this.http.post(`${environment.apiUrl}/${this.loginUrl}`, JSON.stringify(body), { headers });
  }
}
