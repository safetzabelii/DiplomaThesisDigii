import { makeAutoObservable, runInAction } from "mobx";
import { Title } from "../models/Title";
import agent from "../api/agent";
import { toast } from "react-toastify";

export default class TitleStore {
    titlesRegistry = new Map<number, Title>();
    loadingInitial = false;
    submitting = false;

    constructor() {
        makeAutoObservable(this);
    }

    loadTitles = async () => {
        this.loadingInitial = true;
        try {
            const titles = await agent.Titles.getAll();
            runInAction(() => {
                titles.forEach(title => {
                    this.titlesRegistry.set(title.id, title);
                });
                this.loadingInitial = false;
            });
            toast.success('Titles loaded successfully');
        } catch (error) {
            runInAction(() => this.loadingInitial = false);
            toast.error('Error loading titles');
            console.error(error);
        }
    }

    createTitle = async (titleName: string, fieldId: number) => {
        this.submitting = true;
        try {

        const title = {titleName, fieldId};
        const newTitle = await agent.Titles.create(title);

        runInAction(() => {
            if(newTitle){
                this.titlesRegistry.set(newTitle.id, newTitle);
            }
            this.submitting = false;

        });
        toast.success('Title created successfully');
    } catch (error) {
        runInAction(() => this.submitting = false);
        toast.error('Error creating title');
        console.error(error);
    }
}



    getTitlesFromField = async (fieldName: string) => {
        this.loadingInitial = true;
        try {
            const titles = await agent.Titles.getTitlesFromField(fieldName);
            runInAction(() => {
                this.titlesRegistry.clear();
                titles.forEach(title => {
                    this.titlesRegistry.set(title.id, title);
                });
                this.loadingInitial = false;
            });
        } catch (error) {
            runInAction(() => this.loadingInitial = false);
            toast.error('Error loading titles from field');
            console.error(error);
        }
    }

    deleteTitle = async (titleName: string) => {
        this.submitting = true;
        try {
            await agent.Titles.delete(titleName);
            runInAction(() => {
                this.titlesRegistry.forEach((v, k) => {
                    if (v.titleName === titleName) this.titlesRegistry.delete(k);
                });
                this.submitting = false;
            });
            toast.success('Title deleted successfully');
        } catch (error) {
            runInAction(() => this.submitting = false);
            toast.error('Error deleting title');
            console.error(error);
        }
           
        }
    }


