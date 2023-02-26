import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user: User;


  constructor(private userService: UserService) { }

  register(user: User) {
    this.userService.register(user.username, user.password).subscribe();
  }

  login(user: User) {
    var tokenId = crypto.randomUUID();
    this.userService.login(tokenId, user.username, user.password).subscribe((token: string) => {
      localStorage.setItem("authToken", token);
    });
  }
}