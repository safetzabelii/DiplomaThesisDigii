import axios, { AxiosResponse, AxiosError } from 'axios';
import { error } from 'console';
import { toast } from 'react-toastify';
import { Faculty } from '../models/Faculty';
import { store } from '../stores/store';
import { ServerError } from '../models/serverError';
import { Administrator } from '../models/Administrator';
import { Student } from '../models/Student';
import { User } from '../models/User';
import { Department } from '../models/Department';
import { Mentor } from '../models/Mentor';
import { Field } from '../models/Field';
import { Title } from '../models/Title';
import { DiplomaThesis } from '../models/DiplomaThesis';
import { ConsultationSchedule } from '../models/ConsultationSchedule';
import { CreateAdminDTO } from '../models/DTOS/CreateAdminDTO';


interface ErrorResponseData {
    errors?: { [key: string]: string[] };
}

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    });
    }

axios.defaults.baseURL = 'https://localhost:7076/api';

axios.defaults.withCredentials = true;

axios.interceptors.request.use(config => {
   const token = store.commonStore.getToken();
    if(token) config.headers.Authorization = `Bearer ${token}`;
    
    return config;
})

axios.interceptors.response.use(async response => {
        await sleep(1000);
        return response;
    }, (error: AxiosError) => {
        if (error.response) {
            const { data, status, config } = error.response;
            const typedData = data as ErrorResponseData;
            
    switch (status) {
        case 400:
           if(config.method === 'get' && typedData.errors?.hasOwnProperty('id')){
            return 'error';
           }
           if(typedData.errors){
            const modalStateErrors = [];
            for(const key in typedData.errors){
                if(typedData.errors[key]){
                    modalStateErrors.push(typedData.errors[key])
                }
            }
            throw modalStateErrors.flat();
           }
            break;
        case 401:
            toast.error('unauthorized');
            break;
        case 404:
            console.log(error.response);
            break;
        case 500:
            toast.error('/server-error');
            store.commonStore.setServerError(data as ServerError);
            
            break;
    }
} else {
    console.error('Network Error', error);
        toast.error('Network error or server not reachable');
}

    return Promise.reject(error);
})


const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}): Promise<T> => axios.post<T>(url, body).then(response => response.data),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Faculties = {
    getAll: () => requests.get<any[]>('/Faculties/faculties'),
    create: (facultyName: string) => requests.post<void>('/Faculties/faculty', { name: facultyName }),
    delete: (id: number) => requests.del<void>(`/Faculties/faculty/${id}`),
};
const Administrators = {
    approveThesis: (thesisApplicationId: number) => requests.put<void>(`/Administrator/approve/${thesisApplicationId}`, {}),
    submitThesis: (thesisApplicationId: number) => requests.put<void>(`/Administrator/submitted/${thesisApplicationId}`, {}),
    removeThesis: (thesisApplicationId: number) => requests.del<void>(`/Administrator/remove/${thesisApplicationId}`),
    setThesisDate: (thesisApplicationId: number, date: Date) => requests.put<void>(`/Administrator/setDate`, { thesisApplicationId, date }),
};

const Students = {
    submitApplication: (titleName: string, mentorId: number) => requests.post<void>('/Student/submitapplication', { titleName, mentorId }),
    cancelApplication: (applicationId: number) => requests.del<void>(`/Student/cancelapplication/${applicationId}`),
};

const Titles = {
    getAll: () => requests.get<Title[]>('/Titles/all'),
    create: (title: { titleName: string, fieldId: number }) => requests.post<Title>('/Titles', title),
    getTitlesFromField: (fieldName: string) => requests.get<Title[]>(`/Titles/${fieldName}`),
    delete: (titleName: string) => requests.del<void>(`/Titles/${titleName}`),
};



const Departments = {
    getAll: () => requests.get<any[]>('/Departments/departments'),
    getByFaculty: (facultyId: number) => requests.get<any>(`/Departments/departments/${facultyId}`),
    create: (department: any) => requests.post<void>('/Departments/department', department),
    delete: (departmentId: number) => requests.del<void>(`/Departments/department/${departmentId}`),
};

const Mentors = {
    addDepartment: (departmentId: number) => requests.put<void>(`/Mentor/departments/${departmentId}`, {}),
    removeDepartment: (departmentId: number) => requests.del<void>(`/Mentor/departments/${departmentId}`),
    addField: (fieldId: number) => requests.put<void>(`/Mentor/fields/${fieldId}`, {}),
    removeField: (fieldId: number) => requests.del<void>(`/Mentor/fields/${fieldId}`),
    assessDiplomaThesis: (diplomaThesisId: number, assessment: number) => requests.put<void>(`/Mentor/assessDiplomaThesis/${diplomaThesisId}`, { assessment }),
    setAvailability: (availability: string) => requests.put<void>(`/Mentor/availability`, { availability }),
};


const Fields = {
    getAll: () => requests.get<any[]>('/Fields/all'),
    create: (field: {fieldName: string, departmentId: number}) => requests.post<Field>('/Fields', field),
    delete: (fieldName: string) => requests.del<void>(`/Fields/${fieldName}`),
    getMentors: (fieldName: string) => requests.get<any[]>(`/Fields/${fieldName}/mentors`),
    getStudents: (fieldName: string) => requests.get<any[]>(`/Fields/${fieldName}/students`),
};

const Users = {
    createAdmin: (adminDto: any) => requests.post<void>('/Users/administrator', adminDto),
    deleteAdmin: (adminId: number) => requests.del<void>(`/Users/administrator/${adminId}`),
    createStudent: (studentDto: any) => requests.post<void>('/Users/student', studentDto),
    getStudents: () => requests.get<Student[]>('/Users/students'),
    getMentors: () => requests.get<Mentor[]>('/Users/mentors'),
    deleteStudent: (studentId: number) => requests.del<void>(`/Users/student/${studentId}`),
    createMentor: (mentorDto: any) => requests.post<void>('/Users/mentor', mentorDto),
    deleteMentor: (mentorId: number) => requests.del<void>(`/Users/mentor/${mentorId}`),
};

const Authentication = {
    login: (credentials: any) => requests.post<any>('/Authentication/login', credentials),
    getLoggedUser: () => requests.get<any>('/Authentication/logged-user'),
    logout: () => requests.post<void>('/Authentication/logout', {}),
};






const agent = {
    Faculties,
    Administrators,
    Students,
    Titles,
    Departments,
    Mentors,
    Fields,
    // DiplomaThesis,
    // ConsultationSchedule,
    Users,
    Authentication

}

export default agent;