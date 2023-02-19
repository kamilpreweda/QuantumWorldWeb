import { Component, Input } from '@angular/core';
import { BuildingType, Resource, User } from 'src/app/models/user';
import { BuildingService } from 'src/app/services/building.service';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent {
  user: User;
  users: User[] = [];
  type: BuildingType;
  email: string = "string";

  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private buildingService: BuildingService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  build(type: BuildingType): void {
    this.buildingService.upgradeBuilding(type, this.email).subscribe(() => {
      window.location.reload();
    });
    console.log("button clicked");
  }
}

