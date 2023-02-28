import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  user: User;
  constructor(private router: Router, private userService: UserService, private jwtTokenService: JwtTokenService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  changePage(path: string): void {
    const navigationDetails: string[] = []
    navigationDetails.push(path);
    this.router.navigate(navigationDetails);
  }

  changeAppearance(buttonId: string): void {
    const menuButton = document.getElementById(buttonId);
    if (menuButton === null) {
      return;
    }
    const buttons = document.getElementsByClassName("menuButton");
    Array.prototype.filter.call(
      buttons,
      (button) => { button.style.borderWidth = "0px", button.style.color = '#FFFFFF' }

    );
    menuButton.style.borderWidth = "1px";
    menuButton.style.borderColor = "#00FFFF";
    menuButton.style.color = '#FFD700';
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  onLogout() {
    localStorage.removeItem("authToken");
    window.location.reload;
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
