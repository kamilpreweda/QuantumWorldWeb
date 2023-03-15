import { Component } from '@angular/core';
import { BuildingType, ResearchType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { JwtTokenService } from 'src/app/services/jwt-token.service';
import { ResearchService } from 'src/app/services/research.service';
import { UserService } from 'src/app/services/user.service'
import { ValidationService } from 'src/app/services/validation.service';
import { CountdownEvent, CountdownConfig } from 'ngx-countdown';

@Component({
  selector: 'app-research',
  templateUrl: './research.component.html',
  styleUrls: ['./research.component.css']
})
export class ResearchComponent {
  user: User;
  type: ResearchType;
  isResearchUpgrading = false;
  configs: { [key in ResearchType]: CountdownConfig } = {
    [ResearchType.TheExpanseResearch]: { leftTime: 0, demand: true },
    [ResearchType.ArtOfWarResearch]: { leftTime: 0, demand: true },
    [ResearchType.HyperdriveResearch]: { leftTime: 0, demand: true },
    [ResearchType.TerraformingResearch]: { leftTime: 0, demand: true },
  }

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService, private researchService: ResearchService, private validation: ValidationService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => {
      this.user = result;
      var researches = this.user.research;
      researches.forEach(research => {
        this.configs[research.type] = { leftTime: research.timeToBuildInSeconds, demand: true }
      });
      this.isResearchUpgrading = this.user.research.some(r => r.isUnderConstruction);
      if (this.isResearchUpgrading) {
        var research = this.user.research.find(r => r.isUnderConstruction === true);
        this.configs[research!.type] = { leftTime: research!.timeToBuildInSeconds, demand: false }
      }
    });
  }

  discover(type: ResearchType) {
    return this.researchService.upgradeResearch(type, this.user.username);
  }

  canBuild(type: ResearchType): boolean {
    var research = this.user.research.find(r => (r.type === type));
    var labolatoryLevel = this.user.buildings.find(b => (b.type === BuildingType.Labolatory))!.level;
    var isLabolatoryUpgrading = this.user.buildings.find(b => (b.type === BuildingType.Labolatory))!.isUnderConstruction;
    return (this.validation.checkRequiredBuildingLevel(labolatoryLevel, research!.labolatoryLevelRequirement)) && (!isLabolatoryUpgrading);
  }

  hasEnoughResources(type: ResearchType): boolean {
    var research = this.user.research.find(r => (r.type === type));
    return this.validation.checkResourceRequirements(research!.cost, this.user!.resources);
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  handleCountdown(e: CountdownEvent, type: ResearchType) {
    var research = this.user.research.find(r => (r.type === type));
    this.isResearchUpgrading = true;
    research!.isUnderConstruction = true;
    if (e.action === 'done' && this.canBuild(type)) {
      this.discover(type)!.subscribe(() => {
        this.isResearchUpgrading = false;
        research!.isUnderConstruction = false;
        this.configs[type] = { ...this.configs[type], demand: true }
        window.location.reload();
      });
      this.isResearchUpgrading = false;
      research!.isUnderConstruction = false;
    }
  }

  onClick(type: ResearchType, username: string) {
    this.researchService.setConstructionStartDate(type, username).subscribe(() => {
      window.location.reload();
    });
  }

  isLabolatoryRequirementMet(type: ResearchType): string {

    var labolatoryLevel = this.user.buildings.find(b => b.type === BuildingType.Labolatory)!.level;
    var researchRequirement = this.user.research.find(r => r.type === type)!.labolatoryLevelRequirement;
    if (labolatoryLevel >= researchRequirement) {
      return 'green';
    }
    return 'red';
  }
}


