import { Time } from '@angular/common';
import { Injectable } from '@angular/core';
import { Moment } from 'moment';

@Injectable({
  providedIn: 'root'
})
export class DisplayHelperService {

  constructor() { }

  changeDisplayedName(name: string): string {
    var changedName: string;

    if (name.includes("Resource")) {
      changedName = name.replace("Resource", "");
      return changedName;
    }
    else if (name.includes("Research")) {
      changedName = name.replace("Research", "");
      return changedName;
    }
    else if (name.includes("Ship")) {
      changedName = name.replaceAll("Ship", "");
      return changedName;
    }
    else if (name.includes("Enemy")) {
      changedName = name.replaceAll("Enemy", "");
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

  numberWithDots(number: number) {
    return number.toFixed().toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
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

  replaceComasWithNewLine(message: string[]): string {
    return message.join(',').replaceAll(',', '\n');
  }

  replaceColors() {
    var word: string = "defeated";
    var element = document.getElementById("content");
    var originalHtml = element!.innerHTML;
    var newHtml = originalHtml!.replace(word, word.fontcolor("red"));
  }
}


