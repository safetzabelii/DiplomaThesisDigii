import { Department } from "./Department";
import { Field } from "./Field";
import { User } from "./User";

export interface Mentor {
    id: number;
    name: string;
    surname: string;
    email: string;
    status: string;
    availability: string;
    fieldId: number;
    field: Field;
    departmentId: number;
    department: Department;
  }