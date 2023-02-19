import { Injectable } from '@angular/core';
import { Research } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  checkResearchRequirements(enemyResearch: Array<Research>, playerResearch: Array<Research>): boolean {

    var canAttack: boolean = false;
    var requirementsMet: boolean = false;

    enemyResearch.forEach(research => {
      var currentPlayerResearch = playerResearch.find(r => r.type === research.type);

      if (currentPlayerResearch!.level < research.level) {
        requirementsMet = false;
        canAttack = false;
        return false;
      }
      canAttack = true
      requirementsMet = true
      return true;
    });
    return canAttack && requirementsMet;
  }
}
