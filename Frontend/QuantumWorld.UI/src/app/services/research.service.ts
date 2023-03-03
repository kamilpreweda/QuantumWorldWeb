import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResearchType } from '../models/user';


@Injectable({
  providedIn: 'root'
})
export class ResearchService {
  private url = "Research";
  private dateUrl = "Research/date";

  constructor(private http: HttpClient) { }

  public upgradeResearch(type: ResearchType, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
    }
    return this.http.post(`${environment.apiUrl}/${this.url}`, JSON.stringify(body), { headers })
  }

  public setConstructionStartDate(type: ResearchType, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      type: type,
      username: username,
      date: new Date().toJSON()
    }
    return this.http.post(`${environment.apiUrl}/${this.dateUrl}`, JSON.stringify(body), { headers }).subscribe((res: any) => {
      console.log(res);
    });
  };
}
