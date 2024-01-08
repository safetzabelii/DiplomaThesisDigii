import { Department } from "./Department";
import { Field } from "./Field";
import { User } from "./User";

export interface Mentor {
    id: number;
    status: string;
    availability: string;
    user: User;
    fields: Field[];
    departments: Department[];
  }