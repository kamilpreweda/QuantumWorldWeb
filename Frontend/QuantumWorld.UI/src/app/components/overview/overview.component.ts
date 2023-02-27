import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {
  user: User;
  users: User[] = [];

  constructor(private userService: UserService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }
}
