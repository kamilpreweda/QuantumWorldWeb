import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[a-zA-Z]+$')]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
  })

  constructor(private userService: UserService, private router: Router) { }

  register() {
    this.userService.register(this.username!.value!.toString(), this.password!.value!.toString()).subscribe();
    console.log(this.loginForm.value);
  }

  login() {
    var tokenId = crypto.randomUUID();
    this.userService.login(tokenId, this.username!.value!.toString(), this.password!.value!.toString()).subscribe((token: string) => {
      if (token) {
        localStorage.setItem("authToken", token);
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

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }
}