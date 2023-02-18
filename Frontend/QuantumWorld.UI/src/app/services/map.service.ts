import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EnemyType } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  private url = "Battle";
  constructor(private http: HttpClient) { }

  public startBattle(type: EnemyType, email: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      email: email,
    }
    console.log(body);
    console.log(JSON.stringify(body));
    console.log(`${environment.apiUrl}/${this.url}`);
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }
}
