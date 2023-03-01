import { Injectable } from '@angular/core';
import { Resource } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {

  constructor() { }

  GenerateResources(resources: Resource[]) {
    resources.forEach(resource => {
      resource.value += resource.income
    });
  }
}
