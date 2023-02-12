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
    const buttons = document.getElementsByClassName("mapButton");
    Array.prototype.filter.call(
      buttons,
      (button) => { button.classList.remove("active") }
    )
    menuButton.classList.toggle("active");
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
  numberWithDots(number: number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
  }
}
