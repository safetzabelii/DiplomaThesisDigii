import { Department } from "../Department";
import { Field } from "../Field";

export interface CreateMentorDTO {
    name: string;
    surname: string;
    dob: Date;
    email: string;
    password: string;
    gender: string;
    phone: string;
    address: string;
    role: string;
    status: string;
    availability: string;
    department?: Department;
    departmentId?: number;
    field?: Field;
    fieldId?: number;
  }
  