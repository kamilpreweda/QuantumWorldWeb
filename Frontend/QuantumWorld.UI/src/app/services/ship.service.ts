import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ShipType } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private url = "Ships";
  private dateAndCountUrl = "Ships/date-and-count";

  constructor(private http: HttpClient) { }

  public buildShip(type: ShipType, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
    }
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }
  public setConstructionStartDate(type: ShipType, username: string, count: number) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
      count: count,
      date: new Date().toJSON()
    }
    return this.http.post(`${environment.apiUrl}/${this.dateAndCountUrl}`, JSON.stringify(body), { headers });
  }
}
