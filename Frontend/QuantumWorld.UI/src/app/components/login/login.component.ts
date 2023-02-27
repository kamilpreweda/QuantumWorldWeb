import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string
  password: string
  display: boolean = true;

  constructor(private userService: UserService, private router: Router) { }

  register() {
    this.userService.register(this.username, this.password).subscribe();
  }

  login() {
    var tokenId = crypto.randomUUID();
    this.userService.login(tokenId, this.username, this.password).subscribe((token: string) => {
      if (token) {
        localStorage.setItem("authToken", token);
        // this.userService.getUser(this.username).subscribe()
        window.location.reload();
      }
      else {
        console.log("Invalid credentials");
      }
    });
  }

  changePage(path: string): void {
    const navigationDetails: string[] = []
    navigationDetails.push(path);
    this.router.navigate(navigationDetails);
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }
}