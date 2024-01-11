import { createContext,useContext } from "react";
import CommonStore from "./commonStore";
import FacultyStore from "./facultyStore";
import UserStore from "./userStore";
import AuthenticationStore from "./authenticationStore";
import DepartmentStore from "./departmentStore";
import StudentStore from "./studentStore";
import FieldStore from "./fieldStore";
import TitleStore from "./titlesStores";
import MentorStore from "./mentorStore";
import AdministratorStore from "./administratorStore";

interface Store{
    commonStore: CommonStore;
    facultyStore: FacultyStore;
    userStore: UserStore;
    authenticationStore: AuthenticationStore;
    departmentStore: DepartmentStore;
    studentStore: StudentStore;
    fieldStore: FieldStore;
    titleStore: TitleStore;
    mentorStore: MentorStore;
    administatorStore: AdministratorStore;
    
    
}

export const store: Store = {
    commonStore: new CommonStore(),
    facultyStore: new FacultyStore(),
    userStore: new UserStore(),
    authenticationStore: new AuthenticationStore(),
    departmentStore: new DepartmentStore(),
    studentStore: new StudentStore(),
    fieldStore: new FieldStore(),
    titleStore: new TitleStore(),
    mentorStore: new MentorStore(),
    administatorStore: new AdministratorStore(),

}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}