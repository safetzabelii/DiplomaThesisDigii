import { Department } from "./Department";

export interface Faculty {
    id: number;
    name: string;
    departments: Department[];
}