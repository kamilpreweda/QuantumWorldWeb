import { Component } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  errorMessage: string | null = null;
  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(3), Validators.pattern('[a-zA-Z]+$')]),
    password: new FormControl('', [Validators.required, Validators.minLength(8)]),
  })

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.changePage('Overview');
  }

  register() {
    this.userService.register(this.username!.value!.toString(), this.password!.value!.toString()).subscribe(() => {
      alert("Registration succesful.");
    },
      (error) => {
        alert(error.error);
      });
  }

  async login() {
    var tokenId = crypto.randomUUID();
    this.userService.login(tokenId, this.username!.value!.toString(), this.password!.value!).subscribe((token: string) => {
      if (token) {
        localStorage.setItem("authToken", token);
        window.location.reload();
      }
    }, (error) => {
      alert(error.error);
    });
  }

  changePage(path: string): void {
    if (this.loggedIn()) {
      const navigationDetails: string[] = []
      navigationDetails.push(path);
      this.router.navigate(navigationDetails);
    }
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