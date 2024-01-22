import React, { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { useStore } from '../../app/stores/store';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';
import { Mentor } from '../../app/models/Mentor';
import { Title } from '../../app/models/Title';
import { CreateApplciationDTO } from '../../app/models/DTOS/CreateApplicationDTO';

const SubmitApplicationForm = () => {
    const [application, setApplication] = useState<CreateApplciationDTO>({ titleName: '', mentorId: 0 });
    const [titles, setTitles] = useState<Title[]>([]);
    const [mentors, setMentors] = useState<Mentor[]>([]);
    const { studentStore, titleStore, userStore } = useStore();
    const [submitSuccess, setSubmitSuccess] = useState(false); 
    const [currentThesisId, setCurrentThesisId] = useState<number | null>(null);

    


    useEffect(() => {
        const loadMentors = async () => {
            try {
                const fetchedMentors = await userStore.getMentors();
                setMentors(fetchedMentors);
            } catch (error) {
                console.error("Failed to fetch mentors", error);
                toast.error("Failed to load mentors");
            }
        };
        const loadTitles = async () => {
            try {
                await titleStore.loadTitles();
                const updatedTitles = Array.from(titleStore.titlesRegistry.values());
                setTitles(updatedTitles);
            } catch (error){
                console.error("Failed to fetch fields", error);
                toast.error("Failed to load fields");
            }
        };

        loadMentors();
        loadTitles();

        studentStore.getCurrentThesisId().then((id) => {
            setCurrentThesisId(id);
        });
    }, [titleStore, userStore, studentStore]);
    
    
    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setSubmitSuccess(false);
    
        if (!application.titleName || application.mentorId === 0) {
            toast.error('Please select both a title and a mentor');
            return;
        }
    
        try {
            const success = await studentStore.submitApplication(application);
            if (success) {
                toast.success('Application submitted successfully');
                setSubmitSuccess(true);
                setApplication({ titleName: '', mentorId: 0 });
                
                

            } else {
                toast.error('Submission failed');
            }
        } catch (error) {
            console.error("Submission error", error);
            toast.error('Error submitting the application');

        }
    };

    

    return (
        <LayoutWithSidebar>
            <div className="flex justify-center items-center h-screen bg-gray-100 p-4">
                <div className="w-full max-w-2xl bg-white rounded-lg shadow-md p-6">
                    <h2 className="text-xl font-semibold text-gray-700 mb-6">Submit Application</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="mb-4">
                            <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="titleName">
                                Title Name
                            </label>
                            <select
                                value={application.titleName}
                                onChange={(e) => setApplication({ ...application, titleName: e.target.value })}
                                className="block w-full px-4 py-2 text-gray-700 bg-white border border-gray-300 rounded-md focus:border-blue-500 focus:outline-none focus:ring"
                                id="titleName"
                            >
                                <option value="">Select a Title</option>
                                {titles.map((title) => (
                                    <option key={title.id} value={title.titleName}>
                                        {title.titleName}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className="mb-4">
                            <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="mentorId">
                                Mentor Name
                            </label>
                            <select
                                value={application.mentorId.toString()}
                                onChange={(e) => setApplication({ ...application, mentorId: parseInt(e.target.value, 10) })}
                                className="block w-full px-4 py-2 text-gray-700 bg-white border border-gray-300 rounded-md focus:border-blue-500 focus:outline-none focus:ring"
                                id="mentorId"
                            >
                                <option value="">Select a Mentor</option>
                                {mentors.map((mentor) => (
                                    <option key={mentor.id} value={mentor.id}>
                                        {mentor.name}
                                    </option>
                                ))}
                            </select>
                        </div>

                        <div className="flex justify-between items-center mt-6">
                            <button
                                type="submit"
                                className="px-4 py-2 bg-blue-500 hover:bg-blue-700 text-white font-bold rounded focus:outline-none focus:shadow-outline transition duration-300"
                            >
                                Submit Application
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </LayoutWithSidebar>
);
};


export default SubmitApplicationForm;
