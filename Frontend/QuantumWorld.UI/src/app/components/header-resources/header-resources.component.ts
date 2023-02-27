import { Component, Input } from '@angular/core';
import { User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-header-resources',
  templateUrl: './header-resources.component.html',
  styleUrls: ['./header-resources.component.css']
})
export class HeaderResourcesComponent {
  @Input() user!: User;
  users: User[] = [];
  carbonFiberResource: number;

  constructor(private userService: UserService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
    // this.userService.getUser(this.user.username).subscribe((result: User) => {this.user = result;});
    // console.log(this.user.username);
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }
}





