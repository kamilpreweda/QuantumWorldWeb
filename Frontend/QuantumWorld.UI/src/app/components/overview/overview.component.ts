import { Component } from '@angular/core';
import { Building, Research, Ship, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent {
  user: User;
  buildingInProgress?: Building;
  researchInProgress?: Research;
  shipInProgress?: Ship;


  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { 
      this.user = result;
      this.buildingInProgress = this.user.buildings.find(b => b.isUnderConstruction === true);
      this.researchInProgress = this.user.research.find(r => r.isUnderConstruction === true);
      this.shipInProgress = this.user.ships.find(s => s.isUnderConstruction === true);
    });
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
