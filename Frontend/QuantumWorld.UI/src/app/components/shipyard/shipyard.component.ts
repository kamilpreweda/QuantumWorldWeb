import { Component } from '@angular/core';
import { ShipType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { ShipService } from 'src/app/services/ship.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-shipyard',
  templateUrl: './shipyard.component.html',
  styleUrls: ['./shipyard.component.css']
})
export class ShipyardComponent {
  user: User;
  users: User[] = [];
  type: ShipType;
  email: string = "string";

  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private shipService: ShipService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  build(type: ShipType): void {
    this.shipService.buildShip(type, this.email).subscribe(() => {
      window.location.reload();
    });
  }
}
