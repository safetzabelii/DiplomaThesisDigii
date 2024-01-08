
import { ConsultationSchedule } from './ConsultationSchedule';
import { Mentor } from './Mentor';
import { Student } from './Student';
import { Title } from './Title';

export interface DiplomaThesis {
  id: number;
  dueDate: Date | null;
  submissionDate: Date | null;
  assessment: number | null;
  level: string;
  studentId: number | null;
  student: Student | null;
  mentorId: number;
  mentor: Mentor;
  titleId: number;
  title: Title;
  consultationSchedules: ConsultationSchedule[];
}
