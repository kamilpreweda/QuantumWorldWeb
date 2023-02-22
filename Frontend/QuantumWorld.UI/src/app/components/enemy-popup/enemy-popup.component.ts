import { Component, Input } from '@angular/core';
import { Enemy, EnemyType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { MapService } from 'src/app/services/map.service';
import { ValidationService } from 'src/app/services/validation.service';

@Component({
  selector: 'app-enemy-popup',
  templateUrl: './enemy-popup.component.html',
  styleUrls: ['./enemy-popup.component.css']
})
export class EnemyPopupComponent {
  @Input() enemy?: Enemy;
  @Input() user!: User;
  type: EnemyType;
  email: string = "string";

  constructor(public displayHelper: DisplayHelperService, private mapService: MapService, private validation: ValidationService) { }

  attack(type: EnemyType): void {
    if (this.canAttack()) {
      this.mapService.startBattle(type, this.email).subscribe(() => {
        window.location.reload();
      })
    };
  }

  canAttack(): boolean {
    return (this.validation.checkResearchRequirements(this.enemy!.requirements, this.user!.research)&&(this.validation.checkIfPlayerHasAnyShips(this.user.ships)))
  }
}
