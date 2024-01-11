import React, { useEffect, useState } from 'react';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { observer } from 'mobx-react-lite';
import { Student } from '../../app/models/Student';
import StudentRegister from './studentRegister';
import userStore from '../../app/stores/userStore';


const StudentDashboard = observer(() => {
    const UserStore = useState(new userStore())[0];
    const [students, setStudents] = useState<Student[]>([]);
    useEffect(() => {
        UserStore.getStudents().then(fetchedStudents => {
            setStudents(fetchedStudents);
        }).catch(error => {
            console.error("Failed to fetch students", error);
        });
    }, [userStore]);

    

    
   

    return (
        <LayoutWithSidebar>
            <div className="student-dashboard p-6">
                <h1 className="text-3xl font-bold mb-6">Student Dashboard</h1>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <section className="register-student bg-white p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-semibold mb-4">Register New Student</h2>
                        <StudentRegister />
                    </section>

                    <section className="student-list bg-white p-6 rounded-lg shadow-md overflow-hidden">
                        <h2 className="text-2xl font-semibold mb-4">All Students</h2>
                        <div className="overflow-x-auto">
                            <table className="min-w-full text-sm divide-y divide-gray-200">
                                <thead>
                                    <tr className="text-left text-gray-500 uppercase bg-gray-50">
                                        <th className="px-4 py-2 font-semibold">Name</th>
                                        <th className="px-4 py-2 font-semibold">Surname</th>
                                        <th className="px-4 py-2 font-semibold">Email</th>
                                        <th className="px-4 py-2 font-semibold">Degree</th>
                                        <th className="px-4 py-2 font-semibold">ECTS</th>
                                    </tr>
                                </thead>
                                <tbody className="divide-y divide-gray-200 bg-white">
                                {students.map(student => (
                                    <tr key={student.id} className="hover:bg-gray-50">
                                        <td className="px-4 py-2 font-medium text-gray-900">{student.name}</td>
                                        <td className="px-4 py-2 text-gray-700">{student.surname}</td>
                                        <td className="px-4 py-2 text-gray-600">{student.email}</td>
                                        <td className="px-4 py-2 text-gray-700">{student.degreeLevel}</td>
                                        <td className="px-4 py-2 text-gray-700">{student.ects}</td>
                                    </tr>
                                ))}
                            </tbody>
                            </table>
                        </div>
                    </section>
                </div>
            </div>
        </LayoutWithSidebar>
    );
});

export default StudentDashboard;



