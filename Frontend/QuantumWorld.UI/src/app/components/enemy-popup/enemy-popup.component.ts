import { Component, Input } from '@angular/core';
import { Enemy, EnemyType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { MapService } from 'src/app/services/map.service';
import { ValidationService } from 'src/app/services/validation.service';
import { CountdownEvent, CountdownConfig } from 'ngx-countdown';
import { JwtTokenService } from 'src/app/services/jwt-token.service';

@Component({
  selector: 'app-enemy-popup',
  templateUrl: './enemy-popup.component.html',
  styleUrls: ['./enemy-popup.component.css']
})
export class EnemyPopupComponent {
  @Input() enemy?: Enemy;
  @Input() user!: User;
  @Input() configs!: { [key in EnemyType]: CountdownConfig };
  type: EnemyType;
  isEnemyUnderAttack: boolean = false;

  constructor(public displayHelper: DisplayHelperService, private mapService: MapService, private validation: ValidationService, private jwtTokenService: JwtTokenService) { }

  attack(type: EnemyType) {
    return this.mapService.startBattle(type, this.user.username);
  }

  canAttack(): boolean {
    return (this.validation.checkResearchRequirements(this.enemy!.requirements, this.user!.research) && (this.validation.checkIfPlayerHasAnyShips(this.user.ships)))
  }

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }

  handleCountdown(e: CountdownEvent, type: EnemyType) {
    // var enemy = this.user.enemies.find(e => (e.type === type));
    this.isEnemyUnderAttack = true;
    this.enemy!.isUnderAttack = true;
    if (e.action === 'done' && this.canAttack()) {
      this.isEnemyUnderAttack = false;
      this.enemy!.isUnderAttack = false;
      this.configs[type] = { ...this.configs[type], demand: true }
      window.location.reload();
      this.isEnemyUnderAttack = false;
      this.enemy!.isUnderAttack = false;
    }
  }

  onClick(type: EnemyType, username: string) {
    console.log(type, console.log(username));
    this.mapService.setAttackStartDate(type, username).subscribe(() => {
      window.location.reload();
    });
  }

  isEnemyRequirementMet(requirementLevel: number, requirementName: string): string {

    var userResearchLevel = this.user.research.find(r => r.name === requirementName)!.level;
    if (userResearchLevel >= requirementLevel) {
      return 'green';
    }
    return 'red';
  }
}
