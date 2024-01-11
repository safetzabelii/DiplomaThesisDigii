import React, { useEffect, useState } from 'react';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { observer } from 'mobx-react-lite';
import StudentStore from '../../app/stores/studentStore';
import UserStore from '../../app/stores/userStore';
import { Student } from '../../app/models/Student';
import StudentRegister from './studentRegister';


const StudentDashboard = observer(() => {
    const userStore = useState(new UserStore())[0];
    const [students, setStudents] = useState<Student[]>([]);
    

    // useEffect(() => {
    //     const fetchStudents = async () => {
    //         try {
    //             const fetchedStudents = await userStore.getStudents();
    //             console.log('Fetched Students:', fetchedStudents); // Add this line for debugging
    //             if (fetchedStudents) {
    //                 setStudents(fetchedStudents);
    //             }
    //         } catch (error) {
    //             console.error('Error fetching students:', error);
    //         }
    //     };
    //     fetchStudents();
    // }, [userStore]);
   

    return (
         <LayoutWithSidebar>
            <div className="student-dashboard p-6">
                <h1 className="text-3xl font-bold mb-6">Student Dashboard</h1>

                <section className="register-student bg-white p-4 rounded-lg shadow-md mb-6">
                    <h2 className="text-2xl font-semibold mb-4">Register New Student</h2>
                    <StudentRegister />
                </section>
            <section className="student-list bg-white p-4 rounded-lg shadow-md">
                    <h2 className="text-2xl font-semibold mb-4">All Students</h2>
                    <ul>
                    {students.map(student => (
                            <li key={student.id}>
                                {student.user.name} {student.user.surname} - {student.user.email}
                            </li>
                        ))}
                    </ul>
                </section>

            <section className="thesis-applications bg-white p-4 rounded-lg shadow-md mb-6">
                <h2 className="text-2xl font-semibold mb-4">Your Thesis Applications</h2>
                {/* List and manage thesis applications with styled components */}
            </section>

            <section className="available-titles bg-white p-4 rounded-lg shadow-md">
                <h2 className="text-2xl font-semibold mb-4">Available Titles for Thesis</h2>
                {/* List available titles and allow submission of applications with styled components */}
            </section>
        </div>
        </LayoutWithSidebar>
    );
});

export default StudentDashboard;



