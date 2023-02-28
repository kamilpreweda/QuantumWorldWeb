import { Component } from '@angular/core';
import { Message, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { MessageService } from 'src/app/services/message.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent {
  user: User;
  message: Message;    

  constructor(private userService: UserService, private messageService: MessageService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  delete(id: number): void {
    this.messageService.deleteMessage(id, this.user.username).subscribe(() => {
      window.location.reload();
    })
  }
  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
