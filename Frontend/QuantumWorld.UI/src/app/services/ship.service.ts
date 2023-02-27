import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ShipType } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private url = "Ships";

  constructor(private http: HttpClient) { }

  public buildShip(type: ShipType, count: number, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      count: count,
      username: username,
    }
    console.log(type);
    console.log(count);
    console.log(username);
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }
}
