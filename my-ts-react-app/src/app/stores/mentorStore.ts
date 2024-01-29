import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { toast } from "react-toastify";
import { DiplomaThesis } from "../models/DiplomaThesis";

export default class MentorStore {
  loading = false;
  submitting = false;
  thesis: DiplomaThesis[] = [];
  selectedThesis: DiplomaThesis | null = null;
  assessmentGrade: number | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  getAllThesisMentor = async () => {
    this.setLoading(true);
    try {
      const thesis = await agent.Mentors.getAllThesisMentor();
        this.setLoading(false);
        return thesis;
    } catch (error) {
     this.setSubmitting(false);
     toast.error('Error getting all theses');
     console.error(error);
    }
  }


  setAvailability = async (availability: string) => {
    this.submitting = true;
    try {
      await agent.Mentors.setAvailability(availability);
      runInAction(() => (this.submitting = false));
      toast.success("Availability set successfully");
    } catch (error) {
      runInAction(() => (this.submitting = false));
      toast.error("Problem setting availability");
      console.log(error);
    }
  }

  assessDiplomaThesis = async (thesisId: number, assessmentGrade: number) => {
    if (this.selectedThesis) {
      this.loading = true;
      try {
        await agent.Mentors.assessDiplomaThesis(thesisId, assessmentGrade);
        runInAction(() => {
          this.loading = false;
          this.assessmentGrade = assessmentGrade; 
        });
        toast.success("Diploma thesis assessed successfully");
      } catch (error) {
        runInAction(() => (this.loading = false));
        toast.error("Problem assessing diploma thesis");
        console.log(error);
      }
    }
  }

  addField = async (fieldId: number) => {
    this.loading = true;
    try {
      await agent.Mentors.addField(fieldId);
      runInAction(() => (this.loading = false));
      toast.success("Field added successfully");
    } catch (error) {
      runInAction(() => (this.loading = false));
      toast.error("Problem adding field");
      console.log(error);
    }
  }

  removeField = async (fieldId: number) => {
    this.loading = true;
    try {
      await agent.Mentors.removeField(fieldId);
      runInAction(() => (this.loading = false));
      toast.success("Field removed successfully");
    } catch (error) {
      runInAction(() => (this.loading = false));
      toast.error("Problem removing field");
      console.log(error);
    }
  }

  addDepartment = async (departmentId: number) => {
    this.loading = true;
    try {
      await agent.Mentors.addDepartment(departmentId);
      runInAction(() => (this.loading = false));
      toast.success("Department added successfully");
    } catch (error) {
      runInAction(() => (this.loading = false));
      toast.error("Problem adding department");
      console.log(error);
    }
  }

  removeDepartment = async (departmentId: number) => {
    this.loading = true;
    try {
      await agent.Mentors.removeDepartment(departmentId);
      runInAction(() => (this.loading = false));
      toast.success("Department removed successfully");
    } catch (error) {
      runInAction(() => (this.loading = false));
      toast.error("Problem removing department");
      console.log(error);
    }
  }
  setLoading = (value: boolean) => {
    this.loading = value;
  }
  setSubmitting = (value: boolean) => {
    this.submitting = value;
}
}
