import { Component, Input } from '@angular/core';
import { BuildingType, Resource, User } from 'src/app/models/user';
import { BuildingService } from 'src/app/services/building.service';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service'

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent {
  user: User;
  type: BuildingType;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private buildingService: BuildingService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  build(type: BuildingType): void {
    if(this.canBuild(type)){
    this.buildingService.upgradeBuilding(type, this.user.username).subscribe(() => {
      window.location.reload();
    })
  };
  }

  canBuild(type: BuildingType): boolean {
    var building = this.user.buildings.find(b => (b.type === type));
    return (this.validation.checkResourceRequirements(building!.cost, this.user!.resources)&&(this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace)));
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}

