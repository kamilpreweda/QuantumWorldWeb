import { Component } from '@angular/core';
import { ResearchType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-header-resources',
  templateUrl: './header-resources.component.html',
  styleUrls: ['./header-resources.component.css']
})
export class HeaderResourcesComponent {
  user: User;
  users: User[] = [];

  constructor(private userService: UserService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }  
}


