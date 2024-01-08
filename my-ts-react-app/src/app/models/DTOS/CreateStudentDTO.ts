import { Department } from "../Department";
import { Field } from "../Field";

export interface CreateStudentDTO {
  name: string;
  surname: string;
  dob: Date;
  email: string;
  password: string;
  gender: string;
  phone: string;
  address: string;
  ects: number;
  degreeLevel: string;
  fieldId: number;
  field: Field;
  departmentId: number;
  department: Department;
}
