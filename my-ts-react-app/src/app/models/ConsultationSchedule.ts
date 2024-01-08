import { DiplomaThesis } from './DiplomaThesis';

export interface ConsultationSchedule {
  id: number;
  diplomaThesisId: number;
  diplomaThesis: DiplomaThesis;
  consultationDate: Date;
  consultationPlace: string;
}
