import { Component } from '@angular/core';
import { Resource, User } from './models/user';
import { JwtTokenService } from './services/jwt-token.service';
import { UserService } from './services/user.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'QuantumWorld.UI';
  user: User;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
