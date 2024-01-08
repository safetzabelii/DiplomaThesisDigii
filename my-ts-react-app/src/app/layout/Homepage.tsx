import { useEffect } from 'react';
import { observer } from 'mobx-react-lite'; 
import LayoutWithSidebar from './LayoutWithSidebar';
import { useStore } from '../stores/store';

const Homepage = observer(() => {
  const {userStore} = useStore();

  useEffect(() => {
    userStore.fetchCurrentUserr();
  }, []);

  
  return (
    <LayoutWithSidebar>
      <div className="flex h-screen bg-gray-100">
        <div className="m-auto bg-white rounded-lg shadow p-8">
          <h1 className="text-4xl font-bold text-gray-800 mb-6">Welcome to the Dashboard, {userStore.currentUser?.name}</h1>
          <p className="text-lg text-gray-600 mb-4">You are successfully logged in!</p>
          <div className="text-gray-700">
            <div><strong>Email:</strong> {userStore.currentUser?.email}</div>
            <div><strong>Role:</strong> {userStore.currentUser?.role}</div>
          </div>
        </div>
      </div>
    </LayoutWithSidebar>
  );
});

export default Homepage;
