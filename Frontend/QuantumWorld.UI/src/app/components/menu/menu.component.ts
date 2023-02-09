import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  constructor(private router: Router) { }

  changePage(path: string): void {
    const navigationDetails: string[] = []
    navigationDetails.push(path);
    this.router.navigate(navigationDetails);
  }

  changeAppearance(buttonId: string): void {
    const menuButton = document.getElementById(buttonId);
    if (menuButton === null) {
      return;
    }
    const buttons = document.getElementsByClassName("menuButton");
    Array.prototype.filter.call(
      buttons,
      (button) => { button.style.borderWidth = "0px", button.style.color = '#FFFFFF'}

    );
    menuButton.style.borderWidth = "1px";
    menuButton.style.borderColor = "#00FFFF";
    menuButton.style.color = '#FFD700';
  }
}
