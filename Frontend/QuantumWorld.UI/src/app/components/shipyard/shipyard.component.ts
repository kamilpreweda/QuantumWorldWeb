import { Component, Input } from '@angular/core';
import { BuildingType, ShipType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
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
  users: User[] = [];
  type: ShipType;
  email: string = "string";

  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private shipService: ShipService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  build(type: ShipType): void {
    if (this.canBuild(type)) {
      this.shipService.buildShip(type, this.email).subscribe(() => {
        window.location.reload();
      });
    }
  }
  canBuild(type: ShipType): boolean {
    var ship = this.user.ships.find(s => (s.type === type));
    var spaceshipFactoryLevel = this.user.buildings.find(b => (b.type === BuildingType.SpaceshipFactory))!.level;

    return (this.validation.checkResourceRequirements(ship!.cost, this.user!.resources) && (this.validation.checkSpaceshipFactoryLevel(spaceshipFactoryLevel, ship!.spaceshipFactoryLevelRequirement)));
  }
}

