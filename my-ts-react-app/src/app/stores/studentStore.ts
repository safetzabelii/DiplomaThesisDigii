import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { toast } from "react-toastify";
import { Navigate, useNavigate } from "react-router-dom";

export default class StudentStore {
    loading = false;
    submitting = false;

    constructor() {
        makeAutoObservable(this);
    }

    submitApplication = async (titleName: string, mentorId: number) => {
        this.submitting = true;
        try {
            await agent.Students.submitApplication(titleName, mentorId);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success("Application submitted successfully");
            return true; // Indicate success
        } catch (error) {
            runInAction(() => {
                this.submitting = false;
            });
            toast.error("Problem submitting application");
            console.log(error);
            return false; // Indicate failure
        }
    }

    cancelApplication = async (applicationId: number) => {
        this.submitting = true;
        try {
            await agent.Students.cancelApplication(applicationId);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success("Application cancelled successfully");
            return true;
        } catch (error) {
            runInAction(() => {
                this.submitting = false;
            });
            toast.error("Problem cancelling application");
            console.log(error);
            return false;
        }
    }


    

   

}


    