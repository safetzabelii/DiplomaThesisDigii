import { Faculty } from "./Faculty";
import { Field } from "./Field";
import { Mentor } from "./Mentor";
import { Student } from "./Student";

export interface Department {
    id: number;
    name: string;
    location: string;
    number: number;
    facultyId: number;
    faculty: Faculty;
    mentors: Mentor[];
    students: Student[];
    fields: Field[];
}
