import { Field } from "./Field";

export interface Title {
    id: number;
    titleName: string;
    fieldId: number;
    field: Field;
  }