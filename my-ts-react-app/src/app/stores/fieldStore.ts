import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { toast } from "react-toastify";

export interface Field{
    id: number;
    fieldName: string;
    departmentId: number | null;
}


export default class FieldStore {
    fieldRegistry = new Map<number, Field>();
    loadingInitial = false;
    submitting = false;

    constructor(){
        makeAutoObservable(this);
    }

    loadFields = async () => {
        this.loadingInitial = true;
        try {
            const fields = await agent.Fields.getAll();
            runInAction(() => {
                fields.forEach(field => {
                    this.fieldRegistry.set(field.id, field);
                });
                this.loadingInitial = false;
            });
            toast.success('Fields loaded successfully');
        } catch (error) {
            runInAction(() => this.loadingInitial = false);
            toast.error('Error loading fields');
            console.error(error);
        }
    }

    createField = async (fieldName: string, departmentId: number) => {
        this.submitting = true;
        try {
            const field = { fieldName, departmentId };
            const newField = await agent.Fields.create(field);
            runInAction(() => {
                if (newField) {
                    this.fieldRegistry.set(newField.id, newField);
                }
                this.submitting = false;
            });
            toast.success('Field created successfully');
        } catch (error) {
            runInAction(() => this.submitting = false);
            toast.error('Error creating field');
            console.error(error);
        }
    }



    
    
}