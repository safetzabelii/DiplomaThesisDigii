
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import RegisterAdminForm from './features/admins/form/RegisterAdminForm';
import Home from './app/layout/Home';
import LoginForm from './features/general/LoginForm';
import Homepage from './app/layout/Homepage';
import StudentDashboard from './features/students/studentdashboard';
import MentorsDashboard from './features/mentors/mentorsDashboard';
import FacultyPage from './features/facultyPage';
import PrivateRoute from './app/layout/PrivateRoute';
import PublicRoute from './app/layout/PublicRoute';



function App() {
  


  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/register" element={<PublicRoute><RegisterAdminForm/></PublicRoute>} />
        <Route path="/login" element={<PublicRoute><LoginForm/></PublicRoute>} /> 

        <Route path='/mentor' element={<PrivateRoute><MentorsDashboard/></PrivateRoute>} />
        <Route path='/student' element={<PrivateRoute><StudentDashboard/></PrivateRoute>} />
        <Route path="/homepage" element={<PrivateRoute><Homepage/></PrivateRoute>} />
        <Route path="/faculty" element={<PrivateRoute><FacultyPage/></PrivateRoute>} />
        <Route path="*" element={<div>Page not found</div>} />
        

      </Routes>
    </Router>
  );
}

export default App;
