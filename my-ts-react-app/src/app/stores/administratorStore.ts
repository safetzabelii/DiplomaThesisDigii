import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { toast } from "react-toastify";

export default class AdministratorStore {
    submitting = false;

    constructor(){
        makeAutoObservable(this);
    }
    getAllThesis = async () => {
        this.setSubmitting(true);
        try {
            const theses = await agent.Administrators.getAllThesis();
            this.setSubmitting(false);
            return theses; 
        } catch (error) {
            this.setSubmitting(false);
            toast.error('Error getting all theses');
            console.error(error);
        }
    }

    approveThesisApplication = async (thesisApplicationId: number) => {
        this.submitting = true;
        try {
            await agent.Administrators.approveThesis(thesisApplicationId);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success('Thesis application approved successfully');
        } catch (error) {
            runInAction(() => {this.submitting = false});
            toast.error('Error approving thesis application');
            console.error(error);
        }
    }

    submitThesisApplication = async (thesisApplicationId: number) => {
        this.submitting = true;
        try {
            await agent.Administrators.submitThesis(thesisApplicationId);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success('Thesis application submitted successfully');
        } catch (error) {
            runInAction(() => {this.submitting = false});
            toast.error('Error submitting thesis application');
            console.error(error);
        }
    }

    removeThesisApplication = async (thesisApplicationId: number) => {
        this.submitting = true;
        try {
            await agent.Administrators.removeThesis(thesisApplicationId);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success('Thesis application removed successfully');
        } catch (error) {
            runInAction(() => {this.submitting = false});
            toast.error('Error removing thesis application');
            console.error(error);
        }
    }

    setThesisDueDate = async (thesisApplicationId: number, date: Date) => {
        this.submitting = true;
        try {
            await agent.Administrators.setThesisDate(thesisApplicationId, date);
            runInAction(() => {
                this.submitting = false;
            });
            toast.success('Thesis due date set successfully');
        } catch (error) {
            runInAction(() => {this.submitting = false});
            toast.error('Error setting thesis due date');
            console.error(error);
        }
    }
    setSubmitting = (value: boolean) => {
        this.submitting = value;
    }

}