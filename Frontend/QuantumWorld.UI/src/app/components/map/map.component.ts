import { Component } from '@angular/core';
import { Enemy, EnemyType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { JwtTokenService } from 'src/app/services/jwt-token.service'
import { CountdownConfig } from 'ngx-countdown';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent {
  user: User;
  piratesEnemy?: Enemy;
  outsidersEnemy?: Enemy;
  rebelsEnemy?: Enemy;
  armamentsEnemy?: Enemy;
  distantsEnemy?: Enemy;
  ancientsEnemy?: Enemy;
  type: EnemyType;
  isEnemyUnderAttack = false;
  configs: { [key in EnemyType]: CountdownConfig } = {
    [EnemyType.PiratesEnemy]: { leftTime: 0, demand: true },
    [EnemyType.OutsidersEnemy]: { leftTime: 0, demand: true },
    [EnemyType.RebelsEnemy]: { leftTime: 0, demand: true },
    [EnemyType.ArmamentsEnemy]: { leftTime: 0, demand: true },
    [EnemyType.DistantsEnemy]: { leftTime: 0, demand: true },
    [EnemyType.AncientsEnemy]: { leftTime: 0, demand: true },
  }

  constructor(private userService: UserService, private jwtTokenService: JwtTokenService, public displayHelper: DisplayHelperService) { }

  ngOnInit(): void {
    this.userService.getUser(this.getUsername()).subscribe((result: User) => {
      this.user = result;
      this.piratesEnemy = this.user.enemies!.find(enemy => enemy.name === "PiratesEnemy");
      // console.log(this.piratesEnemy);
      this.outsidersEnemy = this.user.enemies?.find(enemy => enemy.name === "OutsidersEnemy");
      // console.log(this.outsidersEnemy);
      this.rebelsEnemy = this.user.enemies?.find(enemy => enemy.name === "RebelsEnemy");
      // console.log(this.rebelsEnemy);
      this.armamentsEnemy = this.user.enemies?.find(enemy => enemy.name === "ArmamentsEnemy");
      // console.log(this.armamentsEnemy);
      this.distantsEnemy = this.user.enemies?.find(enemy => enemy.name === "DistantsEnemy");
      // console.log(this.distantsEnemy);
      this.ancientsEnemy = this.user.enemies?.find(enemy => enemy.name === "AncientsEnemy");
      // console.log(this.ancientsEnemy);
      var enemies = this.user.enemies;
      enemies.forEach(enemy => {
          // console.log(enemy);
          this.configs[enemy!.type] = { leftTime: enemy!.timeToAttackInSeconds, demand: true }
      });
      this.isEnemyUnderAttack = this.user.enemies.some(e => e.isUnderAttack);
      if (this.isEnemyUnderAttack) {
        var enemy = this.user.enemies.find(r => r.isUnderAttack === true);
        this.configs[enemy!.type] = { leftTime: enemy!.timeToAttackInSeconds, demand: false };
        // console.log(this.configs);
      }
    });
  };

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

  loggedIn() {
    return localStorage.getItem("authToken");
  }

  getUsername(): string {
    const username = this.jwtTokenService.getUsernameFromToken();
    return username;
  }
}
