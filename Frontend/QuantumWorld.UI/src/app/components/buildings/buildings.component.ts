import { Component, Input } from '@angular/core';
import { BuildingType, Resource, User } from 'src/app/models/user';
import { BuildingService } from 'src/app/services/building.service';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';

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

  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private buildingService: BuildingService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
    console.log(this.user.username);
    // this.userService.getUser(this.user.username).subscribe((result: User) => { this.user = result; });
    // console.log(this.user);
  }

  build(type: BuildingType): void {
    if(this.canBuild(type)){
    this.buildingService.upgradeBuilding(type, this.email).subscribe(() => {
      window.location.reload();
    })
  };
  }

  canBuild(type: BuildingType): boolean {
    var building = this.user.buildings.find(b => (b.type === type));

    console.log(this.user.usedSpace);
    console.log(this.user.availibleSpace);
    console.log(this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace))

    return (this.validation.checkResourceRequirements(building!.cost, this.user!.resources)&&(this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace)));
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }
}

