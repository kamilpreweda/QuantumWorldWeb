import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent {
  user: User;
  users: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  changeDisplayedName(name: string): string {
    var changedName: string;

    if (name.includes("Resource")) {
      changedName = name.replace("Resource", "");
      changedName = changedName.replace(/[^A-Z]+/g, "");
      return changedName;
    }
    else if (name.includes("Research")) {
      changedName = name.replace("Research", "");
      return changedName;
    }
    return changedName = name;
  }

  addSpaces(name: string): string {
    var changedName = name.replace(/([A-Z])/g, ' $1').trim();
    return changedName;
  }
}

