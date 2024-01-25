import React, { useState } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";

interface PrivateRouteProps {
  children: React.ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  const location = useLocation();
  const { authenticationStore } = useStore();
  const [isLoading, setIsLoading] = useState(true); 

  React.useEffect(() => {
    const checkLoggedIn = async () => {
      try {
        await authenticationStore.loggedUserr();
        setIsLoading(false); 
      } catch (error) {
        console.error("Error checking login status", error);
        setIsLoading(false); 
      }
    };

    checkLoggedIn();
  }, [authenticationStore]);

  if (isLoading) {
    // Render a loading screen while waiting
    return <p>Loading...</p>
  }

  if (!authenticationStore.isLoggedIn) {
   
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <>{children}</>;
};

export default PrivateRoute;

