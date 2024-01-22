import React, { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { useStore } from '../../app/stores/store';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const CancelApplication = () => {
    const { studentStore } = useStore();
    const [currentThesisId, setCurrentThesisId] = useState<number | null>(null);
    const [currentThesis, setCurrentThesis] = useState<any>(null); 

    useEffect(() => {
        const loadCurrentThesisId = async () => {
            try {
                const id = await studentStore.getCurrentThesisId();
                setCurrentThesisId(id);
                if (id) {
                    const thesis = await studentStore.getCurrentThesis();
                    setCurrentThesis(thesis);
                }
            } catch (error) {
                console.error("Failed to fetch current thesis ID", error);
                toast.error("Failed to load current thesis ID");
            }
        };

        loadCurrentThesisId();
    }, [studentStore]);

    const handleCancel = async () => {
        if (!currentThesisId) {
            toast.error('There is no current thesis ID');
            return;
        }

        try {
            const success = await studentStore.cancelApplication();
            if (success) {
                toast.success('Application cancelled successfully');
                setCurrentThesisId(null);
                setCurrentThesis(null); 
            } else {
                toast.error('Cancellation failed');
            }
        } catch (error) {
            console.error("Cancellation error", error);
            toast.error('Error cancelling the application');
        }
    };

    return (
        <LayoutWithSidebar>
            <div className="flex justify-center items-center h-screen bg-gray-100 p-4">
                <div className="w-full max-w-2xl bg-white rounded-lg shadow-md p-6">
                    <h2 className="text-xl font-semibold text-gray-700 mb-6">Cancel Application</h2>
                    {currentThesis ? (
                        <div>
                            <p className="mb-4">
                                <strong>Thesis Title:</strong> {currentThesis.titleName}
                            </p>
                            <p className="mb-4">
                                <strong>Student:</strong> {currentThesis.studentName || 'N/A'}
                            </p>
                            <p className="mb-4">
                                <strong>Level:</strong> {currentThesis.level || 'N/A'}
                            </p>
                            <div className="flex justify-end mt-4">
                                <button
                                    type="button"
                                    onClick={handleCancel}
                                    className="px-4 py-2 bg-red-500 hover:bg-red-700 text-white font-bold rounded focus:outline-none focus:shadow-outline transition duration-300"
                                >
                                    Cancel Application
                                </button>
                            </div>
                        </div>
                    ) : (
                        <p>No current thesis to cancel</p>
                    )}
                </div>
            </div>
        </LayoutWithSidebar>
    );
};

export default CancelApplication;
