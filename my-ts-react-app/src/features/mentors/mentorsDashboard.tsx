import React, { useEffect, useState } from 'react';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { observer } from 'mobx-react-lite';
import { Mentor } from '../../app/models/Mentor';
import userStore from '../../app/stores/userStore';
import MentorRegister from './mentorRegister';

const MentorDashboard = observer(() => {
    const UserStore = useState(new userStore())[0];
    const [mentors, setMentors] = useState<Mentor[]>([]);
    useEffect(() => {
        UserStore.getMentors().then(fetchedMentors => {
            setMentors(fetchedMentors);
        }).catch(error => {
            console.error("Failed to fetch mentors", error);
        });
    }, [userStore]);

    return (
        <LayoutWithSidebar>
            <div className="mentor-dashboard p-6">
                <h1 className="text-3xl font-bold mb-6">Mentor Dashboard</h1>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                    <section className="register-mentor bg-white p-6 rounded-lg shadow-md">
                        <h2 className="text-2xl font-semibold mb-4">Register New Mentor</h2>
                        <MentorRegister />
                    </section>

                    <section className="mentor-list bg-white p-6 rounded-lg shadow-md overflow-hidden">
                        <h2 className="text-2xl font-semibold mb-4">All Mentors</h2>
                        <div className="overflow-x-auto">
                            <table className="min-w-full text-sm divide-y divide-gray-200">
                                <thead>
                                    <tr className="text-left text-gray-500 uppercase bg-gray-50">
                                        <th className="px-4 py-2 font-semibold">Name</th>
                                        <th className="px-4 py-2 font-semibold">Surname</th>
                                        <th className="px-4 py-2 font-semibold">Email</th>
                                        <th className="px-4 py-2 font-semibold">Status</th>
                                        <th className="px-4 py-2 font-semibold">Availability</th>
                                    </tr>
                                </thead>
                                <tbody className="divide-y divide-gray-200 bg-white">
                                    {mentors.map(mentor => (
                                        <tr key={mentor.id} className="hover:bg-gray-50">
                                            <td className="px-4 py-2 font-medium text-gray-900">{mentor.name}</td>
                                            <td className="px-4 py-2 text-gray-700">{mentor.surname}</td>
                                            <td className="px-4 py-2 text-gray-600">{mentor.email}</td>
                                            <td className="px-4 py-2 text-gray-700">{mentor.status}</td>
                                            <td className="px-4 py-2 text-gray-700">{mentor.availability}</td>
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

export default MentorDashboard;
