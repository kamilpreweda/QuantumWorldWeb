import { Component, ViewChild } from '@angular/core';
import { BuildingType, ShipType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ShipService } from 'src/app/services/ship.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';
import { CountdownEvent, CountdownConfig, CountdownComponent } from 'ngx-countdown';

@Component({
  selector: 'app-shipyard',
  templateUrl: './shipyard.component.html',
  styleUrls: ['./shipyard.component.css']
})
export class ShipyardComponent {
  user: User;
  type: ShipType;
  isShipUpgrading = false;
  @ViewChild('countdown') private countdownComponent: CountdownComponent;
  configs: { [key in ShipType]: CountdownConfig } = {
    [ShipType.LightFighterShip]: { leftTime: 0, demand: true },
    [ShipType.HeavyFighterShip]: { leftTime: 0, demand: true },
    [ShipType.Battleship]: { leftTime: 0, demand: true },
    [ShipType.Destroyer]: { leftTime: 0, demand: true },
    [ShipType.Dreadnought]: { leftTime: 0, demand: true },
    [ShipType.Mothership]: { leftTime: 0, demand: true },
  }

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private shipService: ShipService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => {
      this.user = result;
      var ships = this.user.ships;
      ships.forEach(ship => {
        this.configs[ship.type] = { leftTime: ship.timeToBuildInSeconds, demand: true }
      });
      this.isShipUpgrading = this.user.ships.some(s => s.isUnderConstruction);
      if (this.isShipUpgrading) {
        var ship = this.user.ships.find(s => s.isUnderConstruction === true);
        this.configs[ship!.type] = { leftTime: (ship!.timeToBuildInSeconds), demand: false };
        console.log(ship!.shipsAlreadyBuilt);
      }
    });
  }

  build(type: ShipType) {
    return this.shipService.buildShip(type, this.user.username);
  }
  canBuild(type: ShipType): boolean {
    var ship = this.user.ships.find(s => (s.type === type));
    var spaceshipFactoryLevel = this.user.buildings.find(b => (b.type === BuildingType.SpaceshipFactory))!.level;

    return (this.validation.checkResourceRequirements(ship!.cost, this.user!.resources) && (this.validation.checkRequiredBuildingLevel(spaceshipFactoryLevel, ship!.spaceshipFactoryLevelRequirement)));
  }

  getInputValue(index: number): number {
    var inputValue = (document.getElementById(`input${index}`) as HTMLInputElement).value;
    return +inputValue;
  }
  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  handleCountdown(e: CountdownEvent, type: ShipType) {
    console.log('handleCountdown started');
    var ship = this.user.ships.find(s => (s.type === type));
    this.isShipUpgrading = true;
    console.log(`isShipUpgrading is ${this.isShipUpgrading}`);
    ship!.isUnderConstruction = true;
    console.log(`isUnderConstruction is ${ship!.isUnderConstruction}`);
    console.log(`shipsAlreadyBuilt is ${ship!.shipsAlreadyBuilt}`)
    if (e.action === 'done' && this.canBuild(type)) {
      this.build(type)!.subscribe(() => {
        ship!.shipsAlreadyBuilt++;
        // debugger
        if (ship!.shipsAlreadyBuilt < ship!.shipsToBuild) {
          ship!.shipsToBuild--;
          this.countdownComponent.restart();
          // window.location.reload();
        } else {
          this.isShipUpgrading = false;
          ship!.isUnderConstruction = false;
          window.location.reload();
        }
        const countElement = document.getElementById(`ship-count-${ship!.type}`);
        if (countElement) {
          countElement.innerHTML = ` (Count: ${ship!.count + 1})`;
        }
      });
    }
  }

  onClick(type: ShipType, username: string, count: number) {
    this.shipService.setConstructionStartDate(type, username, count).subscribe(() => {
      window.location.reload();
    })
  }
}

