import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent {
  user: User;
  users: User[] = [];
  showDetails: boolean = false;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0], console.log(this.user); });
  }

  changeDisplayedName(name: string): string {
    var changedName: string;

    if (name.includes("Resource")) {
      changedName = name.replace("Resource", "");
      changedName = changedName.replace(/[^A-Z]+/g, "");
      return changedName;
    }
    else if (name.includes("Research")) {
      changedName = name.replace("Research", "");
      return changedName;
    }
    else if (name.includes("Ship")) {
      changedName = name.replace("Ship", "");
      return changedName;
    }
    return changedName = name;
  }

  addSpaces(name: string): string {
    var changedName = name.replace(/([A-Z])/g, ' $1').trim();
    return changedName;
  }

  displayShortName(name: string): string {
    var changedName = name.replace("Resource", "");
    changedName = changedName.replace(/[^A-Z]+/g, "");
    return changedName;
  }

  changeAppearance(buttonId: string): void {
    const menuButton = document.getElementById(buttonId);
    if (menuButton === null) {
      return;
    }
    const buttons = document.getElementsByClassName("menuButton");
    Array.prototype.filter.call(
      buttons,
      (button) => { button.style.borderWidth = "0px", button.style.color = '#FFFFFF' }

    );
    if (!this.showDetails) {
      menuButton.style.borderWidth = "1px";
      menuButton.style.borderColor = "#00FFFF";
      menuButton.style.color = '#FFD700';
    }
    else {
      menuButton.style.borderWidth = "1px";
      menuButton.style.borderColor = "#FFFFFF";
      menuButton.style.color = '#FFFFFF';
    }    
  }
}

