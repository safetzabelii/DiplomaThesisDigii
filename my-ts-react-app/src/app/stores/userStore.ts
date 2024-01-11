import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../models/User";
import agent from "../api/agent";
import { CreateAdminDTO } from "../models/DTOS/CreateAdminDTO";
import { CreateStudentDTO } from "../models/DTOS/CreateStudentDTO";
import { CreateMentorDTO } from "../models/DTOS/CreateMentorDTO";

export default class UserStore {
    user: User | null = null;
    currentUser: User | null = null;
   
    

    constructor() {
        makeAutoObservable(this);
    }

    fetchCurrentUserr = async () => {
        try {
            const user = await agent.Authentication.getLoggedUser();
            runInAction(() => {
                this.currentUser = user;
                this.user = user;
            });
        } catch (error) {
            console.error("Error while fetching current user", error);
            runInAction(() => {
                this.currentUser = null;
                this.user = null;
            });

        }
    }


   
        
    registerAdmin = async (creds: CreateAdminDTO) => {
        try {
            const formattedCreds: CreateAdminDTO = {
                ...creds,
                Dob: new Date(creds.Dob) 
            };

            await agent.Users.createAdmin(formattedCreds);
        } catch (error) {
            console.error(error);
            throw error; 
        }
    }
    registerStudent = async (creds: CreateStudentDTO) => {
        try {
            await agent.Users.createStudent(creds);
            runInAction(() => {
                
            });
        } catch (error) {
            console.error(error);
            throw error;
        }
    }

    getStudents = async () => {
        try {
            const response = await agent.Users.getStudents(); 
            runInAction(() => {
               
            });
            return response; 
        } catch (error) {
            console.error("Error while fetching students", error);
            // Handle error
            throw error;
        }
    }
   
    deleteStudent = async (id: number) => {
        try {
            await agent.Users.deleteStudent(id);
            runInAction(() => {
                // Update state as needed
            });
        } catch (error) {
            console.error(error);
            throw error;
        }
    }

    registerMentor = async (creds: CreateMentorDTO) => {
        try {
            await agent.Users.createMentor(creds);
            runInAction(() => {
                // Handle response if needed
            });
        } catch (error) {
            console.error(error);
            throw error;
        }
    }
    
    getMentors = async () => {
        try {
            const response = await agent.Users.getMentors(); 
            runInAction(() => {

            });
            return response; 
        } catch (error) {
            console.error("Error while fetching mentors", error);
            
            throw error;
        }
    }

    deleteMentor = async (id: number) => {
        try {
            await agent.Users.deleteMentor(id);
            runInAction(() => {
                // Update state as needed
            });
        } catch (error) {
            console.error(error);
            throw error;
        }
    }
            

}


    