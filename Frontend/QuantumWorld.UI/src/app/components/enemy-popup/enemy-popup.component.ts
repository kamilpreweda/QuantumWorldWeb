import { Component, Input } from '@angular/core';
import { Enemy, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';

@Component({
  selector: 'app-enemy-popup',
  templateUrl: './enemy-popup.component.html',
  styleUrls: ['./enemy-popup.component.css']
})
export class EnemyPopupComponent {
  @Input() enemy?: Enemy; 

  constructor(public displayHelper: DisplayHelperService) { }

}

