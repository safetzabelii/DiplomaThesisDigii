// SideNavbar.tsx
import React, { useState } from 'react';
import { useStore } from '../stores/store';
import { useNavigate } from 'react-router-dom';

const SideNavbar: React.FC = () => {
    const { authenticationStore } = useStore();
    const navigate = useNavigate();
    const [isLoggingOut, setIsLoggingOut] = useState(false); 


    const handleLogout = () => {
        setIsLoggingOut(true); 
        authenticationStore.logout();
        setTimeout(() => {
          navigate('/');
          setIsLoggingOut(false); 
        }, 1000); 
      };

    return (
        <div className="w-64 h-screen fixed bg-gray-800 text-white flex flex-col">
            <div className="flex items-center justify-center h-20 shadow-md">
                <h1 className="text-2xl font-semibold">Digi-Thesis</h1>
            </div>
            <ul className="flex flex-col flex-1 py-4">
                <li>
                    <a href="/student" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Student</span>
                    </a>
                </li>
                <li>
                    <a href="/mentor" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Mentor</span>
                    </a>
                </li>
                <li>
                    <a href="/faculty" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Faculty</span>
                    </a>
                </li>
                <li>
                    <a href="/department" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Department</span>
                    </a>
                </li>
                <li>
                    <a href="/field" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Field</span>
                    </a>
                </li>
                <li>
                    <a href="/title" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Title</span>
                    </a>
                </li>
            </ul>
            
            <div className="flex justify-center px-4 mb-4 mt-auto">
                        <button
                    onClick={handleLogout}
                    disabled={isLoggingOut}
                    className="w-full bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
                    >
                    {isLoggingOut ? 'Logging out...' : 'Log out'}
                    </button>
            </div>
        </div>
    );
};

export default SideNavbar;
