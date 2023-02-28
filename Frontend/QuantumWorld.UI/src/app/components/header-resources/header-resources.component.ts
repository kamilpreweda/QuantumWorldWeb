import { Component, Input } from '@angular/core';
import { User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ResourceService } from 'src/app/services/resource.service';

@Component({
  selector: 'app-header-resources',
  templateUrl: './header-resources.component.html',
  styleUrls: ['./header-resources.component.css']
})
export class HeaderResourcesComponent {
  user: User;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, private resourceService: ResourceService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; this.increaseResource(); });
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  increaseResource() {
    let intervalId; {
      intervalId = setInterval(() => {
        this.resourceService.GenerateResources(this.user.resources);
      }, 1000);
    }
  }
}





