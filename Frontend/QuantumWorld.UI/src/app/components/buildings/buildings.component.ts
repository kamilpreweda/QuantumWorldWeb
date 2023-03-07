import { Component } from '@angular/core';
import { BuildingType, User } from 'src/app/models/user';
import { BuildingService } from 'src/app/services/building.service';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service'
import { CountdownEvent, CountdownConfig } from 'ngx-countdown';

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
      console.log(this.isResearchInProgress());
      buildings.forEach(building => {
        this.configs[building.type] = { leftTime: building.timeToBuildInSeconds, demand: true };
        console.log(`Can I build ${building.type}? Answer: ${this.canBuild(building.type)}`);
      });
      this.isBuildingUpgrading = this.user.buildings.some(b => b.isUnderConstruction);
      if (this.isBuildingUpgrading) {
        var building = this.user.buildings.find(b => b.isUnderConstruction === true);
        this.configs[building!.type] = { leftTime: building!.timeToBuildInSeconds, demand: false }
      }
    });
  }

  build(type: BuildingType) {
    return this.buildingService.upgradeBuilding(type, this.user.username);
  }

  canBuild(type: BuildingType): boolean {
    var building = this.user.buildings.find(b => (b.type === type));
    if (building?.type === BuildingType.Labolatory && this.isResearchInProgress()) {
      return false;
    }
    if (building?.type === BuildingType.SpaceshipFactory && this.isShipUnderConstruction()) {
      return false;
    }
    return (this.validation.checkResourceRequirements(building!.cost, this.user!.resources) && (this.validation.checkIfPlayerHasSpaceForBuilding(this.user.usedSpace, this.user.availibleSpace)));
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
    if (e.action === 'done' && this.canBuild(type)) {
      this.build(type)!.subscribe(() => {
        this.isBuildingUpgrading = false;
        building!.isUnderConstruction = false;
        this.configs[type] = { ...this.configs[type], demand: true }
        window.location.reload();
      });
      this.isBuildingUpgrading = false;
      building!.isUnderConstruction = false;
    }
  }

  onClick(type: BuildingType, username: string) {
    this.buildingService.setConstructionStartDate(type, username).subscribe(() => {
      window.location.reload();
    });
  }

  isResearchInProgress(): boolean {
    if (this.user.research.some(r => (r.isUnderConstruction === true))) {
      return true;
    }
    else return false;
  }

  isShipUnderConstruction(): boolean {
    if (this.user.ships.some(s => (s.isUnderConstruction === true))) {
      return true;
    }
    else return false;
  }
}


