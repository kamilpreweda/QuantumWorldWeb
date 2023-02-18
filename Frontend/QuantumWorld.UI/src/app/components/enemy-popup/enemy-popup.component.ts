import { Component, Input } from '@angular/core';
import { Enemy, EnemyType } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { MapService } from 'src/app/services/map.service';

@Component({
  selector: 'app-enemy-popup',
  templateUrl: './enemy-popup.component.html',
  styleUrls: ['./enemy-popup.component.css']
})
export class EnemyPopupComponent {
  @Input() enemy?: Enemy;
  type: EnemyType;
  email: string = "string"

  constructor(public displayHelper: DisplayHelperService, private mapService: MapService) { }

  attack(type: EnemyType): void {
    this.mapService.startBattle(type, this.email).subscribe(() => {
      window.location.reload();
    });
    console.log("button clicked");
  }
}

