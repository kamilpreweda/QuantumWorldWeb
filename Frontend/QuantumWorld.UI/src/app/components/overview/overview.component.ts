import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {
  user: User;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
