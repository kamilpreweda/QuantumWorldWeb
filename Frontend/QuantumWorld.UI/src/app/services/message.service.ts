import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private url = "Messages";

  constructor(private http: HttpClient) { }

  public deleteMessage(id:number, username: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8')
    let body = {
      id: id,
      username: username,
    }
    return this.http.delete(`${environment.apiUrl}/${this.url}/${body.id}/${body.username}`)
  }
}