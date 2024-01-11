import { Department } from "./Department";
import { DiplomaThesis } from "./DiplomaThesis";
import { Field } from "./Field";
import { User } from "./User";

export interface Student {
  id: number;
  ects: number;
  degreeLevel: string;
  name: string;
  surname: string;
  email: string;
  diplomaThesisId: number | null;
  diplomaThesis: DiplomaThesis | null;
  fieldId: number;
  field: Field;
  departmentId: number;
  department: Department;
}
