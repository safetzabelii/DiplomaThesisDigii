import { Department } from "./Department";
import { DiplomaThesis } from "./DiplomaThesis";
import { Field } from "./Field";
import { User } from "./User";

export interface Student {
  id: number;
  ects: number;
  degreeLevel: string;
  user: User;
  diplomaThesisId: number | null;
  diplomaThesis: DiplomaThesis | null;
  fieldId: number;
  field: Field;
  departmentId: number;
  department: Department;
}
