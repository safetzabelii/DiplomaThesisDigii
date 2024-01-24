import React, { useState, useEffect } from 'react';
import { observer } from 'mobx-react-lite';
import { Button, Card, Loader } from 'semantic-ui-react';
import AdministratorStore from '../../app/stores/administratorStore';
import LayoutWithSidebar from '../../app/layout/LayoutWithSidebar';

const ApproveApplication: React.FC = () => {
    const [administratorStore] = useState(() => new AdministratorStore());
    const [theses, setTheses] = useState<any>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const loadTheses = async () => {
            try {
                setLoading(true);
                const loadedTheses = await administratorStore.getAllThesis();
                setTheses(loadedTheses);
            } catch (e) {
                setError('Failed to load theses.');
            } finally {
                setLoading(false);
            }
        };
        loadTheses();
    }, [administratorStore]);

    const handleApprove = async (thesisApplicationId: number) => {
        setTheses(theses.map((thesis: any) =>
            thesis.id === thesisApplicationId ? { ...thesis, isApproved: true } : thesis
        ));
    
        try {
            await administratorStore.approveThesisApplication(thesisApplicationId);
        } catch (e) {
            setTheses(theses.map((thesis: any) =>
                thesis.id === thesisApplicationId ? { ...thesis, isApproved: false } : thesis
            ));
            setError('Failed to approve the application.');
        }
    };

    const isApproved = (thesis: any) => {
        return thesis.isApproved || (thesis.dueDate && new Date(thesis.dueDate) > new Date());
    };

    return (
        <LayoutWithSidebar>
        <div className="p-4 bg-gray-100">
            {loading ? ( 
                <Loader active inline="centered">
                    Loading Thesis...
                </Loader>
            ) : theses && theses.length > 0 ? (
                <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
                   {theses.map((thesis: any) => (
                        <div key={thesis.id} className="bg-white rounded-lg shadow-md overflow-hidden">
                            <div className="p-6">
                                <h2 className="text-2xl font-semibold mb-2">Thesis #{thesis.id}</h2>
                                <p className="text-gray-600 mb-4">
                                    {thesis.level} - {thesis.titleName}
                                </p>
                                <div className="mb-4">
                                    <strong>Student:</strong> {thesis.studentName}
                                </div>
                                <div>
                                    <strong>Mentor:</strong> {thesis.mentorName}
                                </div>
                            </div>
                            <div className="p-4 bg-gray-200 border-t border-gray-300">
                                <Button
                                    onClick={() => handleApprove(thesis.id)}
                                    className={`w-full ${
                                        isApproved(thesis)
                                            ? 'bg-gray-400 cursor-not-allowed'
                                            : 'bg-green-500 hover:bg-green-600'
                                    } text-white rounded-full py-2`}
                                    disabled={isApproved(thesis)}
                                >
                                    {isApproved(thesis) ? 'Approved' : 'Approve'}
                                </Button>
                            </div>
                        </div>
                    ))}
                </div>
            ) : (
                <p className="text-center text-gray-600">No theses available for approval.</p>
            )}
        </div>
    </LayoutWithSidebar>
    );
};

export default observer(ApproveApplication);
