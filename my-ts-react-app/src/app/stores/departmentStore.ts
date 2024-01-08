import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { Department } from "../models/Department";

export default class DepartmentStore {
    static loadDepartments() {
        throw new Error("Method not implemented.");
    }
    departmentRegistry = new Map<number, Department>();
    selectedDepartment: Department | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }
    get departments() {
        return Array.from(this.departmentRegistry.values());
    }
    get departmentsById() {
        return Array.from(this.departmentRegistry.values()).sort((a, b) => a.id - b.id);
    }
    get lastItem() {
        return this.departments[this.departments.length - 1];
    }

    loadDepartments = async () => {
        this.setLoadingInitial(true);
        try {
            const departments = await agent.Departments.getAll();
            runInAction(() => {
                departments.forEach(department => {
                    this.departmentRegistry.set(department.id, department);
                });
                this.setLoadingInitial(false);
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.setLoadingInitial(false);
            });
        }
    }

    

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createDepartment = async (departmentName: string) => {
        this.loading = true;
        try {
            await agent.Departments.create(departmentName);
            runInAction(() => {
                this.loadDepartments();
                this.editMode = false;
                this.loading = false;
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    // updateDepartment = async (department: Department) => {
    //     this.loading = true;
    //     try {
    //         await agent.Departments.update(department);
    //         runInAction(() => {
    //             this.loadDepartments();
    //             this.editMode = false;
    //             this.loading = false;
    //         });
    //     } catch (error) {
    //         console.error(error);
    //         runInAction(() => {
    //             this.loading = false;
    //         });
    //     }
    // }

    deleteDepartment = async (departmentId: number) => {
        this.loading = true;
        try {
            await agent.Departments.delete(departmentId);
            runInAction(() => {
                this.departmentRegistry.delete(departmentId);
                this.loading = false;
            });
        } catch (error) {
            console.error(error);
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    private setDepartment = (department: Department) => {
        this.departmentRegistry.set(department.id, department);
    }

    selectDepartment = (id: number) => {
        this.selectedDepartment = this.departmentRegistry.get(id);
    }

    cancelSelectedDepartment = () => {
        this.selectedDepartment = undefined;
    }

}