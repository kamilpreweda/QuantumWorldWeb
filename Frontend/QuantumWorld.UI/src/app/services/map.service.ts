import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EnemyType } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MapService {
  private url = "Battle";
  private dateUrl = "Battle/date"
  constructor(private http: HttpClient) { }

  public startBattle(type: EnemyType, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
    }
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }

  public setAttackStartDate(type: EnemyType, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
      date: new Date().toJSON()
    }
    return this.http.post(`${environment.apiUrl}/${this.dateUrl}`, JSON.stringify(body), { headers });
  };
}

