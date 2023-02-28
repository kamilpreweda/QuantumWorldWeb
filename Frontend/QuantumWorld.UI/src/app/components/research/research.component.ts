import { Component } from '@angular/core';
import { BuildingType, ResearchType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ResearchService } from 'src/app/services/research.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-research',
  templateUrl: './research.component.html',
  styleUrls: ['./research.component.css']
})
export class ResearchComponent {
  user: User;
  type: ResearchType;
  
  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private researchService: ResearchService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => { this.user = result; });
  }

  discover(type: ResearchType): void {
    if (this.canBuild(type)) {
      this.researchService.upgradeResearch(type, this.user.username).subscribe(() => {
        window.location.reload();
      });
    }
  }

  canBuild(type: ResearchType): boolean {
    var research = this.user.research.find(r => (r.type === type));
    var labolatoryLevel = this.user.buildings.find(b => (b.type === BuildingType.Labolatory))!.level;
    return (this.validation.checkResourceRequirements(research!.cost, this.user!.resources) && (this.validation.checkRequiredBuildingLevel(labolatoryLevel, research!.labolatoryLevelRequirement)))
  }
  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
