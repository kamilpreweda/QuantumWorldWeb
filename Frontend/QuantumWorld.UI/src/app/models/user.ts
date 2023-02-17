import { Time } from "@angular/common";

export interface User {
    id: string;
    email: string;
    username: string;
    resources?: Resource[];
    buildings?: Building[];
    research?: Research[];
    ships?: Ship[];
    enemies?: Enemy[];
    availibleSpace: number;
    usedSpace: number;
    enemiesDefeated: number;
    points: number;
}

export interface Resource {
    name: string;
    value: number;
}

export interface Building {
    name: string;
    type: BuildingType;
    description: string;
    level: number;
    timeToBuild: Time;
    cost: Resource[];
}

export interface Research {
    name: string;
    description: string;
    level: number;
    timeToBuild: Time;
    cost: Resource[];
}

export interface Ship {
    name: string;
    description: string;
    count: number;
    timeToBuild: Time;
    cost: Resource[];
    healthPoints: number;
    attackPower: number;
}

export interface Enemy {
    name: string;
    description: string;
    timeToAttack: Time;
    rewards: Resource[];
    ships: Ship[];
    requirements: Research[];
}

export type BuildingType =
    "CarbonFiberFactory" |
    "QuantumGlassFactory"

