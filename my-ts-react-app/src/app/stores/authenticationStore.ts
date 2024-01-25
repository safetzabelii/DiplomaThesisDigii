import axios from "axios";
import { makeAutoObservable, runInAction } from "mobx";
import { LoginDTO } from "../models/DTOS/LoginDTO";
import { User } from "../models/User";

export default class AuthenticationStore {
    isLoggedIn = false;
    loggedUser: User | null = null;
    loadingUser = false;
   
    constructor() {
        makeAutoObservable(this);
    }
    
    loggedUserr = async () => {
        try {
            const token = localStorage.getItem("jwt");
            if (token) {
                
                this.isLoggedIn = true;
              
            }
        } catch (error) {
            console.error("Failed to get logged user", error);
            this.isLoggedIn = false;
            this.loggedUser = null;
        }
    }

    login = async (creds: LoginDTO) => {
        try {
            this.loadingUser = true; // Set loading state to true
            const response = await axios.post('/Authentication/login', creds);
            if (response && response.data && response.data.token) {
                const token = response.data.token;
                localStorage.setItem("jwt", token);
                this.isLoggedIn = true;
                
                // Fetch user details here...
                // After fetching, set loadingUser to false
                this.loadingUser = false;
            }
        } catch (error) {
            console.error("Login failed", error);
            this.isLoggedIn = false;
            this.loggedUser = null;
            this.loadingUser = false; // Reset loading state on error
        }
    }

    logout = async () => {
        try {
            localStorage.removeItem("jwt");
            this.isLoggedIn = false;
            this.loggedUser = null;
        } catch (error) {
            console.error("Logout failed", error);
        }
    }
}
