import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { toast } from "react-toastify";
import { Navigate, useNavigate } from "react-router-dom";
import { CreateApplciationDTO } from "../models/DTOS/CreateApplicationDTO";

export default class StudentStore {
    loading = false;
    submitting = false;
    currentThesisId = null;
    currentThesis = null;


    constructor() {
        makeAutoObservable(this);
    }
    
    setCurrentThesisId(id: any) {
        this.currentThesisId = id;
    }
    setCurrentThesis(thesis: any) {
        this.currentThesis = thesis;
    }
    
    getCurrentThesisId = async () => {
        try {
            const thesisId = await agent.Students.getCurrentThesisId();
            runInAction(() => {
                this.setCurrentThesisId(thesisId);
            });
            return thesisId;
        } catch (error) {
            console.log(error);
            return null;
        }
    }
    

    submitApplication = async (application: CreateApplciationDTO) => {
        this.submitting = true;
        try {
            const thesisId = await agent.Students.submitApplication(application);
            runInAction(() => {
                this.submitting = false;
                this.setCurrentThesisId(thesisId);
            });
            toast.success("Application submitted successfully");
            return true;
        } catch (error) {
            runInAction(() => {
                this.submitting = false;
            });
            toast.error("Problem submitting application");
            console.log(error);
            return false;
        }
    }
    
    
    

    cancelApplication = async () => {
        const thesisId = this.currentThesisId;
    
        if (!thesisId) {
            return false;
        }
    
        this.submitting = true;
        try {
            await agent.Students.cancelApplication(thesisId);
            runInAction(() => {
                this.submitting = false;
                this.setCurrentThesisId(null); 
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


    