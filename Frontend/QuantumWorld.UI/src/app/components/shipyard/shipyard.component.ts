import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-shipyard',
  templateUrl: './shipyard.component.html',
  styleUrls: ['./shipyard.component.css']
})
export class ShipyardComponent {
  user: User;
  users: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  changeDisplayedName(name: string): string {
    var changedName = name.replace("Resource", "");
    changedName = changedName.replace(/[^A-Z]+/g, "");
    return changedName;
  }

  addSpaces(name: string): string {
    var changedName = name.replace(/([A-Z])/g, ' $1').trim();
    return changedName;
  }

  numberWithDots(number: number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
}
