import React from 'react';
import { Navigate } from 'react-router-dom';
import { useStore } from '../stores/store';

interface PrivateRouteProps {
    children: React.ReactElement; 
  }

const PublicRoute: React.FC<PrivateRouteProps> = ({ children }) => {
const {authenticationStore} = useStore();

  if (authenticationStore.isLoggedIn) {
    return <Navigate to="/homepage" replace />;
  }

  return <>{children}</>; 
};

export default PublicRoute;
