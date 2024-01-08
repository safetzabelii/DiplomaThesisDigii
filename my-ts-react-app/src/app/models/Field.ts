import { Department } from "./Department";
import { Mentor } from "./Mentor";
import { Student } from "./Student";
import { Title } from "./Title";

export interface Field {
    id: number;
    fieldName: string;
    departmentId: number | null;
    department: Department | null;
    students: Student[];
    titles: Title[];
    mentors: Mentor[];
  }