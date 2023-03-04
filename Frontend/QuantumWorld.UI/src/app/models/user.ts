import { Time } from "@angular/common";

export interface User {
    id: string;
    email: string;
    username: string;
    password: string;
    resources: Resource[];
    buildings: Building[];
    research: Research[];
    ships: Ship[];
    enemies: Enemy[];
    availibleSpace: number;
    usedSpace: number;
    enemiesDefeated: number;
    points: number;
    messages: Message[];
}

export interface Resource {
    name: string;
    value: number;
    income: number;
}

export interface Building {
    name: string;
    type: BuildingType;
    description: string;
    level: number;
    timeToBuildInSeconds: number;
    cost: Resource[];
    isUnderConstruction: boolean;
}

export interface Research {
    name: string;
    type: ResearchType;
    description: string;
    level: number;
    timeToBuildInSeconds: number;
    cost: Resource[];
    labolatoryLevelRequirement: number;
    isUnderConstruction: boolean;
}

export interface Ship {
    name: string;
    type: ShipType;
    description: string;
    count: number;
    shipsToBuild: number;
    shipsAlreadyBuilt: number;
    timeToBuildInSeconds: number;
    cost: Resource[];
    healthPoints: number;
    attackPower: number;
    spaceshipFactoryLevelRequirement: number;
    isUnderConstruction: boolean;
}

export interface Enemy {
    name: string;
    type: EnemyType;
    description: string;
    timeToAttack: Time;
    rewards: Resource[];
    ships: Ship[];
    requirements: Research[];
    isDefeated: boolean;
    isUnderAttack: boolean;
}

export interface Message {
    date: string;
    title: string;
    content: string[];
    id: number;
}

export enum BuildingType {
    CarbonFiberFactory,
    QuantumGlassFactory,
    HiggsBosonDetector,
    Labolatory,
    SpaceshipFactory

}

export enum ResearchType {
    TheExpanseResearch,
    ArtOfWarResearch,
    HyperdriveResearch,
    TerraformingResearch
}

export enum ShipType {
    LightFighterShip,
    HeavyFighterShip,
    Battleship,
    Destroyer,
    Dreadnought,
    Mothership
}

export enum EnemyType {
    PiratesEnemy,
    OutsidersEnemy,
    RebelsEnemy,
    ArmamentsEnemy,
    DistantsEnemy,
    AncientsEnemy
}


