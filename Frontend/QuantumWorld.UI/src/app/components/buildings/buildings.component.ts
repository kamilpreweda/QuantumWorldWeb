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
  configs: { [key in BuildingType]: CountdownConfig } = {
    [BuildingType.CarbonFiberFactory]: { leftTime: 0, demand: true },
    [BuildingType.HiggsBosonDetector]: { leftTime: 0, demand: true },
    [BuildingType.QuantumGlassFactory]: { leftTime: 0, demand: true },
    [BuildingType.Labolatory]: { leftTime: 0, demand: true },
    [BuildingType.SpaceshipFactory]: { leftTime: 0, demand: true },
  }

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private buildingService: BuildingService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => {
      this.user = result;
      var buildings = this.user.buildings;
      buildings.forEach(building => {
        this.configs[building.type] = { leftTime: building.timeToBuildInSeconds, demand: true }
      });
      this.isBuildingUpgrading = this.user.buildings.some(b => b.isUnderConstruction);
      console.log(this.user.buildings.some(b => b.isUnderConstruction));
      if (this.isBuildingUpgrading) {
        var building = this.user.buildings.find(b => b.isUnderConstruction === true);
        this.configs[building!.type] = { leftTime: building!.timeToBuildInSeconds, demand: false }
        // demand false if timer has to continue
      }
    });
  }

  build(type: BuildingType) {
    // if (this.canBuild(type)) {
    return this.buildingService.upgradeBuilding(type, this.user.username);
    // }
    // return 1;
  }

  canBuild(type: BuildingType): boolean {
    var building = this.user.buildings.find(b => (b.type === type));
    return (this.validation.checkResourceRequirements(building!.cost, this.user!.resources) && (this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace)));
    // && (!this.isBuildingUpgrading));
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  handleCountdown(e: CountdownEvent, type: BuildingType) {
    var building = this.user.buildings.find(b => (b.type === type));
    this.isBuildingUpgrading = true;
    building!.isUnderConstruction = true;
    console.log(e.action, this.canBuild(type), (e.action && this.canBuild(type)));
    // debugger
    if (e.action === 'done' && this.canBuild(type)) {
      this.build(type)!.subscribe(() => {
        this.isBuildingUpgrading = false;
        building!.isUnderConstruction = false;
        this.configs[type] = { ...this.configs[type], demand: true }
        console.log(this.user.buildings.some(b => b.isUnderConstruction));
        console.log('Here i am')
        window.location.reload();
      });
      this.isBuildingUpgrading = false;
      building!.isUnderConstruction = false;
    }
  }

  onClick(type: BuildingType, username: string) {
    this.buildingService.setConstructionStartDate(type, username);
  }
}


