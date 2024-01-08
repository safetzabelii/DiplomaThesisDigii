import React, { ReactNode } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";

interface PrivateRouteProps {
    children: React.ReactElement; 
  }

const PrivateRoute: React.FC<PrivateRouteProps> = ({children}) => {
    const location = useLocation();
    const {authenticationStore} = useStore();

    if(!authenticationStore.loggedUserr){

        return <Navigate to="/login" state={{from: location}} replace />;

    }
    return children;
};

export default PrivateRoute;
