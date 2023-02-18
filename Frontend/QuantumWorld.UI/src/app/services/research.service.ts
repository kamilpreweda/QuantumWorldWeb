import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResearchType } from '../models/user';


@Injectable({
  providedIn: 'root'
})
export class ResearchService {
  private url = "Research";

  constructor(private http: HttpClient) { }

  public upgradeResearch(type: ResearchType, email: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      email: email,
    }
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }
}
