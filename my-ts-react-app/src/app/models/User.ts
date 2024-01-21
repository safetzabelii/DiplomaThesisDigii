export interface User {
    id: number;
    name: string;
    surname: string;
    dob: Date;
    email: string;
    password: string;
    gender: string;
    phone: string;
    address: string;
    role?: string;
    token?: string;
  }
  