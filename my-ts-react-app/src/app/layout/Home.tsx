import React from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
      <h1 className="text-4xl font-bold text-gray-800 mb-6">Welcome to Digi-Thesis</h1>
      <p className="text-lg text-gray-600 mb-4">Explore our features and capabilities.</p>
      <div className="space-x-4">
        
        <Link to="/login" className="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded">
          Login
        </Link>
        {/* Add more links or buttons as needed */}
      </div>
    </div>
  );
};

export default Home;
