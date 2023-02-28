import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {

  jwtToken: string;
  decodedToken: { [key: string]: string };

  constructor() { }

  getDecodedToken(token: string): any {
      return jwt_decode(token)
    }

  getUsernameFromToken(): string{
    const token = localStorage.getItem("authToken");
    const decodedToken = this.getDecodedToken(token!);
    const username = decodedToken.name;
    return username;
  }
  }


