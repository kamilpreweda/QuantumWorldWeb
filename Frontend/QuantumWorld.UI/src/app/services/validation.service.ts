import { Injectable } from '@angular/core';
import { Research, Resource } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor() { }

  checkResearchRequirements(enemyResearch: Array<Research>, playerResearch: Array<Research>): boolean {

    var canAttack: boolean = false;
    var requirementsMet: boolean = true;

    enemyResearch.forEach(research => {
      var currentPlayerResearch = playerResearch.find(r => r.type === research.type);

      if (currentPlayerResearch!.level < research.level) {
        requirementsMet = false;
        return false;
      }
      canAttack = true
      return true;
    });
    return (canAttack && requirementsMet);
  }

  checkResourceRequirements(costs: Array<Resource>, playerResource: Array<Resource>): boolean {

    var canBuild: boolean = false;
    var requirementsMet: boolean = true;

    costs.forEach(cost => {
      var currentPlayerResource = playerResource.find(r => r.name === cost.name);

      if (currentPlayerResource!.value < cost.value) {
        requirementsMet = false;
        return false;
      }
      canBuild = true
      return true;
    });
    return canBuild && requirementsMet;
  }
}
