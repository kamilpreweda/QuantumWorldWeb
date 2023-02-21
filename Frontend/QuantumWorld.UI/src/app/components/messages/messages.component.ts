import { Component } from '@angular/core';
import { Message, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { MessageService } from 'src/app/services/message.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent {
  user: User;
  users: User[] = [];
  message: Message;    

  constructor(private userService: UserService, private messageService: MessageService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0]; console.log(this.user.messages) });
  }

  delete(id: number): void {
    this.messageService.deleteMessage(id, this.user.email).subscribe(() => {
      window.location.reload();
    })
  }
}
