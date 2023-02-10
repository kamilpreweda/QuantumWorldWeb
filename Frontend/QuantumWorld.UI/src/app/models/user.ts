import { Time } from "@angular/common";

export interface User {
    id: string;
    email: string;
    username: string;
    resources?: Resource[];
    buildings?: Building[];
    research?: Research[];
    ships?: any[];
    enemies?: any[];
}

export interface Resource {
    name: string;
    value: number;
}

export interface Building {
    name: string;
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