import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BuildingType } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class BuildingService {
  private url = "Buildings";

  constructor(private http: HttpClient) { }

  public upgradeBuilding(type: BuildingType, email: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      email: email,
    }
    console.log(`${environment.apiUrl}/${this.url}`);
    console.log(body);
    console.log(JSON.stringify(body));
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  };
}

