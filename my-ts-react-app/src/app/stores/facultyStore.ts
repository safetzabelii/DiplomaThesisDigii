import agent from "../api/agent";
import { makeAutoObservable, runInAction } from "mobx";
import { Faculty } from "../models/Faculty";
import { CreateFacultyDTO } from "../models/DTOS/CreateFacultyDTO";

export default class FacultyStore {
    facultyRegistry = new Map<number, Faculty>();
    selectedFaculty: Faculty | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this);
    }
    get faculties() {
        return Array.from(this.facultyRegistry.values());
    }
    get facultiesById() {
        return Array.from(this.facultyRegistry.values()).sort((a, b) => a.id - b.id);
    }
    get lastItem() {
        return this.faculties[this.faculties.length - 1];
    }

    loadFaculties = async () => {
        this.setLoadingInitial(true);
        try {
            const faculties = await agent.Faculties.getAll();
            runInAction(() => {
                faculties.forEach(faculty => {
                    this.facultyRegistry.set(faculty.id, faculty);
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

    createFaculty = async (createDTO: CreateFacultyDTO) => {
        this.loading = true;
        try {
            await agent.Faculties.create(createDTO.facultyName);
            runInAction(() => {
                this.loadFaculties();
                this.editMode = false;
                this.loading = false;
            });
        } catch (error) {
            console.error("Error creating faculty:", error);
            runInAction(() => {
                this.loading = false;
            });
        }
    };
    
    
    

    deleteFaculty = async () => {
        this.loading = true;
        try {
          await agent.Faculties.delete;
          runInAction(() => {
            this.loading = false;
            this.loadFaculties(); // Reload faculties after deletion
          });
        } catch (error) {
          console.error("Error deleting faculty:", error);
          runInAction(() => {
            this.loading = false;
          });
        }
      };
      


    private setFaculty = (faculty: Faculty) => {
        this.facultyRegistry.set(faculty.id, faculty);
    }

    private getFaculty = (id: number) => {
        return this.facultyRegistry.get(id);
    }

    private cancelSelectedFaculty = () => {
        this.selectedFaculty = undefined;
    }

}

    