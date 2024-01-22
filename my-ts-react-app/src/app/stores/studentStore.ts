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
            const thesisid = await agent.Students.getCurrentThesisId();
            runInAction(() => {
                this.setCurrentThesisId(thesisid);
            });
            return thesisid;
        } catch (error) {
            console.log(error);
            return null;
        }
    }
    getCurrentThesis = async () => {
        try {
            const thesis = await agent.Students.getCurrentThesis();
            runInAction(() => {
                this.setCurrentThesis(thesis);
            });
            return thesis; 
        } catch (error) {
            console.error("Error fetching current thesis", error);
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
        const thesisid = this.currentThesisId;
    
        if (!thesisid) {
            return false;
        }
    
        this.submitting = true;
        try {
            await agent.Students.cancelApplication(thesisid);
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


    