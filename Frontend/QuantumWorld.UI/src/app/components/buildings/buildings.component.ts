import { Component, QueryList, ViewChild } from '@angular/core';
import { BuildingType, User } from 'src/app/models/user';
import { BuildingService } from 'src/app/services/building.service';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service'
import { CountdownComponent, CountdownEvent, CountdownConfig } from 'ngx-countdown';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-buildings',
  templateUrl: './buildings.component.html',
  styleUrls: ['./buildings.component.css']
})
export class BuildingsComponent {

  user: User;
  type: BuildingType;
  isBuildingUpgrading = false;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private buildingService: BuildingService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => {
      this.user = result;
      this.isBuildingUpgrading = this.user.buildings.some(b => b.isUpgrading);
    });
  }

  build(type: BuildingType): void {
    var building = this.user.buildings.find(b => (b.type === type));
    if (this.canBuild(type)) {
      this.buildingService.upgradeBuilding(type, this.user.username).subscribe(() => {
        this.isBuildingUpgrading = true;
        building!.isUpgrading = true;
        // window.location.reload();
      });
    }
  }

  canBuild(type: BuildingType): boolean {
    var building = this.user.buildings.find(b => (b.type === type));
    return (this.validation.checkResourceRequirements(building!.cost, this.user!.resources) && (this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace)) && (!this.isBuildingUpgrading));
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  handleCountdown(e: CountdownEvent) {
    if (e.action === 'done') {
      window.location.reload();
    }
  }
}


