import { Component } from '@angular/core';
import { Enemy, EnemyType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent {
  user: User;
  users: User[] = [];
  piratesEnemy?: Enemy;
  outsidersEnemy?: Enemy;
  rebelsEnemy?: Enemy;
  armamentsEnemy?: Enemy;
  distantsEnemy?: Enemy;
  ancientsEnemy?: Enemy;

  constructor(private userService: UserService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => {
      this.users = result; this.user = this.users[0];
      this.piratesEnemy = this.user.enemies.find(enemy => enemy.name === "PiratesEnemy")
      this.outsidersEnemy = this.user.enemies?.find(enemy => enemy.name === "OutsidersEnemy")
      this.rebelsEnemy = this.user.enemies?.find(enemy => enemy.name === "RebelsEnemy")
      this.armamentsEnemy = this.user.enemies?.find(enemy => enemy.name === "ArmamentsEnemy")
      this.distantsEnemy = this.user.enemies?.find(enemy => enemy.name === "DistantsEnemy")
      this.ancientsEnemy = this.user.enemies?.find(enemy => enemy.name === "AncientsEnemy")
    });
  }

  showPopup(id: string) {
    var popup = document.getElementById(id);
    popup?.classList.toggle("show");
  }

  hideAllPopups() {
    const popups = [];
    var piratesPopup = document.getElementById('piratesContainer');
    var outsidersPopup = document.getElementById('outsidersContainer');
    var rebelsPopup = document.getElementById('rebelsContainer');
    var armamentsPopup = document.getElementById('armamentsContainer');
    var distantsPopup = document.getElementById('distantsContainer');
    var ancientsPopup = document.getElementById('ancientsContainer');
    popups.push(piratesPopup, outsidersPopup, rebelsPopup, armamentsPopup, distantsPopup, ancientsPopup)
    popups.forEach(popup => {
      if (popup?.classList.contains("show")) {
        popup!.classList.remove("show");
      }
    });
  }
}
