import { Component, Input } from '@angular/core';
import { BuildingType, ShipType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ShipService } from 'src/app/services/ship.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-shipyard',
  templateUrl: './shipyard.component.html',
  styleUrls: ['./shipyard.component.css']
})
export class ShipyardComponent {
  user: User;
  type: ShipType;

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private shipService: ShipService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
    console.log(this.getUsername());
    console.log(this.user);
  }

  build(type: ShipType, count: number): void {
    if (this.canBuild(type)) {
      this.shipService.buildShip(type, count, this.user.username).subscribe(() => {
        window.location.reload();
      });
    }
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
}

