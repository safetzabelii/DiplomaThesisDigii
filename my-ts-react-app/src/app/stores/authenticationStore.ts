import axios from "axios";
import { makeAutoObservable, runInAction } from "mobx";
import { LoginDTO } from "../models/DTOS/LoginDTO";

export default class AuthenticationStore {
    isLoggedIn = false;
    loggedUser = null;
   

    constructor() {
        makeAutoObservable(this);
    }
    loggedUserr = async () => {
        try {
            const response = await axios.get('/Authentication/logged-user');
            if (response && response.data) {
                this.loggedUser = response.data;
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
            const response = await axios.post('/Authentication/login', creds, { withCredentials: true });

            if (response && response.data) {
                this.isLoggedIn = true;
                this.loggedUser = response.data;
                 }
        } catch (error) {
            console.error("Login failed", error);
            this.isLoggedIn = false;
            this.loggedUser = null;
        }
    }

    logout = async () => {
        try {
            await axios.post('/Authentication/logout', {}, { withCredentials: true });
            this.isLoggedIn = false;
            this.loggedUser = null;
        } catch (error) {
            console.error("Logout failed", error);
        }
    }
    
    
    
}

    