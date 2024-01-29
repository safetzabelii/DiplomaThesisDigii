// SideNavbar.tsx
import React, { useEffect, useState } from 'react';
import { useStore } from '../stores/store';
import { useNavigate } from 'react-router-dom';
import userStore from '../stores/userStore';
import { Link } from 'react-router-dom';

const SideNavbar: React.FC = () => {
    const { authenticationStore, userStore } = useStore();
    const navigate = useNavigate();
    const [isLoggingOut, setIsLoggingOut] = useState(false); 

    useEffect(() => {
        const fetchUserData = async () => {
            await userStore.fetchCurrentUserr();
        };
        fetchUserData();
    }, [userStore]);


    const handleLogout = () => {
        setIsLoggingOut(true); 
        authenticationStore.logout();
        setTimeout(() => {
          navigate('/');
          setIsLoggingOut(false); 
          window.location.reload();
        }, 1000); 
      };

    return (
        <div className="w-64 h-screen fixed bg-gray-800 text-white flex flex-col">
            <div className="flex items-center justify-center h-20 shadow-md">
                <h1 className="text-2xl font-semibold">Digi-Thesis</h1>
            </div>
            <ul className="flex flex-col flex-1 py-4">
            { userStore.currentUser?.role === 'Admin' && (
                <>
                <li>
                <Link to="/student" className="flex items-center p-4 hover:bg-gray-700">
                <span>Student</span>
                </Link>
                </li>
                <li>
                    <Link to="/mentor" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Mentor</span>
                    </Link>
                </li>
                <li>
                    <Link to="/faculty" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Faculty</span>
                    </Link>
                </li>
                <li>
                    <Link to="/department" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Department</span>
                    </Link>
                </li>
                <li>
                    <Link to="/field" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Field</span>
                    </Link>
                </li>
                <li>
                <Link to="/title" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Title</span>
                    </Link>
                </li>
                </>
                )}
                {userStore.currentUser?.role === 'Student' && (
                <>
                <li>
                    <Link to="/Submitapplication" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Submit Application</span>
                    </Link>
                </li>
                <li>
                    <Link to="/Cancelapplication" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Cancel Application</span>
                    </Link>
                </li>
                </>
                )}
                 {userStore.currentUser?.role === 'Admin' && (
                <>
                <li>
                    <Link to="/Approveapplication" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Approve Thesis</span>
                    </Link>
                </li>
                <li>
                    <Link to="/SubmitThesis" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Submit Thesis</span>
                    </Link>
                </li>
                <li>
                    <Link to="/DeleteThesis" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Delete Thesis</span>
                    </Link>
                </li>
                </>
                )}
            
            {userStore.currentUser?.role === 'Mentor' && (
                <>
                <li>
                    <Link to="/AssessThesis" className="flex items-center p-4 hover:bg-gray-700">
                        <span>Assess Thesis</span>
                    </Link>
                    </li>
                </>
                )}
            
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
